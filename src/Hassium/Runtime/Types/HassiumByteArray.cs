using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumByteArray : HassiumList
    {
        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "add", new HassiumFunction(add, -1)  },
            { "contains", new HassiumFunction(contains, 1)  },
            { "format", new HassiumFunction(format, 1)  },
            { INDEX, new HassiumFunction(index, 1)  },
            { ITER, new HassiumFunction(iter, 0)  },
            { ITERABLEFULL, new HassiumFunction(iterablefull, 0)  },
            { ITERABLENEXT, new HassiumFunction(iterablenext, 0)  },
            { "length", new HassiumProperty(get_length)  },
            { "remove", new HassiumFunction(remove, 1)  },
            { "removeat", new HassiumFunction(removeat, 1)  },
            { "reverse", new HassiumFunction(reverse, 0)  },
            { STOREINDEX, new HassiumFunction(storeindex, 2)  },
            { "toascii", new HassiumFunction(toascii, 0)  },
            { "tobytearr", new HassiumFunction(tobytearr, 0)  },
            { "tohex", new HassiumFunction(tohex, 0)  },
            { TOLIST, new HassiumFunction(tolist, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },
        };

        public new List<byte> Values { get; private set; }
        
        public HassiumByteArray(byte[] bytes, IEnumerable<HassiumObject> values) : base(values)
        {
            AddType(TypeDefinition);
            Values = bytes.ToList();
        }

        [FunctionAttribute("func add (byte : char) : null")]
        public static new HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            Values.Add((byte)args[0].ToChar(vm, args[0], location).Char);
            return Null;
        }

        [FunctionAttribute("func contains (byte : char) : bool")]
        public static new HassiumBool contains(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumBool(Values.Contains((byte)args[0].ToChar(vm, args[0], location).Char));
        }

        public override HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, args[0], location).Values;
            for (int i = 0; i < Values.Count(); i++)
                if ((byte)list[i].ToChar(vm, args[i], location).Char != Values[i])
                    return False;
            return True;
        }

        [FunctionAttribute("func __equals__ (l : list) : bool")]
        public static new HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var list = args[0].ToList(vm, args[0], location).Values;
            for (int i = 0; i < Values.Count(); i++)
                if ((byte)list[i].ToChar(vm, args[i], location).Char != Values[i])
                    return False;
            return True;
        }

        [FunctionAttribute("func format (fmt : string) : string")]
        public static new HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            StringBuilder sb = new StringBuilder();
            string fmt = args[0].ToString(vm, args[0], location).String;
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(tostring(vm, self, location).String);
            foreach (var b in bytes)
                sb.AppendFormat(fmt, b);

            return new HassiumString(sb.ToString());
        }

        public override HassiumObject Index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count())
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.Attribs[INVOKE].Invoke(vm, location, self, index));
                return Null;
            }
            return new HassiumChar((char)Values[(int)index.Int]);
        }

        [FunctionAttribute("func __index__ (i : int) : object")]
        public static new HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count())
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.Attribs[INVOKE].Invoke(vm, location, self, index));
                return Null;
            }
            return new HassiumChar((char)Values[(int)index.Int]);
        }

        public new int IterIndex = 0;

        public override HassiumObject Iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func __iter__ () : list")]
        public static new HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self;
        }

        public override HassiumObject IterableFull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(IterIndex >= Values.Count());
        }

        [FunctionAttribute("func __iterfull__ () : bool")]
        public static new HassiumObject iterablefull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var arr = (self as HassiumByteArray);
            return new HassiumBool(arr.IterIndex >= arr.Values.Count());
        }

        public override HassiumObject IterableNext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)Values[(self as HassiumByteArray).IterIndex++]);
        }

        [FunctionAttribute("func __iternext__ () : bool")]
        public static new HassiumObject iterablenext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var arr = (self as HassiumByteArray);
            return new HassiumChar((char)arr.Values[arr.IterIndex++]);
        }

        [FunctionAttribute("length { get; }")]
        public static new HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumInt(Values.Count());
        }

        [FunctionAttribute("func remove (byte : char) : null")]
        public static new HassiumNull remove(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var b = (byte)args[0].ToChar(vm, args[0], location).Char;

            if (!Values.Contains(b))
            {
                vm.RaiseException(HassiumKeyNotFoundException.Attribs[INVOKE].Invoke(vm, location, self, args[0]));
                return Null;
            }

            Values.Remove(b);
            return Null;
        }

        [FunctionAttribute("func removeat (index : int) : null")]
        public static new HassiumNull removeat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.Attribs[INVOKE].Invoke(vm, location, self, index));
                return Null;
            }
            Values.Remove(Values[(int)index.Int]);
            return Null;
        }

        [FunctionAttribute("func reverse () : list")]
        public static new HassiumList reverse(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            byte[] list = new byte[Values.Count];
            Values.CopyTo(list);
            list.Reverse();
            return new HassiumByteArray(list, new HassiumObject[0]);
        }

        public override HassiumObject StoreIndex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.Attribs[INVOKE].Invoke(vm, location, self, index));
                return Null;
            }
            Values[(int)index.Int] = (byte)args[1].ToChar(vm, args[1], location).Char;
            return args[1];
        }

        [FunctionAttribute("func __storeindex__ (index : int, byte : char) : object")]
        public static new HassiumObject storeindex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.Attribs[INVOKE].Invoke(vm, location, self, index));
                return Null;
            }
            Values[(int)index.Int] = (byte)args[1].ToChar(vm, args[1], location).Char;
            return args[1];
        }

        [FunctionAttribute("func toascii () : string")]
        public static new HassiumString toascii(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumString(ASCIIEncoding.ASCII.GetString(Values.ToArray()));
        }

        [FunctionAttribute("func tobytearr () : list")]
        public static new HassiumByteArray tobytearr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return self as HassiumByteArray;
        }

        [FunctionAttribute("func tohex () : string")]
        public static new HassiumString tohex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumString(BitConverter.ToString(Values.ToArray()).Replace("-", string.Empty));
        }

        public override HassiumList ToList(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tolist () : list")]
        public static new HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self as HassiumByteArray;
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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

        [FunctionAttribute("func tostring () : string")]
        public static new HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            StringBuilder sb = new StringBuilder();

            sb.Append("[ ");
            foreach (var v in Values)
                sb.AppendFormat("{0}, ", v.ToString());
            if (Values.Count > 0)
                sb.Remove(sb.Length - 2, 2);
            sb.Append(" ]");

            return new HassiumString(sb.ToString());
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || Attribs.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (Attribs[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in Attribs)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
