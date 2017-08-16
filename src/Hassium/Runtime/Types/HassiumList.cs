using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumList : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("list");

        public List<HassiumObject> Values { get; private set; }

        public HassiumList(IEnumerable<HassiumObject> values)
        {
            AddType(TypeDefinition);
            Values = values.ToList();

            AddAttribute("add", add, -1);
            AddAttribute("contains", contains, 1);
            AddAttribute("fill", fill, 2);
            AddAttribute("format", format, 1);
            AddAttribute(INDEX, Index, 1);
            AddAttribute(ITER, Iter, 0);
            AddAttribute(ITERABLEFULL, IterableFull, 0);
            AddAttribute(ITERABLENEXT, IterableNext, 0);
            AddAttribute("length", new HassiumProperty(get_length));
            AddAttribute("peek", peek, 0);
            AddAttribute("pop", pop, 0);
            AddAttribute("push", push, 1);
            AddAttribute("remove", remove, 1);
            AddAttribute("removeat", removeat, 1);
            AddAttribute("reverse", reverse, 0);
            AddAttribute(STOREINDEX, StoreIndex, 2);
            AddAttribute("toascii", toascii, 0);
            AddAttribute("tobytearr", tobytearr, 0);
            AddAttribute("tohex", tohex, 0);
            AddAttribute(TOLIST, ToList, 0);
            AddAttribute(TOSTRING, ToString, 0);
        }

        [FunctionAttribute("func add (params args) : null")]
        public HassiumNull add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Values.Add(arg);

            return Null;
        }

        [FunctionAttribute("func contains (obj : object) : bool")]
        public HassiumBool contains(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var value in Values)
                if (value == args[0] || args[0].EqualTo(vm, location, args[0]).Bool)
                    return True;
            return False;
        }

        [FunctionAttribute("func __equals__ (l : list) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, location).Values;
            if (list.Count != Values.Count)
                return False;

            for (int i = 0; i < Values.Count; i++)
                if (!contains(vm, location, list[i]).Bool)
                    return False;

            return True;
        }

        [FunctionAttribute("func fill (count : int, val : object) : list")]
        public HassiumList fill(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            int count = (int)args[0].ToInt(vm, location).Int;
            for (int i = 0; i < count; i++)
                list.add(vm, location, args[1]);

            return list;
        }

        [FunctionAttribute("func format (fmt : string) : string")]
        public HassiumString format(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            string fmt = args[0].ToString(vm, location).String;
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(ToString(vm, location).String);
            foreach (var b in bytes)
                sb.AppendFormat(fmt, b);

            return new HassiumString(sb.ToString());
        }

        [FunctionAttribute("func __index__ (i : int) : object")]
        public override HassiumObject Index(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException._new(vm, location, this, index));
                return Null;
            }
            return Values[(int)index.Int];
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
            return new HassiumBool(iterIndex >= Values.Count);
        }

        [FunctionAttribute("func __iternext__ () : bool")]
        public override HassiumObject IterableNext(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Values[iterIndex++];
        }

        [FunctionAttribute("length { get; }")]
        public HassiumInt get_length(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Values.Count);
        }

        [FunctionAttribute("func peek () : object")]
        public HassiumObject peek(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Values[Values.Count - 1];
        }

        [FunctionAttribute("func pop () : object")]
        public HassiumObject pop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            try
            {
                return Values[Values.Count - 1];
            }
            finally
            {
                Values.Remove(Values[Values.Count - 1]);
            }
        }

        [FunctionAttribute("func push (obj : object) : null")]
        public HassiumNull push(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Values.Add(args[0]);
            return Null;
        }

        [FunctionAttribute("func remove (obj : object) : null")]
        public HassiumNull remove(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var value in Values)
            {
                if (value == args[0] || args[0].EqualTo(vm, location, args[0]).Bool)
                {
                    Values.Remove(value);
                    return Null;
                }
            }
            return Null;
        }

        [FunctionAttribute("func removeat (index : int) : null")]
        public HassiumNull removeat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
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
        public HassiumList reverse(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumObject[] list = new HassiumObject[Values.Count];
            Values.CopyTo(list);
            list.Reverse();
            return new HassiumList(list);
        }

        [FunctionAttribute("func __storeindex__ (index : int, obj : object) : object")]
        public override HassiumObject StoreIndex(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException._new(vm, location, this, index));
                return Null;
            }
            Values[(int)index.Int] = args[1];
            return args[1];
        }

        [FunctionAttribute("func toascii () : string")]
        public HassiumString toascii(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] bytes = new byte[Values.Count];
            for (int i = 0; i < Values.Count; i++)
                bytes[i] = (byte)Values[i].ToChar(vm, location).Char;
            return new HassiumString(ASCIIEncoding.ASCII.GetString(bytes));
        }

        [FunctionAttribute("func tobigint () : BigInt")]
        public override HassiumBigInt ToBigInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return HassiumBigInt.ImportAttribs(new HassiumBigInt() { BigInt = new BigInteger(ListToByteArr(vm, location, this)) });
        }

        [FunctionAttribute("func tobytearr () : list")]
        public HassiumByteArray tobytearr(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var arr = new HassiumByteArray(new byte[0], new HassiumObject[0]);

            foreach (var val in Values)
                arr.Values.Add((byte)val.ToChar(vm, location).Char);

            return arr;
        }

        [FunctionAttribute("func tohex () : string")]
        public HassiumString tohex(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] bytes = new byte[Values.Count];
            for (int i = 0; i < Values.Count; i++)
                bytes[i] = (byte)Values[i].ToChar(vm, location).Char;
            return new HassiumString(BitConverter.ToString(bytes).Replace("-", string.Empty));
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
                sb.AppendFormat("{0}, ", v.ToString(vm, location).String);
            if (Values.Count > 0)
                sb.Remove(sb.Length - 2, 2);
            sb.Append(" ]");

            return new HassiumString(sb.ToString());
        }
    }
}
