using System;
using System.Collections.Generic;
using System.Reflection;

using Hassium.Parser;
using Hassium.Runtime.StandardLibrary;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.CodeGen
{
    public class HassiumCompiler : IVisitor
    {
        private SymbolTable table;
        private HassiumModule module;
        private MethodBuilder currentMethod;

        public HassiumModule Compile(AstNode ast, SymbolTable table, string name)
        {
            this.table = table;
            module = new HassiumModule(name);
            module.Attributes.Add("Event", new HassiumEvent());
            module.Attributes.Add("Thread", new HassiumThread());

            foreach (AstNode child in ast.Children)
            {
                if (child is FuncNode)
                {
                    child.Visit(this);
                    module.Attributes.Add(currentMethod.Name, currentMethod);
                }
                else if (child is ClassNode)
                    child.Visit(this);
                else if (child is EnumNode)
                {
                    HassiumObject enumerator = new HassiumObject();
                    for (int i = 0; i < child.Children.Count; i++)
                        enumerator.Attributes.Add(((IdentifierNode)child.Children[i]).Identifier, new HassiumDouble(i));
                    module.Attributes.Add(((EnumNode)child).Name, enumerator);
                }
                else if (child is UseNode)
                {
                    UseNode use = child as UseNode;
                    if (use.Target is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)use.Target).Identifier;
                        foreach (InternalModule internalModule in InternalModule.InternalModules)
                            if (internalModule.Name.ToLower() == identifier.ToLower())
                                foreach (KeyValuePair<string, HassiumObject> attribute in internalModule.Attributes)
                                    module.Attributes.Add(attribute.Key, attribute.Value);
                    }
                    else if (use.Target is StringNode)
                    {
                        string path = ((StringNode)use.Target).String;
                        if (path.EndsWith(".dll"))
                        {
                            foreach (InternalModule internalModule in LoadModulesFromDLL(path))
                            {
                                foreach (KeyValuePair<string, HassiumObject> attribute in internalModule.Attributes)
                                    module.Attributes.Add(attribute.Key, attribute.Value);
                            }
                        }
                        else
                        {
                            HassiumModule compiledModule = HassiumExecuter.FromFilePath(path, false);
                            foreach (KeyValuePair<string, HassiumObject> attribute in compiledModule.Attributes)
                                if (!module.Attributes.ContainsKey(attribute.Key))
                                    module.Attributes.Add(attribute.Key, attribute.Value);
                            foreach (HassiumObject constant in compiledModule.ConstantPool)
                                if (!module.ConstantPool.Contains(constant))
                                    module.ConstantPool.Add(constant);
                        }
                    }
                }
            }
            foreach (AstNode child in ast.Children)
            {
                if (child is ClassNode)
                {
                    ClassNode clazz = child as ClassNode;
                    foreach (string inherit in clazz.Inherits)
                    {
                        Dictionary<string, HassiumObject> inheritedAttributes = MethodBuilder.CloneDictionary(module.Attributes[inherit].Attributes);
                        foreach (KeyValuePair<string, HassiumObject> attribute in inheritedAttributes)
                        {
                            if (!module.Attributes[clazz.Name].Attributes.ContainsKey(attribute.Key))
                            if (attribute.Value is MethodBuilder)
                            {
                                MethodBuilder newMethod = ((MethodBuilder)attribute.Value);
                                newMethod.Parent = module.Attributes[clazz.Name] as HassiumClass;
                                module.Attributes[clazz.Name].Attributes.Add(attribute.Key, newMethod);
                            }
                            if (attribute.Value is UserDefinedProperty)
                            {
                                UserDefinedProperty property = attribute.Value as UserDefinedProperty;
                                property.GetMethod.Parent = module.Attributes[clazz.Name] as HassiumClass;
                                if (property.SetMethod != null)
                                    property.SetMethod.Parent = module.Attributes[clazz.Name] as HassiumClass;
                                module.Attributes[clazz.Name].Attributes.Add(attribute.Key, property);
                            }
                        }
                    }
                }
            }
            return module;
        }

        public void Accept(ArgListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ArrayAccessNode node)
        {
            node.VisitChildren(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Load_List_Element);
        }
        public void Accept(ArrayDeclarationNode node)
        {
            node.VisitChildren(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Create_List, node.Children.Count);
        }
        public void Accept(AttributeAccessNode node)
        {
            node.Left.Visit(this);
            if (!containsConstant(node.Right))
                module.ConstantPool.Add(new HassiumString(node.Right));
            currentMethod.Emit(node.SourceLocation, InstructionType.Load_Attribute, findIndex(node.Right));
        }
        public void Accept(BinaryOperationNode node)
        {
            if (node.BinaryOperation != BinaryOperation.Assignment && node.BinaryOperation != BinaryOperation.Swap)
            {
                node.Left.Visit(this);
                node.Right.Visit(this);
            }
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    node.Right.Visit(this);
                    if (node.Left is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Left).Identifier;
                        if (!table.FindSymbol(identifier))
                            table.AddSymbol(identifier);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, table.GetIndex(identifier));
                        currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, table.GetIndex(identifier));
                    }
                    else if (node.Left is AttributeAccessNode)
                    {
                        AttributeAccessNode accessor = node.Left as AttributeAccessNode;
                        accessor.Left.Visit(this);
                        if (!containsConstant(accessor.Right))
                            module.ConstantPool.Add(new HassiumString(accessor.Right));
                        currentMethod.Emit(node.SourceLocation, InstructionType.Store_Attribute, findIndex(accessor.Right));
                        accessor.Left.Visit(this);
                    }
                    else if (node.Left is ArrayAccessNode)
                    {
                        ArrayAccessNode access = node.Left as ArrayAccessNode;
                        access.Target.Visit(this);
                        access.Expression.Visit(this);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Store_List_Element);
                    }
                    break;
                case BinaryOperation.Addition:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 0);
                    break;
                case BinaryOperation.Subtraction:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 1);
                    break;
                case BinaryOperation.Multiplication:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 2);
                    break;
                case BinaryOperation.Division:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 3);
                    break;
                case BinaryOperation.Modulus:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 4);
                    break;
                case BinaryOperation.XOR:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 5);
                    break;
                case BinaryOperation.OR:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 6);
                    break;
                case BinaryOperation.XAnd:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 7);
                    break;
                case BinaryOperation.EqualTo:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 8);
                    break;
                case BinaryOperation.NotEqualTo:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 9);
                    break;
                case BinaryOperation.GreaterThan:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 10);
                    break;
                case BinaryOperation.GreaterThanOrEqual:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 11);
                    break;
                case BinaryOperation.LesserThan:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 12);
                    break;
                case BinaryOperation.LesserThanOrEqual:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 13);
                    break;
                case BinaryOperation.LogicalOr:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 14);
                    break;
                case BinaryOperation.LogicalAnd:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 15);
                    break;
                case BinaryOperation.Swap:
                    table.AddSymbol("__swaptmp__");
                    int tmp = table.GetIndex("__swaptmp__");
                    int left = table.GetIndex(((IdentifierNode)node.Left).Identifier);
                    int right = table.GetIndex(((IdentifierNode)node.Right).Identifier);
                    node.Left.Visit(this);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, tmp);
                    node.Right.Visit(this);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, left);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, tmp);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, right);
                    break;
                case BinaryOperation.Power:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 16);
                    break;
                case BinaryOperation.Root:
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 17);
                    break;
            }
        }
        public void Accept(BoolNode node)
        {
            currentMethod.Emit(node.SourceLocation, InstructionType.Push_Bool, node.Value ? 1 : 0);
        }
        public void Accept(BreakNode node)
        {
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, currentMethod.BreakLabels.Pop());
        }
        public void Accept(CaseNode node)
        {
        }
        public void Accept(CharNode node)
        {
            if (!containsConstant(node.Char.ToString()))
                module.ConstantPool.Add(new HassiumChar(node.Char));
            currentMethod.Emit(node.SourceLocation, InstructionType.Push_Object, findIndex(node.Char.ToString()));
        }
        public void Accept(ClassNode node)
        {
            if (!containsConstant(node.Name))
                module.ConstantPool.Add(new HassiumString(node.Name));
            HassiumClass clazz = new HassiumClass();
            foreach (AstNode child in node.Body.Children)
            {
                child.Visit(this);
                if (child is FuncNode)
                {
                    currentMethod.Parent = clazz;
                    clazz.Attributes.Add(currentMethod.Name, currentMethod);
                }
                if (child is PropertyNode)
                {
                    PropertyNode propNode = child as PropertyNode;
                    if (!containsConstant(propNode.Identifier))
                        module.ConstantPool.Add(new HassiumString(propNode.Identifier));
                    UserDefinedProperty property = new UserDefinedProperty(propNode.Identifier);
                    currentMethod = new MethodBuilder();
                    currentMethod.Name =  "__get__" + propNode.Identifier;
                    currentMethod.Parent = clazz;
                    table.EnterScope();
                    propNode.GetBody.Visit(this);
                    table.PopScope();
                    property.GetMethod = currentMethod;

                    if (node.Children.Count > 2)
                    {
                        currentMethod = new MethodBuilder();
                        currentMethod.Name = "__set__" + propNode.Identifier;
                        currentMethod.Parent = clazz;
                        table.EnterScope();
                        if (!table.FindSymbol("value"))
                            table.AddSymbol("value");
                        currentMethod.Parameters.Add("value", table.GetIndex("value"));
                        propNode.SetBody.Visit(this);
                        table.PopScope();
                        property.SetMethod = currentMethod;
                    }
                    clazz.Attributes.Add(propNode.Identifier, property);
                }
            }
            module.Attributes.Add(node.Name, clazz);
        }
        public void Accept(CodeBlockNode node)
        {
            table.EnterScope();
            node.VisitChildren(this);
            table.PopScope();
        }
        public void Accept(ConditionalNode node)
        {
            double elseLabel = generateSymbol();
            double endLabel = generateSymbol();
            node.Predicate.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_False, elseLabel);
            node.Body.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, endLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, elseLabel);
            if (node.Children.Count > 2)
                node.ElseBody.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, endLabel);
        }
        public void Accept(ContinueNode node)
        {
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, currentMethod.ContinueLabels.Pop());
        }
        public void Accept(DoubleNode node)
        {
            currentMethod.Emit(node.SourceLocation, InstructionType.Push, node.Number);
        }
        public void Accept(EnumNode node)
        {
        }
        public void Accept(ExpressionNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ExpressionStatementNode node)
        {
            node.VisitChildren(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Pop);
        }
        public void Accept(ForNode node)
        {
            double forLabel = generateSymbol();
            double endLabel = generateSymbol();
            currentMethod.ContinueLabels.Push(forLabel);
            currentMethod.BreakLabels.Push(endLabel);
            node.SingleStatement.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, forLabel);
            node.Predicate.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_False, endLabel);
            node.Body.Visit(this);
            node.RepeatStatement.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, forLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, endLabel);
        }
        public void Accept(ForeachNode node)
        {
            double foreachLabel = generateSymbol();
            double endLabel = generateSymbol();
            currentMethod.ContinueLabels.Push(foreachLabel);
            currentMethod.BreakLabels.Push(endLabel);
            table.EnterScope();
            table.AddSymbol(node.Identifier);
            table.AddSymbol("__tmp__");
            int tmp = table.GetIndex("__tmp__");
            node.Expression.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, tmp);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, foreachLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, tmp);
            currentMethod.Emit(node.SourceLocation, InstructionType.Enumerable_Full);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_True, endLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, tmp);
            currentMethod.Emit(node.SourceLocation, InstructionType.Enumerable_Next);
            currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, table.GetIndex(node.Identifier));
            node.Body.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, foreachLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, endLabel);
            table.PopScope();
        }
        public void Accept(FuncNode node)
        {
            if (!containsConstant(node.Name))
                module.ConstantPool.Add(new HassiumString(node.Name));

            currentMethod = new MethodBuilder();
            currentMethod.Name = node.Name;
            currentMethod.SourceRepresentation = node.SourceRepresentation;

            table.EnterScope();

            for (int i = 0; i < node.Parameters.Count; i++)
            {
                table.AddSymbol(node.Parameters[i]);
                currentMethod.Parameters.Add(node.Parameters[i], table.GetIndex(node.Parameters[i]));
            }

            node.Children[0].VisitChildren(this);
            table.PopScope();
        }
        public void Accept(FunctionCallNode node)
        {
            for (int i = node.Arguments.Children.Count - 1; i >= 0; i--)
                node.Arguments.Children[i].Visit(this);
            node.Target.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Call, node.Arguments.Children.Count);
        }
        public void Accept(IdentifierNode node)
        {
            if (!table.FindSymbol(node.Identifier))
            {
                if (!containsConstant(node.Identifier))
                    module.ConstantPool.Add(new HassiumString(node.Identifier));
                currentMethod.Emit(node.SourceLocation, InstructionType.Load_Global, findIndex(node.Identifier));
            }
            else
                currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, table.GetIndex(node.Identifier));
        }
        public void Accept(Int64Node node)
        {
            if (!containsConstant(node.Number.ToString()))
                module.ConstantPool.Add(new HassiumInt(node.Number));
            currentMethod.Emit(node.SourceLocation, InstructionType.Push_Object, findIndex(node.Number.ToString()));
        }
        public void Accept(KeyValuePairNode node)
        {
            node.Left.Visit(this);
            node.Right.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Key_Value_Pair);
        }
        public void Accept(LambdaNode node)
        {
            MethodBuilder previousMethod = currentMethod;
            currentMethod = new MethodBuilder();
            currentMethod.Name = "__lambda__";
            table.EnterScope();

            for (int i = 0; i < node.Parameters.Count; i++)
            {
                table.AddSymbol(node.Parameters[i]);
                currentMethod.Parameters.Add(node.Parameters[i], table.GetIndex(node.Parameters[i]));
            }

            node.Children[0].VisitChildren(this);
            table.PopScope();
            // Swap from the lambda method to the current method
            MethodBuilder lambda = currentMethod;
            currentMethod = previousMethod;
            module.ConstantPool.Add(lambda);

            currentMethod.Emit(node.SourceLocation, InstructionType.Push_Object, findIndex(lambda));
            currentMethod.Emit(node.SourceLocation, InstructionType.Build_Closure);
        }
        public void Accept(NewNode node)
        {
            node.Call.IsConstructorCall = true;
            node.Call.Visit(this);
        }
        public void Accept(PropertyNode node)
        {
        }
        public void Accept(ReturnNode node)
        {
            node.VisitChildren(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Return);
        }
        public void Accept(StatementNode node)
        {
        }
        public void Accept(StringNode node)
        {
            if (!containsConstant(node.String))
                module.ConstantPool.Add(new HassiumString(node.String));
            currentMethod.Emit(node.SourceLocation, InstructionType.Push_Object, findIndex(node.String));
        }
        public void Accept(SwitchNode node)
        {
            node.Predicate.Visit(this);
            table.EnterScope();
            table.AddSymbol("__tmp__");
            int tmp = table.GetIndex("__tmp__");
            currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, tmp);
            foreach (AstNode child in node.Children)
            {
                CaseNode cas = child as CaseNode;
                double caseLabel = generateSymbol();
                double endCase = generateSymbol();
                currentMethod.BreakLabels.Push(endCase);
                foreach (AstNode predicate in cas.Children)
                {
                    currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, tmp);
                    predicate.Visit(this);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 8);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_True, caseLabel);
                }
                currentMethod.Emit(node.SourceLocation, InstructionType.Jump, endCase);
                currentMethod.Emit(node.SourceLocation, InstructionType.Label, caseLabel);
                cas.Body.Visit(this);
                currentMethod.Emit(node.SourceLocation, InstructionType.Label, endCase);
            }
        }
        public void Accept(TerenaryOperationNode node)
        {
            double falseLabel = generateSymbol();
            double endLabel = generateSymbol();
            node.Predicate.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_False, falseLabel);
            node.TrueBody.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, endLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, falseLabel);
            node.ElseBody.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, endLabel);
        }
        public void Accept(ThisNode node)
        {
            currentMethod.Emit(node.SourceLocation, InstructionType.Self_Reference, findIndex(currentMethod.Name));
        }
        public void Accept(TupleNode node)
        {
            node.VisitChildren(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Create_Tuple, node.Children.Count);
        }
        public void Accept(UnaryOperationNode node)
        {
            switch (node.UnaryOperation)
            {
                case UnaryOperation.Not:
                    node.Body.Visit(this);
                    currentMethod.Emit(node.SourceLocation, InstructionType.Unary_Operation, 0);
                    break;
                case UnaryOperation.PostDecrement:
                case UnaryOperation.PostIncrement:
                case UnaryOperation.PreDecrement:
                case UnaryOperation.PreIncrement:
                    if (node.Body is IdentifierNode)
                    {
                        string identifier = ((IdentifierNode)node.Body).Identifier;
                        if (!table.FindSymbol(identifier))
                            table.AddSymbol(identifier);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, table.GetIndex(identifier));
                        if (node.UnaryOperation == UnaryOperation.PostDecrement || node.UnaryOperation == UnaryOperation.PostIncrement)
                            currentMethod.Emit(node.SourceLocation, InstructionType.Dup);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Push, 1);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Binary_Operation, 
                                           (node.UnaryOperation == UnaryOperation.PostIncrement || node.UnaryOperation == UnaryOperation.PreIncrement) ? 0 : 1);
                        currentMethod.Emit(node.SourceLocation, InstructionType.Store_Local, table.GetIndex(identifier));
                        if (node.UnaryOperation == UnaryOperation.PreDecrement || node.UnaryOperation == UnaryOperation.PreIncrement)
                            currentMethod.Emit(node.SourceLocation, InstructionType.Load_Local, table.GetIndex(identifier));
                    }
                    break;
            }
        }
        public void Accept(UseNode node)
        {
        }
        public void Accept(WhileNode node)
        {
            double whileLabel = generateSymbol();
            double endLabel = generateSymbol();
            currentMethod.ContinueLabels.Push(whileLabel);
            currentMethod.BreakLabels.Push(endLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, whileLabel);
            node.Predicate.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump_If_False, endLabel);
            node.Body.Visit(this);
            currentMethod.Emit(node.SourceLocation, InstructionType.Jump, whileLabel);
            currentMethod.Emit(node.SourceLocation, InstructionType.Label, endLabel);
        }

        private int findIndex(HassiumObject constant)
        {
            for (int i = 0; i < module.ConstantPool.Count; i++)
                if (module.ConstantPool[i] == constant)
                    return i;
            return -1;
        }
        private int findIndex(string constant)
        {
            for (int i = 0; i < module.ConstantPool.Count; i++)
                if (module.ConstantPool[i].ToString(null) == constant)
                    return i;
            return -1;
        }

        private bool containsConstant(string constant)
        {
            foreach (HassiumObject obj in module.ConstantPool)
                if (obj.ToString(null) == constant)
                    return true;
            return false;
        }

        private double nextSymbol = 0;
        private double generateSymbol()
        {
            return ++nextSymbol;
        }

        public static InternalModule[] LoadModulesFromDLL(string path)
        {
            List<InternalModule> modules = new List<InternalModule>();
            Assembly ass = Assembly.LoadFrom(path);
            foreach (var type in ass.GetTypes())
            {
                if (type.IsSubclassOf(typeof(InternalModule)))
                    modules.Add((InternalModule)Activator.CreateInstance(type));
            }
            return modules.ToArray();
        }
    }
}