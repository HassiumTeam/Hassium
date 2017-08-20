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

        public Stack<int> BreakLabels { get; private set; }
        public Stack<int> ContinueLabels { get; private set; }

        public List<HassiumInstruction> Instructions { get; private set; }

        public bool IsConstructor { get { return Name == "new"; } }

        public Dictionary<int, int> Labels { get; private set; }

        public HassiumModule Module { get; set; }
        public string Name { get; set; }

        public Dictionary<FunctionParameter, int> Parameters { get; private set; }
        public HassiumMethod ReturnType { get; set; }

        public SourceLocation SourceLocation { get; set; }
        public string SourceRepresentation { get; set; }
        
        public HassiumMethod(HassiumModule module)
        {
            BreakLabels = new Stack<int>();
            ContinueLabels = new Stack<int>();
            Instructions = new List<HassiumInstruction>();
            Labels = new Dictionary<int, int>();
            Parameters = new Dictionary<FunctionParameter, int>();

            Module = module;
            Name = string.Empty;
            SourceRepresentation = string.Empty;
            
            AddType(TypeDefinition);
        }
        public HassiumMethod(HassiumModule module, string name)
        {
            BreakLabels = new Stack<int>();
            ContinueLabels = new Stack<int>();
            Instructions = new List<HassiumInstruction>();
            Labels = new Dictionary<int, int>();
            Parameters = new Dictionary<FunctionParameter, int>();

            Module = module;
            Name = name;
            SourceRepresentation = string.Empty;
            
            AddType(TypeDefinition);
        }

        public void Emit(SourceLocation location, InstructionType instructionType, int argument = -1, string constant = "", HassiumObject obj = null)
        {
            Instructions.Add(new HassiumInstruction(location, instructionType, argument, constant, obj));
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
                vm.PushCallStack(string.Format("{0}\t[{1}]", SourceRepresentation, location));
            if (SourceRepresentation != string.Empty || Name == "__init__")
                vm.StackFrame.PushFrame();

            int i = 0;
            foreach (var param in Parameters)
            {
                // If there's more arguments provided than called for
                if (i >= args.Length)
                {
                    vm.RaiseException(HassiumArgLengthException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumInt(Parameters.Count), new HassiumInt(args.Length)));
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
                            HassiumList.add(vm, list, location, args[i]);
                        vm.StackFrame.Add(param.Value, list);
                    }
                    break;
                }
                else if (param.Key.FunctionParameterType == FunctionParameterType.Enforced)
                {
                    var enforcedType = vm.ExecuteMethod(param.Key.EnforcedType);
                    if (enforcedType is HassiumTrait)
                    {
                        if (!(enforcedType as HassiumTrait).Is(vm, location, arg).Bool)
                        {
                            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, arg, enforcedType));
                            return Null;
                        }
                    }
                    else if (!arg.Types.Contains(enforcedType))
                    {
                        vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, arg, enforcedType));
                        return Null;
                    }
                }
                vm.StackFrame.Add(param.Value, arg);
            }

            // If there's less arguments than called for
            if (i < args.Length)
            {
                vm.RaiseException(HassiumArgLengthException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumInt(Parameters.Count), new HassiumInt(args.Length)));
                return Null;
            }

            if (IsConstructor)
            {
                HassiumClass clazz = new HassiumClass(Parent.Name);
                clazz.BoundAttributes = CloneDictionary(Parent.BoundAttributes);
                clazz.AddType(Parent.TypeDefinition);

                foreach (var inherit in Parent.Inherits)
                {
                    foreach (var attrib in CloneDictionary(vm.ExecuteMethod(inherit).GetAttributes()))
                    {
                        if (!clazz.ContainsAttribute(attrib.Key))
                        {
                            attrib.Value.Parent = clazz;
                            clazz.BoundAttributes.Add(attrib.Key, attrib.Value);
                        }
                    }
                }

                foreach (var type in Parent.Types)
                    clazz.AddType(type as HassiumTypeDefinition);
                foreach (var attrib in clazz.BoundAttributes.Values)
                    attrib.Parent = clazz;
                vm.ExecuteMethod(clazz.BoundAttributes["new"] as HassiumMethod);
                vm.PopCallStack();
                vm.StackFrame.PopFrame();
                return clazz;
            }

            var ret = vm.ExecuteMethod(this);

            if (ReturnType != null)
            {
                var enforcedType = vm.ExecuteMethod(ReturnType);
                enforcedType = enforcedType is HassiumTypeDefinition ? enforcedType : enforcedType.Type();
                if (!ret.Types.Contains(enforcedType))
                {
                    vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, ret, enforcedType));
                    return this;
                }
            }

            if (Name != "__init__" && Name != string.Empty) vm.StackFrame.PopFrame();
            if (SourceRepresentation != string.Empty && Name != "__init__")
                vm.PopCallStack();
            return ret;
        }

        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>
 (Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);

            var pairs = original;
            var keys = original.Keys.ToArray();
            for (int i = 0; i < original.Count; i++)
            {
                ret.Add(keys[i], (TValue)original[keys[i]]);
            }
            return ret;
        }
    }
}
