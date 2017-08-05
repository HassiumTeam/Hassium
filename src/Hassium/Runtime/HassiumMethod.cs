using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hassium.Compiler;
using Hassium.Compiler.Emit;
using Hassium.Compiler.Parser;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumMethod : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("func");

        public string Name { get; set; }
        public bool IsConstructor { get { return Name == "new"; } }

        public SourceLocation SourceLocation { get; set; }
        public string SourceRepresentation { get; set; }

        public Stack<int> BreakLabels { get; private set; }
        public Stack<int> ContinueLabels { get; private set; }
        public Dictionary<int, int> Labels { get; private set; }

        public List<HassiumInstruction> Instructions { get; private set; }

        public Dictionary<FunctionParameter, int> Parameters { get; private set; }
        public HassiumMethod ReturnType { get; set; }

        public HassiumMethod()
        {
            BreakLabels = new Stack<int>();
            ContinueLabels = new Stack<int>();
            Instructions = new List<HassiumInstruction>();
            Labels = new Dictionary<int, int>();
            Parameters = new Dictionary<FunctionParameter, int>();

            Name = string.Empty;
            SourceRepresentation = string.Empty;

            AddAttribute(INVOKE, Invoke);
            AddType(TypeDefinition);
        }
        public HassiumMethod(string name)
        {
            BreakLabels = new Stack<int>();
            ContinueLabels = new Stack<int>();
            Instructions = new List<HassiumInstruction>();
            Labels = new Dictionary<int, int>();
            Parameters = new Dictionary<FunctionParameter, int>();
            
            Name = name;
            SourceRepresentation = string.Empty;

            AddAttribute(INVOKE, Invoke);
            AddType(TypeDefinition);
        }

        public void Emit(SourceLocation location, InstructionType instructionType, int argument = 0)
        {
            Instructions.Add(new HassiumInstruction(location, instructionType, argument));
        }
        public void EmitLabel(SourceLocation location, int label)
        {
            Labels.Add(label, Instructions.Count - 1);
        }

        public HassiumObject Invoke(VirtualMachine vm, SourceLocation location, StackFrame.Frame frame)
        {
            vm.StackFrame.Frames.Push(frame);
            return Invoke(vm, location);
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (SourceRepresentation != string.Empty)
                vm.PushCallStack(string.Format("{0}\t{1}", SourceRepresentation, location));
            if (SourceRepresentation != string.Empty || Name == "__init__")
                vm.StackFrame.PushFrame();

            int i = 0;
            foreach (var param in Parameters)
            {
                if (i >= args.Length)
                {
                    vm.RaiseException(HassiumArgumentLengthException._new(vm, location, this, new HassiumInt(Parameters.Count), new HassiumInt(args.Length)));
                    return Null;
                }
                var arg = args[i++];
                if (param.Key.FunctionParameterType == FunctionParameterType.Variadic)
                {
                    if (arg is HassiumList || arg is HassiumTuple)
                        vm.StackFrame.Add(param.Value, arg);
                    else
                    {
                        HassiumList list = new HassiumList(new HassiumObject[0]);
                        for (--i; i < args.Length; i++)
                            list.add(vm, location, args[i]);
                        vm.StackFrame.Add(param.Value, list);
                    }
                    break;
                }
                if (param.Key.FunctionParameterType == FunctionParameterType.Enforced)
                {
                    var enforcedType = vm.ExecuteMethod(param.Key.EnforcedType);
                    if (enforcedType is HassiumTrait)
                    {
                        if (!(enforcedType as HassiumTrait).Is(vm, location, arg).Bool)
                        {
                            vm.RaiseException(HassiumConversionFailedException._new(vm, location, arg, enforcedType));
                            return Null;
                        }
                    }
                    else if (!arg.Types.Contains(enforcedType))
                    {
                        vm.RaiseException(HassiumConversionFailedException._new(vm, location, arg, enforcedType));
                        return Null;
                    }
                }
                vm.StackFrame.Add(param.Value, arg);
            }

            if (i < args.Length)
            {
                vm.RaiseException(HassiumArgumentLengthException._new(vm, location, this, new HassiumInt(Parameters.Count), new HassiumInt(args.Length)));
                return Null;
            }

            if (IsConstructor)
            {
                HassiumClass ret = new HassiumClass(Parent.Name);
                ret.Attributes = CloneDictionary(Parent.Attributes);
                ret.AddType(Parent.TypeDefinition);

                foreach (var inherit in Parent.Inherits)
                {
                    foreach (var attrib in CloneDictionary(vm.ExecuteMethod(inherit).Attributes))
                    {
                        if (!ret.Attributes.ContainsKey(attrib.Key))
                        {
                            attrib.Value.Parent = ret;
                            ret.Attributes.Add(attrib.Key, attrib.Value);
                        }
                    }
                }

                foreach (var type in Parent.Types)
                    ret.AddType(type as HassiumTypeDefinition);
                foreach (var attrib in ret.Attributes.Values)
                    attrib.Parent = ret;
                vm.ExecuteMethod(ret.Attributes["new"] as HassiumMethod);
                vm.PopCallStack();
                vm.StackFrame.PopFrame();
                return ret;
            }
            else
            {
                var ret = vm.ExecuteMethod(this);

                if (ReturnType != null)
                {
                    var enforcedType = vm.ExecuteMethod(ReturnType);
                    enforcedType = enforcedType is HassiumTypeDefinition ? enforcedType : enforcedType.Type();
                    if (!ret.Types.Contains(enforcedType))
                    {
                        vm.RaiseException(HassiumConversionFailedException._new(vm, location, ret, enforcedType));
                        return this;
                    }
                }

                if (Name != "__init__" && Name != string.Empty) vm.StackFrame.PopFrame();
                if (SourceRepresentation != string.Empty && Name != "__init__")
                    vm.PopCallStack();
                return ret;
            }
        }

        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>
 (Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
    }
}
