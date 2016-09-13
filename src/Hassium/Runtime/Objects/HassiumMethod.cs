using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Compiler;
using Hassium.Compiler.CodeGen;
using Hassium.Compiler.Parser.Ast;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects
{
    public class HassiumMethod: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("func");

        public Stack<int> BreakLabels { get; private set; }
        public Stack<int> ContinueLabels { get; private set; }
        public List<Instruction> Instructions { get; private set; }
        public bool IsConstructor { get { return Name == "new"; } }
        public Dictionary<int, int> Labels { get; private set; }
        public string Name { get; set; }
        public Dictionary<FuncParameter, int> Parameters { get; private set; }
        public string ReturnType { get; set; }
        public string SourceRepresentation { get; set; }

        public HassiumMethod()
        {
            AddType(TypeDefinition);
            AddAttribute("parameterLengths", new HassiumProperty(get_parameterLengths));

            BreakLabels = new Stack<int>();
            ContinueLabels = new Stack<int>();
            Instructions = new List<Instruction>();
            Labels = new Dictionary<int, int>();
            Parameters = new Dictionary<FuncParameter, int>();
        }

        public HassiumList get_parameterLengths(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumList(new HassiumObject[] { new HassiumInt(Parameters.Count) });
        }

        public void Emit(SourceLocation location, InstructionType instructionType, int argument = 0)
        {
            Instructions.Add(new Instruction(location, instructionType, argument));
        }
        public void EmitLabel(SourceLocation location, int label)
        {
            Labels.Add(label, Instructions.Count - 1);
           // Instructions.Add(new Instruction(location, InstructionType.Label, label));
        }

        public HassiumObject Invoke(VirtualMachine vm, StackFrame.Frame frame)
        {
            vm.StackFrame.Frames.Push(frame);
            return Invoke(vm);
        }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            try
            {
                if (Name != "lambda" && Name != "catch" && Name != "thread") vm.StackFrame.PushFrame();
                vm.CallStack.Push(SourceRepresentation);
                int i = 0;
                foreach (var param in Parameters)
                {
                    var arg = args[i++];
                    if (param.Key.IsVariadic)
                    {
                        if (arg is HassiumList || arg is HassiumTuple)
                            vm.StackFrame.Add(param.Value, arg);
                        else
                        {
                            HassiumList list = new HassiumList(new HassiumObject[0]);
                            for (; i < args.Length; i++)
                                list.add(vm, args[i]);
                            vm.StackFrame.Add(param.Value, list);
                        }
                        break;
                    }
                    if (param.Key.IsEnforced)
                    if (!arg.Types.Contains((HassiumTypeDefinition)vm.Globals[param.Key.Type].Type()))
                        throw new InternalException(vm, InternalException.PARAMETER_ERROR, param.Key.Type, arg.Type());
                    vm.StackFrame.Add(param.Value, arg);
                }
                if (IsConstructor)
                {
                    HassiumClass ret = new HassiumClass();
                    ret.Attributes = CloneDictionary(Parent.Attributes);
                    foreach (var type in Parent.Types)
                        ret.AddType(type);
                    foreach (var attrib in ret.Attributes.Values)
                        attrib.Parent = ret;
                    vm.ExecuteMethod(ret.Attributes["new"] as HassiumMethod);
                    vm.StackFrame.PopFrame();
                    vm.CallStack.Pop();
                    return ret;
                }
                else
                {
                    var val = vm.ExecuteMethod(this);
                    if (Name == "catch")
                    {
                        vm.CallStack.Pop();
                        return val;
                    }
                    if (ReturnType != "" && ReturnType != null)
                    if (val.Type() != vm.Globals[ReturnType])
                        throw new InternalException(vm, InternalException.RETURN_ERROR, ReturnType, val.Type());
                    if (Name != "lambda") vm.StackFrame.PopFrame();
                    vm.CallStack.Pop();
                    return val;
                }
            }
            catch (InternalException ex)
            {
                Console.WriteLine("At location {0}:", ex.VM.CurrentSourceLocation);
                Console.WriteLine("{0} at:", ex.Message);
                while (ex.VM.CallStack.Count > 0)
                    Console.WriteLine(ex.VM.CallStack.Pop());
                Environment.Exit(0);
                return null;
            }
        }

        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>
        (Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue) entry.Value.Clone());
            }
            return ret;
        }
    }
}