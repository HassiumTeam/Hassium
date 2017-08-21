using System;
using System.Collections.Generic;
using System.IO;

using Hassium.Compiler.Exceptions;
using Hassium.Compiler.Lexer;
using Hassium.Compiler.Parser;
using Hassium.Compiler.Parser.Ast;
using Hassium.Runtime;
using Hassium.Runtime.Types;

namespace Hassium.Compiler.Emit
{
    public class HassiumCompiler : IVisitor
    {
        public static HassiumModule CompileModuleFromFilePath(string abspath, bool suppressWarns = false)
        {
            if (!File.Exists(abspath))
                throw new CompilerException(new SourceLocation(abspath, 0, 0), "Could not find input file {0}!", abspath);
            return CompileModuleFromString(abspath, File.ReadAllText(abspath), suppressWarns);
        }
        
        public static HassiumModule CompileModuleFromString(string abspath, string code, bool suppressWarns = false)
        {
            var tokens = new Scanner().Scan(abspath, code);
            var ast = new Parser.Parser().Parse(tokens);
            var module = new HassiumCompiler(suppressWarns).Compile(ast);

            if (!suppressWarns)
                module.DisplayWarnings();

            return module;

        }

        private Stack<HassiumMethod> methodStack;
        private Stack<HassiumClass> classStack;

        private SymbolTable table;
        private HassiumModule module;

        private bool suppressWarns;

        public HassiumCompiler(bool suppressWarns)
        {
            this.suppressWarns = suppressWarns;
        }

        public HassiumModule Compile(AstNode ast, HassiumModule module = null)
        {
            methodStack = new Stack<HassiumMethod>();
            classStack = new Stack<HassiumClass>();

            table = new SymbolTable();
            this.module = module == null ? new HassiumModule() : module;

            classStack.Push(new HassiumClass("__global__"));
            methodStack.Push(new HassiumMethod(module, "__init__") { Parent = classStack.Peek() });

            ast.Visit(this);

            var globalClass = classStack.Pop();
            globalClass.AddAttribute("__init__", methodStack.Pop());
            this.module.AddAttribute("__global__", globalClass);
            return this.module;
        }

        public void Accept(ArgumentListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(AttributeAccessNode node)
        {
            node.Left.Visit(this);
            emit(node.SourceLocation, InstructionType.LoadAttribute, node.Right);
        }
        public void Accept(BinaryOperationNode node)
        {
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    node.Right.Visit(this);
                    // var = val
                    if (node.Left is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Left).Identifier;

                        // Locals are lowercase
                        HassiumWarning.EnforceCasing(module, node.Left.SourceLocation, identifier, HassiumCasingType.Lower);

                        if (table.Scopes.Count == 2)
                            table.AddGlobalSymbol(identifier);

                        if (table.ContainsGlobalSymbol(identifier))
                        {
                            emit(node.SourceLocation, InstructionType.StoreGlobal, identifier);
                            emit(node.SourceLocation, InstructionType.LoadGlobal,  identifier);
                        }
                        else
                        {
                            table.HandleSymbol(identifier);
                            emit(node.SourceLocation, InstructionType.StoreLocal, table.GetSymbol(identifier));
                            emit(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(identifier));
                        }
                    }
                    // var.attrib = val
                    else if (node.Left is AttributeAccessNode)
                    {
                        AttributeAccessNode accessor = node.Left as AttributeAccessNode;
                        accessor.Left.Visit(this);
                        emit(node.SourceLocation, InstructionType.StoreAttribute, accessor.Right);
                        accessor.Left.Visit(this);
                    }
                    // var [index] = val
                    else if (node.Left is IterableAccessNode)
                    {
                        IterableAccessNode access = node.Left as IterableAccessNode;
                        access.Index.Visit(this);
                        access.Target.Visit(this);
                        emit(node.SourceLocation, InstructionType.StoreIterableElement);
                    }
                    break;
                case BinaryOperation.Swap:
                    emit(node.SourceLocation, InstructionType.Push, table.GetSymbol(((IdentifierNode)node.Left).Identifier));
                    emit(node.SourceLocation, InstructionType.Swap, table.GetSymbol(((IdentifierNode)node.Right).Identifier));
                    break;
                default:
                    node.VisitChildren(this);
                    emit(node.SourceLocation, InstructionType.BinaryOperation, (int)node.BinaryOperation);
                    break;
            }
        }
        public void Accept(BreakNode node)
        {
            emit(node.SourceLocation, InstructionType.Jump, methodStack.Peek().BreakLabels.Pop());
        }
        public void Accept(CharNode node)
        {
            emit(node.SourceLocation, InstructionType.PushObject, new HassiumChar(node.Char));
        }
        public void Accept(ClassDeclarationNode node)
        {
            // Classes are PascalCase
            HassiumWarning.EnforceCasing(module, node.SourceLocation, node.Name, HassiumCasingType.Pascal);

            var clazz = new HassiumClass(node.Name);
            clazz.IsPrivate = node.IsPrivate;
            clazz.Parent = classStack.Peek();

            foreach (var inherit in node.Inherits)
            {
                methodStack.Push(new HassiumMethod(module) { Parent = classStack.Peek() });
                inherit.Visit(this);
                emit(inherit.SourceLocation, InstructionType.Return);
                clazz.Inherits.Add(methodStack.Pop());
            }

            classStack.Push(clazz);
            table.EnterScope();

            node.Body.Visit(this);

            table.LeaveScope();
            classStack.Pop();
            classStack.Peek().AddAttribute(node.Name, clazz);
        }
        public void Accept(CodeBlockNode node)
        {
            table.EnterScope();
            node.VisitChildren(this);
            table.LeaveScope();
        }
        public void Accept(ContinueNode node)
        {
            emit(node.SourceLocation, InstructionType.Jump, methodStack.Peek().ContinueLabels.Pop());
        }
        public void Accept(DictionaryDeclarationNode node)
        {
            for (int i = 0; i < node.Keys.Count; i++)
            {
                node.Values[i].Visit(this);
                node.Keys[i].Visit(this);
            }
            emit(node.SourceLocation, InstructionType.BuildDictionary, node.Keys.Count);
        }
        public void Accept(DoWhileNode node)
        {
            var bodyLabel = nextLabel();
            var endLabel = nextLabel();

            int breakLabelCount = methodStack.Peek().BreakLabels.Count;
            int continueLabelCount = methodStack.Peek().ContinueLabels.Count;

            methodStack.Peek().BreakLabels.Push(endLabel);
            methodStack.Peek().ContinueLabels.Push(bodyLabel);

            emitLabel(node.Body.SourceLocation, bodyLabel);
            node.Body.Visit(this);
            node.Condition.Visit(this);
            emit(node.Body.SourceLocation, InstructionType.JumpIfTrue, bodyLabel);
            emitLabel(node.Condition.SourceLocation, endLabel);

            restoreLabels(breakLabelCount, continueLabelCount);
        }
        public void Accept(EnforcedAssignmentNode node)
        {
            methodStack.Push(new HassiumMethod(module));
            methodStack.Peek().Parent = classStack.Peek();
            node.Type.Visit(this);
            emit(node.Type.SourceLocation, InstructionType.Return);
            var enforcedType = methodStack.Pop();

            node.Value.Visit(this);

            if (table.ContainsGlobalSymbol(node.Variable))
            {
                emit(node.SourceLocation, InstructionType.StoreGlobalVariable, table.GetGlobalSymbol(node.Variable));
                emit(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(node.Variable));
            }
            else
            {
                table.HandleSymbol(node.Variable);
                emit(node.SourceLocation, InstructionType.StoreLocal, table.GetSymbol(node.Variable));
                emit(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(node.Variable));
            }

            emit(node.SourceLocation, InstructionType.EnforcedAssignment, enforcedType);
        }
        public void Accept(EnumNode node)
        {
            // Enums are PascalCase
            HassiumWarning.EnforceCasing(module, node.SourceLocation, node.Name, HassiumCasingType.Pascal);

            HassiumEnum enum_ = new HassiumEnum(node.Name);
            enum_.IsPrivate = node.IsPrivate;
            foreach (var pair in node.Attributes)
                enum_.AddAttribute(pair.Value, new HassiumInt(pair.Key));

            classStack.Peek().AddAttribute(enum_.Name, enum_);
        }
        public void Accept(ExpressionStatementNode node)
        {
            node.Expression.Visit(this);

            // Peephole optimization: Remove push/pop redundancies
            var instructions = methodStack.Peek().Instructions;
            var type = instructions[instructions.Count - 1].InstructionType;
            if (type == InstructionType.Push || type == InstructionType.PushConstant || type == InstructionType.PushHandler || type == InstructionType.PushObject)
                instructions.Remove(instructions[instructions.Count - 1]);
            else
                emit(node.SourceLocation, InstructionType.Pop);
        }
        public void Accept(FloatNode node)
        {
            emit(node.SourceLocation, InstructionType.PushObject, new HassiumFloat(node.Float));
        }
        public void Accept(ForNode node)
        {
            var bodyLabel = nextLabel();
            var repeatLabel = nextLabel();
            var endLabel = nextLabel();

            int breakLabelCount = methodStack.Peek().BreakLabels.Count;
            int continueLabelCount = methodStack.Peek().ContinueLabels.Count;

            methodStack.Peek().BreakLabels.Push(endLabel);
            methodStack.Peek().ContinueLabels.Push(repeatLabel);

            table.EnterScope();
            node.InitialStatement.Visit(this);
            emitLabel(node.Condition.SourceLocation, bodyLabel);
            node.Condition.Visit(this);
            emit(node.Condition.SourceLocation, InstructionType.JumpIfFalse, endLabel);
            if (node.Body is CodeBlockNode)
                node.Body.VisitChildren(this);
            else
                node.Body.Visit(this);
            emitLabel(node.RepeatStatement.SourceLocation, repeatLabel);
            node.RepeatStatement.Visit(this);
            emit(node.RepeatStatement.SourceLocation, InstructionType.Jump, bodyLabel);
            emitLabel(node.RepeatStatement.SourceLocation, endLabel);
            table.LeaveScope();

            restoreLabels(breakLabelCount, continueLabelCount);
        }
        public void Accept(ForeachNode node)
        {
            var bodyLabel = nextLabel();
            var endLabel = nextLabel();

            int breakLabelCount = methodStack.Peek().BreakLabels.Count;
            int continueLabelCount = methodStack.Peek().ContinueLabels.Count;

            methodStack.Peek().BreakLabels.Push(endLabel);
            methodStack.Peek().ContinueLabels.Push(bodyLabel);

            table.EnterScope();
            var tmp = table.HandleSymbol(nextLabel().ToString());
            node.Expression.Visit(this);
            emit(node.SourceLocation, InstructionType.Iter);
            emit(node.SourceLocation, InstructionType.StoreLocal, tmp);
            emitLabel(node.SourceLocation, bodyLabel);
            emit(node.SourceLocation, InstructionType.LoadLocal, tmp);
            emit(node.SourceLocation, InstructionType.IterableFull);
            emit(node.SourceLocation, InstructionType.JumpIfTrue, endLabel);
            emit(node.SourceLocation, InstructionType.LoadLocal, tmp);
            emit(node.SourceLocation, InstructionType.IterableNext);
            emit(node.SourceLocation, InstructionType.StoreLocal, table.HandleSymbol(node.Variable));
            if (node.Body is CodeBlockNode)
                node.Body.VisitChildren(this);
            else
                node.Body.Visit(this);
            emit(node.SourceLocation, InstructionType.Jump, bodyLabel);
            emitLabel(node.SourceLocation, endLabel);
            table.LeaveScope();

            restoreLabels(breakLabelCount, continueLabelCount);
        }
        public void Accept(FunctionCallNode node)
        {
            foreach (var param in node.Parameters.Arguments)
                param.Visit(this);
            node.Target.Visit(this);
            emit(node.SourceLocation, InstructionType.Call, node.Parameters.Arguments.Count);

            foreach (var attrib in node.InitialAttributes)
            {
                attrib.Value.Visit(this);
                emit(attrib.Value.SourceLocation, InstructionType.PushConstant, attrib.Key);
                emit(attrib.Value.SourceLocation, InstructionType.SetInitialAttribute);
            }
        }
        public void Accept(FunctionDeclarationNode node)
        {
            // Funcs are lowercase
            HassiumWarning.EnforceCasing(module, node.SourceLocation, node.Name, HassiumCasingType.Lower);

            var method = new HassiumMethod(module, node.Name);
            method.IsPrivate = node.IsPrivate;
            methodStack.Push(method);
            method.SourceLocation = node.SourceLocation;
            method.SourceRepresentation = node.ToString();
            method.Parent = classStack.Peek();

            table.EnterScope();

            foreach (var param in node.Parameters)
            {
                if (param.FunctionParameterType == FunctionParameterType.Enforced)
                {
                    methodStack.Push(new HassiumMethod(module));
                    methodStack.Peek().Parent = classStack.Peek();
                    param.Type.Visit(this);
                    emit(param.Type.SourceLocation, InstructionType.Return);
                    param.EnforcedType = methodStack.Pop();
                }
                method.Parameters.Add(param, table.AddSymbol(param.Name));
            }
            
            if (node.Body is CodeBlockNode)
                node.Body.VisitChildren(this);
            else
                node.Body.Visit(this);

            if (node.EnforcedReturnType != null)
            {
                methodStack.Push(new HassiumMethod(module));
                node.EnforcedReturnType.Visit(this);
                emit(node.EnforcedReturnType.SourceLocation, InstructionType.Return);
                method.ReturnType = methodStack.Pop();
            }

            table.LeaveScope();
            method = methodStack.Pop();

            if (classStack.Peek().ContainsAttribute(method.Name))
            {
                var attrib = classStack.Peek().BoundAttributes[method.Name];
                if (attrib is HassiumMultiFunc)
                    (attrib as HassiumMultiFunc).Methods.Add(method);
                else
                {
                    classStack.Peek().BoundAttributes.Remove(method.Name);
                    var multiFunc = new HassiumMultiFunc();
                    multiFunc.Methods.Add(attrib as HassiumMethod);
                    multiFunc.Methods.Add(method);
                    classStack.Peek().AddAttribute(method.Name, multiFunc);
                }
            }
            else
                classStack.Peek().AddAttribute(method.Name, method);
        }
        public void Accept(IdentifierNode node)
        {
            if (node.Identifier == "this")
                emit(node.SourceLocation, InstructionType.SelfReference);
            else if (table.ContainsGlobalSymbol(node.Identifier))
                emit(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(node.Identifier));
            else if (!table.ContainsSymbol(node.Identifier))
                emit(node.SourceLocation, InstructionType.LoadGlobal, node.Identifier);
            else
                emit(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(node.Identifier));
        }
        public void Accept(IfNode node)
        {
            var elseLabel = nextLabel();
            var endLabel = nextLabel();

            node.Condition.Visit(this);
            emit(node.Condition.SourceLocation, InstructionType.JumpIfFalse, elseLabel);
            node.IfBody.Visit(this);
            emit(node.IfBody.SourceLocation, InstructionType.Jump, endLabel);
            emitLabel(node.ElseBody.SourceLocation, elseLabel);
            node.ElseBody.Visit(this);
            emitLabel(node.ElseBody.SourceLocation, endLabel);
        }
        public void Accept(IntegerNode node)
        {
            emit(node.SourceLocation, InstructionType.PushObject, new HassiumInt(node.Integer));
        }
        public void Accept(IterableAccessNode node)
        {
            node.Index.Visit(this);
            node.Target.Visit(this);
            emit(node.SourceLocation, InstructionType.LoadIterableElement);
        }
        public void Accept(LambdaNode node)
        {
            var lambda = new HassiumMethod(module);
            methodStack.Push(lambda);
            lambda.Parent = classStack.Peek();

            table.EnterScope();

            foreach (var param in node.Parameters.Arguments)
            {
                string name = ((IdentifierNode)param).Identifier;
                lambda.Parameters.Add(new FunctionParameter(FunctionParameterType.Normal, name), table.HandleSymbol(name));
            }

            node.Body.VisitChildren(this);

            table.LeaveScope();
            methodStack.Pop();

            emit(node.SourceLocation, InstructionType.PushObject, lambda);
            emit(node.SourceLocation, InstructionType.BuildClosure);
        }
        public void Accept(ListDeclarationNode node)
        {
            foreach (var element in node.Elements)
                element.Visit(this);
            emit(node.SourceLocation, InstructionType.BuildList, node.Elements.Count);
        }
        public void Accept(MultipleAssignmentNode node)
        {
            node.Value.Visit(this);
            for (int i = 0; i < node.Targets.Count; i++)
                emit(node.Targets[i].SourceLocation, InstructionType.Duplicate);
            foreach (var target in node.Targets)
                Accept(new BinaryOperationNode(target.SourceLocation, BinaryOperation.Assignment, target, new CodeBlockNode(target.SourceLocation)));
        }
        public void Accept(RaiseNode node)
        {
            node.Exception.Visit(this);
            emit(node.SourceLocation, InstructionType.Raise);
        }
        public void Accept(ReturnNode node)
        {
            node.Value.Visit(this);
            emit(node.SourceLocation, InstructionType.Return);
        }
        public void Accept(StringNode node)
        {
            emit(node.SourceLocation, InstructionType.PushObject, new HassiumString(node.String));
        }
        public void Accept(SwitchNode node)
        {
            table.EnterScope();
            var endLabel = nextLabel();
            node.Value.Visit(this);
            for (int i = 0; i < node.Cases.Count; i++)
                emit(node.Value.SourceLocation, InstructionType.Duplicate);

            foreach (var pair in node.Cases)
            {
                var falseLabel = nextLabel();
                pair.Key.Visit(this);
                emit(pair.Key.SourceLocation, InstructionType.BinaryOperation, (int)BinaryOperation.EqualTo);
                emit(pair.Key.SourceLocation, InstructionType.JumpIfFalse, falseLabel);
                pair.Value.Visit(this);
                emit(node.Value.SourceLocation, InstructionType.Jump, endLabel);
                emitLabel(pair.Value.SourceLocation, falseLabel);
            }

            node.Default.Visit(this);

            emitLabel(node.SourceLocation, endLabel);
            table.LeaveScope();
        }
        public void Accept(TernaryOperationNode node)
        {
            var falseLabel = nextLabel();
            var endLabel = nextLabel();

            node.Condition.Visit(this);
            emit(node.Condition.SourceLocation, InstructionType.JumpIfFalse, falseLabel);
            node.TrueExpression.Visit(this);
            emit(node.TrueExpression.SourceLocation, InstructionType.Jump, endLabel);
            emitLabel(node.FalseExpression.SourceLocation, falseLabel);
            node.FalseExpression.Visit(this);
            emitLabel(node.FalseExpression.SourceLocation, endLabel);

        }
        public void Accept(ThreadNode node)
        {
            var method = new HassiumMethod(module);
            methodStack.Push(method);
            method.Parent = classStack.Peek();

            if (node.Body is CodeBlockNode)
                node.Body.VisitChildren(this);
            else
                node.Body.Visit(this);

            methodStack.Pop();
            
            emit(node.SourceLocation, InstructionType.BuildThread, method);
            if (node.DoImmediately)
                emit(node.SourceLocation, InstructionType.StartThread);
        }
        public void Accept(TraitNode node)
        {
            // Traits are PascalCase
            HassiumWarning.EnforceCasing(module, node.SourceLocation, node.Name, HassiumCasingType.Pascal);

            HassiumTrait trait = new HassiumTrait(node.Name);
            trait.IsPrivate = node.IsPrivate;

            foreach (var pair in node.Attributes)
            {
                methodStack.Push(new HassiumMethod(module));
                pair.Value.Visit(this);
                emit(pair.Value.SourceLocation, InstructionType.Return);
                var type = methodStack.Pop();

                HassiumDictionary.DictTypeDef.add(null, trait.Traits, pair.Value.SourceLocation, new HassiumString(pair.Key), type);
            }

            classStack.Peek().AddAttribute(node.Name, trait);
        }
        public void Accept(TryCatchNode node)
        {
            var endLabel = nextLabel();
            var temp = methodStack.Peek();
            methodStack.Push(new HassiumMethod(module, "catch"));
            methodStack.Peek().Parent = classStack.Peek();
            table.EnterScope();
            methodStack.Peek().Parameters.Add(new FunctionParameter(FunctionParameterType.Normal, "value"), table.HandleSymbol("value"));
            node.CatchBody.VisitChildren(this);
            var handler = new HassiumExceptionHandler(temp, methodStack.Peek(), endLabel);
            methodStack.Pop();
            emit(node.SourceLocation, InstructionType.PushHandler, handler);
            node.TryBody.Visit(this);
            emit(node.SourceLocation, InstructionType.PopHandler);
            emitLabel(node.SourceLocation, endLabel);
        }
        public void Accept(TupleNode node)
        {
            foreach (var element in node.Elements)
                element.Visit(this);
            emit(node.SourceLocation, InstructionType.BuildTuple, node.Elements.Count);
        }
        public void Accept(UnaryOperationNode node)
        {
            switch (node.UnaryOperation)
            {
                case UnaryOperation.BitwiseNot:
                    node.Target.Visit(this);
                    emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.BitwiseNot);
                    break;
                case UnaryOperation.LogicalNot:
                    node.Target.Visit(this);
                    emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.LogicalNot);
                    break;
                case UnaryOperation.Negate:
                    node.Target.Visit(this);
                    emit(node.SourceLocation, InstructionType.UnaryOperation, (int)UnaryOperation.Negate);
                    break;
                case UnaryOperation.PostDecrement:
                case UnaryOperation.PostIncrement:
                case UnaryOperation.PreDecrement:
                case UnaryOperation.PreIncrement:
                    if (node.Target is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Target).Identifier;
                        HassiumInstruction loadInstruction, storeInstruction;
                        if (table.ContainsGlobalSymbol(identifier))
                        {
                            loadInstruction = new HassiumInstruction(node.SourceLocation, InstructionType.LoadGlobalVariable, table.GetGlobalSymbol(identifier));
                            storeInstruction = new HassiumInstruction(node.SourceLocation, InstructionType.StoreGlobalVariable, table.GetGlobalSymbol(identifier));
                        }
                        else
                        {
                            loadInstruction = new HassiumInstruction(node.SourceLocation, InstructionType.LoadLocal, table.GetSymbol(identifier));
                            storeInstruction = new HassiumInstruction(node.SourceLocation, InstructionType.StoreLocal, table.GetSymbol(identifier));
                        }
                        methodStack.Peek().Instructions.Add(loadInstruction);
                        if (node.UnaryOperation == UnaryOperation.PostDecrement || node.UnaryOperation == UnaryOperation.PostIncrement)
                            emit(node.SourceLocation, InstructionType.Duplicate);
                        emit(node.SourceLocation, InstructionType.Push, 1);
                        emit(node.SourceLocation, InstructionType.BinaryOperation,
                            node.UnaryOperation == UnaryOperation.PostIncrement || node.UnaryOperation == UnaryOperation.PreIncrement ? (int)BinaryOperation.Addition : (int)BinaryOperation.Subtraction);
                        methodStack.Peek().Instructions.Add(storeInstruction);
                        if (node.UnaryOperation == UnaryOperation.PreDecrement || node.UnaryOperation == UnaryOperation.PreIncrement)
                            methodStack.Peek().Instructions.Add(loadInstruction);
                    }
                    break;
            }
        }
        public void Accept(UseNode node)
        {
            string path = node.Module.Replace(".", "/").Replace("\\", "/");
            HassiumObject mod;
            if (InternalModule.InternalModules.ContainsKey(path))
                mod = InternalModule.InternalModules[path];
            else
                mod = resolveModuleByPath(node.SourceLocation, path);
            classStack.Peek().AddAttribute(path.Contains("/") ? Path.GetFileName(path) : path, mod);
        }
        public void Accept(UseFromNode node)
        {
            string path = node.Module.Replace(".", "/").Replace("\\", "/");
            HassiumObject mod;
            if (InternalModule.InternalModules.ContainsKey(path))
                mod = InternalModule.InternalModules[path];
            else
                mod = resolveModuleByPath(node.SourceLocation, path);

            // Hassium source code imports will contain __global__, this is where
            // we have to look for the desired classes.
            if (mod.ContainsAttribute("__global__"))
            {
                var globalClass = mod.BoundAttributes["__global__"];
                // Copy over the __init__ method into ours
                foreach (var attrib in globalClass.BoundAttributes)
                    if (attrib.Key == "__init__")
                        foreach (var instruction in (attrib.Value as HassiumMethod).Instructions)
                            methodStack.Peek().Instructions.Add(instruction);
                // Import *everything*
                if (node.Class == "*")
                {
                    // use * is bad
                    module.AddWarning(node.SourceLocation, "Importing '*' is bad practice!");

                    foreach (var attrib in globalClass.BoundAttributes)
                    {
                        if (!classStack.Peek().ContainsAttribute(attrib.Key))
                        {
                            var value = attrib.Value.Clone() as HassiumObject;
                            value.Parent = classStack.Peek();
                            classStack.Peek().AddAttribute(attrib.Key, value);
                        }
                    }
                }
                else
                    classStack.Peek().AddAttribute(node.Class, globalClass.BoundAttributes[node.Class]);
            }
            else
            {
                if (node.Class == "*")
                {
                    // use * is bad
                    module.AddWarning(node.SourceLocation, "Importing '*' is bad practice!");
                    foreach (var attrib in mod.BoundAttributes)
                    {
                        if (!classStack.Peek().ContainsAttribute(attrib.Key))
                        {
                            var value = attrib.Value.Clone() as HassiumObject;
                            value.Parent = classStack.Peek();
                            if (!classStack.Peek().ContainsAttribute(attrib.Key))
                                classStack.Peek().AddAttribute(attrib.Key, value);
                        }
                    }
                }
                else
                    classStack.Peek().AddAttribute(node.Class, mod.BoundAttributes[node.Class]);
            }
        }
        public void Accept(WhileNode node)
        {
            var bodyLabel = nextLabel();
            var endLabel = nextLabel();

            int breakLabelCount = methodStack.Peek().BreakLabels.Count;
            int continueLabelCount = methodStack.Peek().ContinueLabels.Count;

            methodStack.Peek().BreakLabels.Push(endLabel);
            methodStack.Peek().ContinueLabels.Push(bodyLabel);

            emitLabel(node.Condition.SourceLocation, bodyLabel);
            node.Condition.Visit(this);
            emit(node.Condition.SourceLocation, InstructionType.JumpIfFalse, endLabel);
            node.Body.Visit(this);
            emit(node.Body.SourceLocation, InstructionType.Jump, bodyLabel);
            emitLabel(node.Body.SourceLocation, endLabel);

            restoreLabels(breakLabelCount, continueLabelCount);
        }

        private void emit(SourceLocation location, InstructionType instructionType, int arg = -1)
        {
            methodStack.Peek().Emit(location, instructionType, arg);
        }
        private void emit(SourceLocation location, InstructionType instructionType, string constant)
        {
            methodStack.Peek().Emit(location, instructionType, constant: constant);
        }
        private void emit(SourceLocation location, InstructionType instructionType, HassiumObject obj)
        {
            methodStack.Peek().Emit(location, instructionType, obj: obj);
        }
        private void emitLabel(SourceLocation location, int label)
        {
            methodStack.Peek().EmitLabel(location, label);
        }

        private static int label = 0;
        private int nextLabel()
        {
            return label++;
        }

        private void restoreLabels(int breakLabelCount, int continueLabelCount)
        {
            while (methodStack.Peek().BreakLabels.Count > breakLabelCount)
                methodStack.Peek().BreakLabels.Pop();
            while (methodStack.Peek().ContinueLabels.Count > continueLabelCount)
                methodStack.Peek().ContinueLabels.Pop();
        }

        private HassiumObject resolveModuleByPath(SourceLocation location, string path)
        {
            string filePath = locateFile(path, ".has");
            if (filePath == string.Empty)
                filePath = locateFile(path, ".dll");
            if (filePath == string.Empty)
                throw new CompilerException(location, "Could not locate file by reference '{0}'!", path);

            return CompileModuleFromFilePath(filePath, suppressWarns);
        }

        private string locateFile(string path, string extension, bool pass = false)
        {
            if (File.Exists(path))
                return path;
            if (File.Exists(path + extension))
                return path + extension;
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                              Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            if (homePath == null)
                return string.Empty;
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
            if (!pass)
                return locateFile(Path.Combine(Program.MasterPath, path), extension, true);
            return string.Empty;
        }
    }
}
