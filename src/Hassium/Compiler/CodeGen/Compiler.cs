using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Hassium.Compiler.Parser;
using Hassium.Compiler.Parser.Ast;
using Hassium.Compiler.Scanner;
using Hassium.Compiler.SemanticAnalysis;
using Hassium.Runtime.Objects;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Compiler.CodeGen
{
    public class Compiler : IVisitor
    {
        public static HassiumModule CompileModuleFromSource(string source)
        {
            var tokens = new Lexer().Scan(source);
            var ast = new Parser.Parser().Parse(tokens);
            var table = new SemanticAnalyzer().Analyze(ast);

            return new Compiler().Compile(ast, table);
        }

        private SymbolTable table;
        private HassiumModule module;
        private HassiumMethod method;

        private int labelIndex = 0;
        private int nextLabel()
        {
            return labelIndex++;
        }

        public HassiumModule Compile(AstNode ast, SymbolTable table)
        {
            this.table = table;
            module = new HassiumModule();
            method = new HassiumMethod();

            var globalParent = new HassiumClass();
            foreach (AstNode child in ast.Children)
            {
                if (child is FuncNode)
                {
                    child.Visit(this);
                    method.Parent = new HassiumClass();
                    if (module.Attributes.ContainsKey(method.Name))
                    {
                        if (module.Attributes[method.Name] is HassiumMultiFunc)
                            ((HassiumMultiFunc)module.Attributes[method.Name]).Methods.Add(method);
                        else if (module.Attributes[method.Name] is HassiumMethod)
                            module.Attributes[method.Name] = new HassiumMultiFunc(new List<HassiumMethod>() { method, (HassiumMethod)module.Attributes[method.Name] });
                    }
                    else
                        module.Attributes.Add(method.Name, method);
                }
                else if (child is ClassNode)
                {
                    var clazz = compileClass(child as ClassNode);
                    clazz.Parent = globalParent;
                    module.Attributes.Add(clazz.Name, clazz);
                }
                else if (child is ExpressionStatementNode)
                {
                    if (child.Children[0] is BinaryOperationNode)
                    {
                        var binop = child.Children[0] as BinaryOperationNode;
                        if (binop.BinaryOperation == BinaryOperation.Assignment)
                        {
                            string variable = ((IdentifierNode)binop.Left).Identifier;
                            table.AddGlobalSymbol(variable);
                            var temp = method;
                            method = new HassiumMethod();
                            method.Name = variable;
                            method.SourceRepresentation = string.Format("set_{0}", variable);
                            binop.Right.Visit(this);
                            method.Emit(child.SourceLocation, InstructionType.Return);
                            module.InitialVariables.Add(table.GetGlobalSymbol(variable), method);
                            method = temp;
                        }
                    }
                }
                else if (child is TraitNode || child is PropertyNode || child is UseNode || child is EnumNode)
                    child.Visit(this);
            }
            preformInherits(module);
            return module;
        }

        public void Accept(ArgumentListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(AttributeAccessNode node)
        {
            node.Left.Visit(this);
            if (!module.ConstantPool.ContainsValue(node.Right))
                module.ConstantPool.Add(node.Right.GetHashCode(), node.Right);
            method.Emit(node.SourceLocation, InstructionType.LoadAttribute, node.Right.GetHashCode());
        }
        public void Accept(BinaryOperationNode node)
        {
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    node.Right.Visit(this);
                    if (node.Left is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Left).Identifier;
                        if (table.ContainsGlobalSymbol(identifier))
                        {
                            method.Emit(node.SourceLocation, InstructionType.StoreGlobalVariable, table.GetGlobalSymbol(identifier));
                            method.Emit(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(identifier));
                        }
                        else
                        {
                            if (!table.ContainsSymbol(identifier))
                                table.AddSymbol(identifier);
                            method.Emit(node.SourceLocation, InstructionType.StoreLocal, table.GetSymbol(identifier));
                            method.Emit(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(identifier));
                        }
                    }
                    else if (node.Left is AttributeAccessNode)
                    {
                        AttributeAccessNode accessor = node.Left as AttributeAccessNode;
                        accessor.Left.Visit(this);
                        if (!module.ConstantPool.ContainsValue(accessor.Right))
                            module.ConstantPool.Add(accessor.Right.GetHashCode(), accessor.Right);
                        method.Emit(node.SourceLocation, InstructionType.StoreAttribute, accessor.Right.GetHashCode());
                        accessor.Left.Visit(this);
                    }
                    else if (node.Left is ListAccessNode)
                    {
                        ListAccessNode access = node.Left as ListAccessNode;
                        access.Element.Visit(this);
                        access.Target.Visit(this);
                        method.Emit(node.SourceLocation, InstructionType.StoreListElement);
                    }
                    else if (node.Left is UnaryOperationNode)
                    {
                        var unop = node.Left as UnaryOperationNode;
                        if (unop.UnaryOperation == UnaryOperation.Dereference)
                        {
                            unop.Target.Visit(this);
                            method.Emit(node.SourceLocation, InstructionType.StoreReference);
                        }
                    }
                    break;
                default:
                    node.VisitChildren(this);
                    method.Emit(node.SourceLocation, InstructionType.BinaryOperation, (int)node.BinaryOperation);
                    break;
            }
        }
        public void Accept(BreakNode node)
        {
            method.Emit(node.SourceLocation, InstructionType.Jump, method.BreakLabels.Pop());
        }
        public void Accept(CharNode node)
        {
            var ch = new HassiumChar(node.Char);
            if (!module.ObjectPool.ContainsKey(ch.GetHashCode()))
                module.ObjectPool.Add(ch.GetHashCode(), ch);
            method.Emit(node.SourceLocation, InstructionType.PushObject, ch.GetHashCode());
        }
        public void Accept(ClassNode node)
        {
            module.Attributes.Add(node.Name, compileClass(node));
        }
        private HassiumClass compileClass(ClassNode node)
        {
            if (!module.ConstantPool.ContainsValue(node.Name))
                module.ConstantPool.Add(node.Name.GetHashCode(), node.Name);
            HassiumClass clazz = new HassiumClass();
            clazz.Name = node.Name;
            clazz.Inherits = node.Inherits;
            clazz.TypeDefinition = new HassiumTypeDefinition(clazz.Name);
            clazz.AddType(clazz.TypeDefinition);

            foreach (AstNode child in node.Body.Children)
            {
                if (child is FuncNode)
                {
                    child.Visit(this);
                    method.Parent = clazz;
                    if (clazz.Attributes.ContainsKey(method.Name))
                    {
                        if (clazz.Attributes[method.Name] is HassiumMultiFunc)
                            ((HassiumMultiFunc)clazz.Attributes[method.Name]).Methods.Add(method);
                        else if (clazz.Attributes[method.Name] is HassiumMethod)
                            clazz.Attributes[method.Name] = new HassiumMultiFunc(new List<HassiumMethod>() { method, (HassiumMethod)clazz.Attributes[method.Name] });
                    }
                    else
                        clazz.AddAttribute(method.Name, method);
                }
                else if (child is ClassNode)
                    clazz.AddAttribute(((ClassNode)child).Name, compileClass(child as ClassNode));
                else if (child is EnumNode)
                    clazz.AddAttribute(((EnumNode)child).Name, compileEnum(child as EnumNode));
                else if (child is PropertyNode)
                    clazz.AddAttribute(((PropertyNode)child).Variable, compileProperty(child as PropertyNode, clazz));
            }
            return clazz;
        }
        public void Accept(CodeBlockNode node)
        {
            table.PushScope();
            node.VisitChildren(this);
            table.PopScope();
        }
        public void Accept(ContinueNode node)
        {
            method.Emit(node.SourceLocation, InstructionType.Jump, method.ContinueLabels.Pop());
        }
        public void Accept(DictionaryDeclarationNode node)
        {
            node.VisitChildren(this);
            method.Emit(node.SourceLocation, InstructionType.BuildDictionary, node.Children.Count);
        }
        public void Accept(EnforcedAssignmentNode node)
        {
            node.Value.Visit(this);
            if (!module.ConstantPool.ContainsKey(node.Type.GetHashCode()))
                module.ConstantPool.Add(node.Type.GetHashCode(), node.Type);
            method.Emit(node.SourceLocation, InstructionType.PushConstant, node.Type.GetHashCode());
            if (!table.ContainsSymbol(node.Variable))
                table.AddSymbol(node.Variable);
            method.Emit(node.SourceLocation, InstructionType.EnforcedAssignment, table.GetSymbol(node.Variable));
        }
        public void Accept(EnumNode node)
        {
            module.Attributes.Add(node.Name, compileEnum(node));
        }
        private HassiumEnum compileEnum(EnumNode node)
        {
            HassiumEnum _enum = new HassiumEnum(node.Name);
            foreach (AstNode child in node.Children)
            {
                var op = child as BinaryOperationNode;
                _enum.AddAttribute(((StringNode)op.Left).String, new HassiumInt(((IntegerNode)op.Right).Number));
            }
            return _enum;
        }
        public void Accept(ExpressionStatementNode node)
        {
            node.VisitChildren(this);
            method.Emit(node.SourceLocation, InstructionType.Pop);
        }
        public void Accept(ExtendNode node)
        {
        }
        public void Accept(FloatNode node)
        {
            var fl = new HassiumFloat(node.Number);
            if (!module.ObjectPool.ContainsKey(fl.GetHashCode()))
                module.ObjectPool.Add(fl.GetHashCode(), fl);
            method.Emit(node.SourceLocation, InstructionType.PushObject, fl.GetHashCode());
        }
        public void Accept(ForNode node)
        {
            var startLabel = nextLabel();
            var endLabel = nextLabel();
            method.ContinueLabels.Push(startLabel);
            method.ContinueLabels.Push(endLabel);

            node.StartStatement.Visit(this);
            method.EmitLabel(node.SourceLocation, startLabel);
            node.Predicate.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.JumpIfFalse, endLabel);
            node.Body.Visit(this);
            node.RepeatStatement.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Jump, startLabel);
            method.EmitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(ForeachNode node)
        {
            var startLabel = nextLabel();
            var endLabel = nextLabel();
            method.ContinueLabels.Push(startLabel);
            method.BreakLabels.Push(endLabel);

            int tmp, variable;
            if (!table.ContainsSymbol((++labelIndex).ToString()))
                table.AddSymbol(labelIndex.ToString());
            tmp = table.GetSymbol(labelIndex.ToString());
            if (table.ContainsSymbol(node.Variable))
                variable = table.GetSymbol(node.Variable);
            else
                variable = table.AddSymbol(node.Variable);
            
            node.Target.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Iter);
            method.Emit(node.SourceLocation, InstructionType.StoreLocal, tmp);
            method.EmitLabel(node.SourceLocation, startLabel);
            method.Emit(node.SourceLocation, InstructionType.LoadLocal, tmp);
            method.Emit(node.SourceLocation, InstructionType.IterableFull);
            method.Emit(node.SourceLocation, InstructionType.JumpIfTrue, endLabel);
            method.Emit(node.SourceLocation, InstructionType.LoadLocal, tmp);
            method.Emit(node.SourceLocation, InstructionType.IterableNext);
            method.Emit(node.SourceLocation, InstructionType.StoreLocal, variable);
            node.Body.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Jump, startLabel);
            method.EmitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(FuncNode node)
        {
            if (!module.ConstantPool.ContainsKey(node.Name.GetHashCode()))
                module.ConstantPool.Add(node.Name.GetHashCode(), node.Name);

            method = new HassiumMethod();
            method.Parent = new HassiumClass();
            method.Name = node.Name;
            method.SourceRepresentation = node.GetSourceRepresentation();
            method.ReturnType = node.ReturnType;

            table.PushScope();

            foreach (var param in node.Parameters)
                method.Parameters.Add(param, table.AddSymbol(param.Name));

            node.Children[0].VisitChildren(this);
            table.PopScope();
        }
        public void Accept(FunctionCallNode node)
        {
            foreach (AstNode param in node.Parameters.Children)
                param.Visit(this);
            node.Target.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Call, node.Parameters.Children.Count);
            foreach (var binop in node.InitialAttributes)
            {
                string id = ((IdentifierNode)binop.Left).Identifier;
                if (!module.ConstantPool.ContainsKey(id.GetHashCode()))
                    module.ConstantPool.Add(id.GetHashCode(), id);
                binop.Right.Visit(this);
                method.Emit(node.SourceLocation, InstructionType.PushConstant, id.GetHashCode());
                method.Emit(node.SourceLocation, InstructionType.SetInitialAttribute);
            }
        }
        public void Accept(GlobalNode node)
        {
            if (!table.ContainsGlobalSymbol(node.Variable))
                table.AddGlobalSymbol(node.Variable);
            node.Value.Visit(this);
            module.Globals.Add(table.GetGlobalSymbol(node.Variable), HassiumObject.Null);
            method.Emit(node.SourceLocation, InstructionType.StoreGlobalVariable, table.GetGlobalSymbol(node.Variable));
        }
        public void Accept(IdentifierNode node)
        {
            if (node.Identifier == "this")
            {
                method.Emit(node.SourceLocation, InstructionType.SelfReference);
                return;
            }
            if (table.ContainsGlobalSymbol(node.Identifier))
                method.Emit(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(node.Identifier));
            else if (!table.ContainsSymbol(node.Identifier))
            {
                if (!module.ConstantPool.ContainsValue(node.Identifier))
                    module.ConstantPool.Add(node.Identifier.GetHashCode(), node.Identifier);
                method.Emit(node.SourceLocation, InstructionType.LoadGlobal, node.Identifier.GetHashCode());
            }
            else
                method.Emit(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(node.Identifier));
        }
        public void Accept(IfNode node)
        {
            var elseLabel = nextLabel();
            var endLabel = nextLabel();

            node.Predicate.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.JumpIfFalse, elseLabel);
            node.Body.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Jump, endLabel);
            method.EmitLabel(node.SourceLocation, elseLabel);
            if (node.Children.Count > 2)
                node.ElseBody.Visit(this);
            method.EmitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(IntegerNode node)
        {
            var i = new HassiumInt(node.Number);
            if (!module.ObjectPool.ContainsKey(i.GetHashCode()))
                module.ObjectPool.Add(i.GetHashCode(), i);
            method.Emit(node.SourceLocation, InstructionType.PushObject, i.GetHashCode());
        }
        public void Accept(KeyValuePairNode node)
        {
            node.Value.Visit(this);
            node.Key.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.BuildKeyValuePair);
        }
        public void Accept(LambdaNode node)
        {
            var lambda = compileLambda(node);
            int hash = lambda.GetHashCode();
            if (!module.ObjectPool.ContainsKey(hash))
                module.ObjectPool.Add(hash, lambda);
            method.Emit(node.SourceLocation, InstructionType.PushObject, hash);
            method.Emit(node.SourceLocation, InstructionType.BuildClosure);
        }
        private HassiumMethod compileLambda(LambdaNode node)
        {
            var temp = method;
            method = new HassiumMethod();
            method.Name = "lambda";
            method.Parent = temp.Parent;
            table.PushScope();

            foreach (AstNode param in node.Parameters.Children)
            {
                string name = ((IdentifierNode)param).Identifier;
                if (!table.ContainsSymbol(name))
                    table.AddSymbol(name);
                method.Parameters.Add(new FuncParameter(name), table.GetSymbol(name));
            }

            node.Body.VisitChildren(this);
            table.PopScope();

            var ret = method;
            method = temp;
            return ret;
        }
        public void Accept(ListAccessNode node)
        {
            node.Element.Visit(this);
            node.Target.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.LoadListElement);
        }
        public void Accept(ListDeclarationNode node)
        {
            foreach (var val in node.InitialValues)
                val.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.BuildList, node.InitialValues.Count);
        }
        public void Accept(PropertyNode node)
        {
            module.Attributes.Add(node.Variable, compileProperty(node, method.Parent));
        }
        private HassiumProperty compileProperty(PropertyNode node, HassiumClass parent)
        {
            var temp = method;
            method = new HassiumMethod();
            method.Name = string.Format("get_{0}", node.Variable);
            method.SourceRepresentation = string.Format("{0} ()", method.Name);
            table.PushScope();
            node.GetBody.Visit(this);
            table.PopScope();
            HassiumMethod getBody = method;
            getBody.Parent = parent;
            getBody.ReturnType = "";
            method = new HassiumMethod();
            method.Name = string.Format("set_{0}", node.Variable);
            method.SourceRepresentation = string.Format("{0} (value)", method.Name);
            table.PushScope();
            if (!table.ContainsSymbol("value"))
                table.AddSymbol("value");
            method.Parameters.Add(new FuncParameter("value"), table.GetSymbol("value"));
            node.SetBody.Visit(this);
            table.PopScope();
            HassiumMethod setBody = method;
            setBody.Parent = parent;
            setBody.ReturnType = "";
            HassiumProperty property = new HassiumProperty(getBody, setBody);
            property.Parent = parent;
            method = temp;

            return property;
        }
        public void Accept(RaiseNode node)
        {
            node.Expression.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Raise);
        }
        public void Accept(ReturnNode node)
        {
            node.Value.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Return);
        }
        public void Accept(StatementNode node) {}
        public void Accept(StringNode node)
        {
            var str = new HassiumString(node.String);
            if (!module.ObjectPool.ContainsKey(str.GetHashCode()))
                module.ObjectPool.Add(str.GetHashCode(), str);
            method.Emit(node.SourceLocation, InstructionType.PushObject, str.GetHashCode());
        }
        public void Accept(SwitchNode node)
        {
            if (!table.ContainsSymbol((++labelIndex).ToString()))
                table.AddSymbol(labelIndex.ToString());
            int tmp = table.GetSymbol(labelIndex.ToString());
            int endSwitch = nextLabel();

            node.Expression.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.StoreLocal, tmp);
            foreach (var case_ in node.Cases)
            {
                int trueLabel = nextLabel();
                int falseLabel = nextLabel();
                foreach (var expression in case_.Expressions)
                {
                    method.Emit(node.SourceLocation, InstructionType.LoadLocal, tmp);
                    expression.Visit(this);
                    method.Emit(node.SourceLocation, InstructionType.BinaryOperation, (int)BinaryOperation.EqualTo);
                    method.Emit(node.SourceLocation, InstructionType.JumpIfTrue, trueLabel);
                }
                method.Emit(node.SourceLocation, InstructionType.Jump, falseLabel);
                method.EmitLabel(node.SourceLocation, trueLabel);
                case_.Body.Visit(this);
                method.Emit(node.SourceLocation, InstructionType.Jump, endSwitch);
                method.EmitLabel(node.SourceLocation, falseLabel);
            }
            node.DefaultCase.Visit(this);
            method.EmitLabel(node.SourceLocation, endSwitch);
        }
        public void Accept(TernaryOperationNode node)
        {
            var falseLabel = nextLabel();
            var endLabel = nextLabel();
            node.Predicate.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.JumpIfFalse, falseLabel);
            node.TrueStatement.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Jump, endLabel);
            method.EmitLabel(node.SourceLocation, falseLabel);
            node.FalseStatement.Visit(this);
            method.EmitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(TraitNode node)
        {
            int hash = node.Name.GetHashCode();
            if (!module.ConstantPool.ContainsKey(hash))
                module.ConstantPool.Add(hash, node.Name);
            module.Attributes.Add(node.Name, new HassiumTrait(node.Traits));
        }
        public void Accept(TryCatchNode node)
        {
            var endLabel = nextLabel();
            var temp = method;
            method = new HassiumMethod();
            method.Name = "catch";
            table.PushScope();
            if (!table.ContainsSymbol("value"))
                table.AddSymbol("value");
            method.Parameters.Add(new FuncParameter("value"), table.GetSymbol("value"));
            node.CatchBody.VisitChildren(this);
            var handler = new HassiumExceptionHandler(temp, method, endLabel);
            if (!module.ObjectPool.ContainsKey(handler.GetHashCode()))
                module.ObjectPool.Add(handler.GetHashCode(), handler);
            method = temp;
            method.Emit(node.SourceLocation, InstructionType.PushHandler, handler.GetHashCode());
            node.TryBody.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.PopHandler);
            method.EmitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(TupleNode node)
        {
            for (int i = node.Children.Count - 1; i >= 0; i--)
                node.Children[i].Visit(this);
            method.Emit(node.SourceLocation, InstructionType.BuildTuple, node.Children.Count);
        }
        public void Accept(UnaryOperationNode node)
        {
            switch (node.UnaryOperation)
            {
                case UnaryOperation.BitwiseNot:
                    node.Target.Visit(this);
                    method.Emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.BitwiseNot);
                    break;
                case UnaryOperation.Dereference:
                    node.Target.Visit(this);
                    method.Emit(node.SourceLocation, InstructionType.Dereference);
                    break;
                case UnaryOperation.LogicalNot:
                    node.Target.Visit(this);
                    method.Emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.LogicalNot);
                    break;
                case UnaryOperation.Negate:
                    node.Target.Visit(this);
                    method.Emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.Negate);
                    break;
                case UnaryOperation.PostDecrement:
                case UnaryOperation.PostIncrement:
                case UnaryOperation.PreDecrement:
                case UnaryOperation.PreIncrement:
                    if (node.Target is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Target).Identifier;
                        Instruction loadInstruction, storeInstruction;
                        if (table.ContainsGlobalSymbol(identifier))
                        {
                            loadInstruction = new Instruction(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(identifier));
                            storeInstruction = new Instruction(node.SourceLocation, InstructionType.StoreGlobalVariable, table.GetGlobalSymbol(identifier));
                        }
                        else
                        {
                            loadInstruction = new Instruction(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(identifier));
                            storeInstruction = new Instruction(node.SourceLocation, InstructionType.StoreLocal, table.GetSymbol(identifier));
                        }
                        method.Instructions.Add(loadInstruction);
                        if (node.UnaryOperation == UnaryOperation.PostDecrement || node.UnaryOperation == UnaryOperation.PostIncrement)
                            method.Emit(node.SourceLocation, InstructionType.Duplicate);
                        method.Emit(node.SourceLocation, InstructionType.Push, 1);
                        method.Emit(node.SourceLocation, InstructionType.BinaryOperation, 
                            node.UnaryOperation == UnaryOperation.PostIncrement || node.UnaryOperation == UnaryOperation.PreIncrement ? (int)BinaryOperation.Addition : (int)BinaryOperation.Subraction);
                        method.Instructions.Add(storeInstruction);
                        if (node.UnaryOperation == UnaryOperation.PreDecrement || node.UnaryOperation == UnaryOperation.PreIncrement)
                            method.Instructions.Add(loadInstruction);
                    }
                    break;
                case UnaryOperation.Reference:
                    string variable = ((IdentifierNode)node.Target).Identifier;
                    method.Emit(node.SourceLocation, InstructionType.Reference, table.GetSymbol(variable));
                    break;
            }
        }
        public void Accept(UseNode node)
        {
            string name = node.GetName();
            string path = locateFile(name, ".has");
            if (path == string.Empty)
                path = locateFile(name, ".dll");
            HassiumObject mod;
            if (path.EndsWith(".dll"))
            {
                importModules(path);
                return;
            }
            else if (path != string.Empty)
                mod = CompileModuleFromSource(File.ReadAllText(path));
            else if (InternalModule.InternalModules.ContainsKey(name))
            {
                mod = InternalModule.InternalModules[name];
                module.Imports.Add(name);
            }
            else
                throw new CompileException(node.SourceLocation, "Could not find path or module for use \"{0}\"!", name);

            foreach (var pair in mod.Attributes)
            {
                if (module.Attributes.ContainsKey(pair.Key))
                    module.Attributes.Remove(pair.Key);
                module.Attributes.Add(pair.Key, pair.Value);
            }
            if (mod is HassiumModule)
            {
                foreach (var constant in ((HassiumModule)mod).ConstantPool)
                {
                    if (module.ConstantPool.ContainsKey(constant.Key))
                        module.ConstantPool.Remove(constant.Key);
                    module.ConstantPool.Add(constant.Key, constant.Value);
                }
                foreach (var obj in ((HassiumModule)mod).ObjectPool)
                {
                    if (module.ObjectPool.ContainsKey(obj.Key))
                        module.ObjectPool.Remove(obj.Key);
                    module.ObjectPool.Add(obj.Key, obj.Value);
                }
            }
        }
        public void Accept(WhileNode node)
        {
            var startLabel = nextLabel();
            var endLabel = nextLabel();
            method.ContinueLabels.Push(startLabel);
            method.BreakLabels.Push(endLabel);

            method.EmitLabel(node.SourceLocation, startLabel);
            node.Predicate.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.JumpIfFalse, endLabel);
            node.Body.Visit(this);
            method.Emit(node.SourceLocation, InstructionType.Jump, startLabel);
            method.EmitLabel(node.SourceLocation, endLabel);
        }

        private string locateFile(string path, string extension)
        {
            if (File.Exists(path))
                return path;
            if (File.Exists(path + extension))
                return path + extension;
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                              Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            string homeFilePath = Path.Combine(homePath, path);
            if (File.Exists(homeFilePath))
                return homeFilePath;
            if (File.Exists(homeFilePath + extension))
                return homeFilePath + extension;
            homeFilePath = Path.Combine(Path.Combine(homePath, ".Hassium"), path);
            if (File.Exists(homeFilePath))
                return homeFilePath;
            if (File.Exists(homeFilePath + extension))
                return homeFilePath + extension;
            return string.Empty;
        }

        private void importModules(string path)
        {
            var ass = Assembly.LoadFrom(path);

            foreach (var type in ass.GetTypes())
                if (type.IsSubclassOf(typeof(InternalModule)))
                    foreach (var pair in ((InternalModule)Activator.CreateInstance(type)).Attributes)
                    {
                        if (module.Attributes.ContainsKey(pair.Key))
                            module.Attributes.Remove(pair.Key);
                        module.Attributes.Add(pair.Key, pair.Value);
                    }
        }

        private void preformInherits(HassiumObject obj)
        {
            foreach (var attrib in obj.Attributes.Values)
            {
                if (attrib is HassiumClass)
                {
                    var clazz = attrib as HassiumClass;
                    foreach (string inherit in clazz.Inherits)
                        if (module.Attributes.ContainsKey(inherit))
                        {
                            foreach (var pair in module.Attributes[inherit].Attributes)
                                if (!clazz.Attributes.ContainsKey(pair.Key))
                                {
                                    var val = pair.Value.Clone() as HassiumObject;
                                    val.Parent = clazz;
                                    clazz.Attributes.Add(pair.Key, val);
                                }
                        }
                        else if (clazz.Parent.Attributes.ContainsKey(inherit))
                        {
                            foreach (var pair in clazz.Parent.Attributes[inherit].Attributes)
                                if (!clazz.Attributes.ContainsKey(pair.Key))
                                {
                                    var val = pair.Value.Clone() as HassiumObject;
                                    val.Parent = clazz;
                                    clazz.Attributes.Add(pair.Key, val);
                                }
                        }
                    preformInherits(clazz);
                }
            }
        }
    }
}