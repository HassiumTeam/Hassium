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

        [DocStr(
            "@desc Adds a char to this byte array.",
            "@param byte The char to add.",
            "@returns null."
            )]
        [FunctionAttribute("func add (byte : char) : null")]
        public static new HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            Values.Add((byte)args[0].ToChar(vm, args[0], location).Char);
            return Null;
        }

        [DocStr(
            "@desc Returns a boolean indicating if the byte array contains the specified byte.",
            "@param byte The byte to check.",
            "@returns true if the byte array contains the byte, otherwise false."
            )]
        [FunctionAttribute("func contains (byte : char) : bool")]
        public static HassiumBool contains(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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

        [DocStr(
            "@desc Implements the == operator to determine if this byte array is equal to the specified list.",
            "@param l The list to compare.",
            "@returns true if the lists are equal, otherwise false."
            )]
        [FunctionAttribute("func __equals__ (l : list) : bool")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var list = args[0].ToList(vm, args[0], location).Values;
            for (int i = 0; i < Values.Count(); i++)
                if ((byte)list[i].ToChar(vm, args[i], location).Char != Values[i])
                    return False;
            return True;
        }

        [DocStr(
            "@desc Formats this byte array using the specified format string and returns it.",
            "@param fmt The format string.",
            "@returns The formatted list string."
            )]
        [FunctionAttribute("func format (fmt : string) : string")]
        public static HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            return new HassiumChar((char)Values[(int)index.Int]);
        }

        [DocStr(
            "@desc Implements the [] operator to retrieve the object at the specified index.",
            "@param i The zero-based index.",
            "@returns The object at the specified index."
            )]
        [FunctionAttribute("func __index__ (i : int) : object")]
        public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count())
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            return new HassiumChar((char)Values[(int)index.Int]);
        }

        public new int IterIndex = 0;

        public override HassiumObject Iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            (self as HassiumByteArray).IterIndex = 0;
            return this;
        }

        [DocStr(
            "@desc Implements the foreach loop for this object.",
            "@returns This list."
            )]
        [FunctionAttribute("func __iter__ () : list")]
        public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            (self as HassiumByteArray).IterIndex = 0;
            return self;
        }

        public override HassiumObject IterableFull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(IterIndex >= Values.Count());
        }

        [DocStr(
            "@desc Implements the foreach loop for this object.",
            "@returns true if the loop has iterated through each element, otherwise false."
            )]
        [FunctionAttribute("func __iterfull__ () : bool")]
        public static HassiumObject iterablefull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var arr = (self as HassiumByteArray);
            return new HassiumBool(arr.IterIndex >= arr.Values.Count());
        }

        public override HassiumObject IterableNext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)Values[(self as HassiumByteArray).IterIndex++]);
        }

        [DocStr(
            "@desc Implements the foreach loop for this object.",
            "@returns The next element in the iterable stream."
            )]
        [FunctionAttribute("func __iternext__ () : bool")]
        public static HassiumObject iterablenext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var arr = (self as HassiumByteArray);
            return new HassiumChar((char)arr.Values[arr.IterIndex++]);
        }

        [DocStr(
            "@desc Gets the readonly integer representing the length of this list.",
            "@returns The length of the list as int."
            )]
        [FunctionAttribute("length { get; }")]
        public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumInt(Values.Count());
        }

        [DocStr(
            "@desc Removes the specified byte from the list.",
            "@param byte The char to remove.",
            "@returns null."
            )]
        [FunctionAttribute("func remove (byte : char) : null")]
        public static HassiumNull remove(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var b = (byte)args[0].ToChar(vm, args[0], location).Char;

            if (!Values.Contains(b))
            {
                vm.RaiseException(HassiumKeyNotFoundException.KeyNotFoundExceptionTypeDef._new(vm, self, location, self, args[0]));
                return Null;
            }

            Values.Remove(b);
            return Null;
        }

        [DocStr(
            "@desc Removes the object at the specified index.",
            "@param index The zero-based index to remove.",
            "@returns null."
            )]
        [FunctionAttribute("func removeat (index : int) : null")]
        public static HassiumNull removeat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            Values.Remove(Values[(int)index.Int]);
            return Null;
        }

        [DocStr(
            "@desc Returns a new list containing the values of this list in reverse order.",
            "@returns The new list of reversed values."
            )]
        [FunctionAttribute("func reverse () : list")]
        public static HassiumList reverse(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            Values[(int)index.Int] = (byte)args[1].ToChar(vm, args[1], location).Char;
            return args[1];
        }

        [DocStr(
            "@desc Implements the []= operator to store the specified byte at the given index.",
            "@param idnex The zero-based index to store the byte at.",
            "@param byte The char to be stored.",
            "@returns The provided byte."
            )]
        [FunctionAttribute("func __storeindex__ (index : int, byte : char) : object")]
        public static  HassiumObject storeindex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            Values[(int)index.Int] = (byte)args[1].ToChar(vm, args[1], location).Char;
            return args[1];
        }

        [DocStr(
            "@desc Converts this byte array to an ASCII string.",
            "@returns The ASCII value of this array as string."
            )]
        [FunctionAttribute("func toascii () : string")]
        public static HassiumString toascii(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumString(ASCIIEncoding.ASCII.GetString(Values.ToArray()));
        }

        [DocStr(
            "@desc Returns this byte array.",
            "@returns This byte array."
            )]
        [FunctionAttribute("func tobytearr () : list")]
        public static HassiumByteArray tobytearr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return self as HassiumByteArray;
        }

        [DocStr(
            "@desc Converts this byte array to a hex string and returns it.",
            "@returns The hex string value of this byte array."
            )]
        [FunctionAttribute("func tohex () : string")]
        public static HassiumString tohex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumByteArray).Values;
            return new HassiumString(BitConverter.ToString(Values.ToArray()).Replace("-", string.Empty));
        }

        public override HassiumList ToList(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [DocStr(
            "@desc Returns this byte array as a list.",
            "@returns This byte array as list."
            )]
        [FunctionAttribute("func tolist () : list")]
        public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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

        [DocStr(
            "@desc Returns this byte array as a string formatted as [ val1, val2, ... ]",
            "@returns The string value of this byte array."
            )]
        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
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
