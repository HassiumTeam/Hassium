using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumByteArray : HassiumList
    {
        public new List<byte> Values { get; private set; }

        public HassiumByteArray(byte[] bytes, IEnumerable<HassiumObject> values) : base(values)
        {
            AddType(TypeDefinition);
            Values = bytes.ToList();
            Attributes.Clear();

            AddAttribute("add", add, -1);
            AddAttribute("contains", contains, 1);
            AddAttribute(INDEX, Index, 1);
            AddAttribute(ITER, Iter, 0);
            AddAttribute(ITERABLEFULL, IterableFull, 0);
            AddAttribute(ITERABLENEXT, IterableNext, 0);
            AddAttribute("length", new HassiumProperty(get_length));
            AddAttribute("remove", remove, 1);
            AddAttribute("removeat", removeat, 1);
            AddAttribute("reverse", reverse, 0);
            AddAttribute(STOREINDEX, StoreIndex, 2);
            AddAttribute("toByteArr", toByteArr, 0);
            AddAttribute(TOLIST, ToList, 0);
            AddAttribute(TOSTRING, ToString, 0);
        }

        [FunctionAttribute("func add (byte : char) : null")]
        public new HassiumNull add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Values.Add((byte)args[0].ToChar(vm, location).Char);
            return Null;
        }

        [FunctionAttribute("func contains (byte : char) : bool")]
        public new HassiumBool contains(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Values.Contains((byte)args[0].ToChar(vm, location).Char));
        }

        [FunctionAttribute("func __equals__ (l : list) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, location).Values;
            for (int i = 0; i < Values.Count(); i++)
                if ((byte)list[i].ToChar(vm, location).Char != Values[i])
                    return False;
            return True;
        }

        [FunctionAttribute("func __index__ (i : int) : object")]
        public override HassiumObject Index(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, location);
            if (index.Int < 0 || index.Int >= Values.Count())
            {
                vm.RaiseException(HassiumIndexOutOfRangeException._new(vm, location, this, index));
                return Null;
            }
            return new HassiumChar((char)Values[(int)index.Int]);
        }

        private int iterIndex = 0;

        [FunctionAttribute("func __iter__ () : list")]
        public override HassiumObject Iter(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func __iterfull__ () : bool")]
        public override HassiumObject IterableFull(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(iterIndex >= Values.Count());
        }

        [FunctionAttribute("func __iternext__ () : bool")]
        public override HassiumObject IterableNext(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)Values[iterIndex++]);
        }

        [FunctionAttribute("length { get; }")]
        public new HassiumInt get_length(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Values.Count());
        }

        [FunctionAttribute("func remove (byte : char) : null")]
        public new HassiumNull remove(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var b = (byte)args[0].ToChar(vm, location).Char;
            
            if (!Values.Contains(b))
            {
                vm.RaiseException(HassiumKeyNotFoundException._new(vm, location, this, args[0]));
                return Null;
            }

            Values.Remove(b);
            return Null;
        }

        [FunctionAttribute("func removeat (index : int) : null")]
        public new HassiumNull removeat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException._new(vm, location, this, index));
                return Null;
            }
            Values.Remove(Values[(int)index.Int]);
            return Null;
        }

        [FunctionAttribute("func reverse () : list")]
        public new HassiumList reverse(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] list = new byte[Values.Count];
            Values.CopyTo(list);
            list.Reverse();
            return new HassiumByteArray(list, new HassiumObject[0]);
        }

        [FunctionAttribute("func __storeindex__ (index : int, byte : char) : object")]
        public override HassiumObject StoreIndex(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException._new(vm, location, this, index));
                return Null;
            }
            Values[(int)index.Int] = (byte)args[1].ToChar(vm, location).Char;
            return args[1];
        }

        [FunctionAttribute("func toByteArr () : list")]
        public HassiumByteArray toByteArr(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tolist () : list")]
        public override HassiumList ToList(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[ ");
            foreach (var v in Values)
                sb.AppendFormat("{0}, ", v.ToString());
            if (Values.Count > 0)
                sb.Remove(sb.Length - 2, 2);
            sb.Append(" ]");

            return new HassiumString(sb.ToString());
        }
    }
}
