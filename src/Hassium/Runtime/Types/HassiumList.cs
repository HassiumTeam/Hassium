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
        public static new HassiumTypeDefinition TypeDefinition = new ListTypeDef();

        public List<HassiumObject> Values { get; private set; }

        public HassiumList(IEnumerable<HassiumObject> values)
        {
            AddType(TypeDefinition);
            Values = values.ToList();
        }

        public static HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Values = (self as HassiumList).Values;
            foreach (var arg in args)
                Values.Add(arg);

            return Null;
        }

        public int IterIndex = 0;

        public override HassiumObject Index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var index = args[0].ToInt(vm, args[0], location);
            if (index.Int < 0 || index.Int >= Values.Count)
            {
                vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                return Null;
            }
            return Values[(int)index.Int];
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return EqualTo(vm, self, location, args).LogicalNot(vm, self, location) as HassiumBool;
        }

        public override HassiumList ToList(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[ ");
            foreach (var v in Values)
                sb.AppendFormat("{0}, ", v.ToString(vm, v, location).String);
            if (Values.Count > 0)
                sb.Remove(sb.Length - 2, 2);
            sb.Append(" ]");

            return new HassiumString(sb.ToString());
        }

        public class ListTypeDef : HassiumTypeDefinition
        {
            public ListTypeDef() : base("list")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "add", new HassiumFunction(add, -1)  },
                    { "contains", new HassiumFunction(contains, 1)  },
                    { EQUALTO, new HassiumFunction(equalto, 1) },
                    { "fill", new HassiumFunction(fill, 2)  },
                    { "format", new HassiumFunction(format, 1)  },
                    { INDEX, new HassiumFunction(index, 1)  },
                    { ITER, new HassiumFunction(iter, 0)  },
                    { ITERABLEFULL, new HassiumFunction(iterablefull, 0)  },
                    { ITERABLENEXT, new HassiumFunction(iterablenext, 0)  },
                    { "length", new HassiumProperty(get_length)  },
                    { NOTEQUALTO, new HassiumFunction(notequalto, 1) },
                    { "peek", new HassiumFunction(peek, 0)  },
                    { "pop", new HassiumFunction(pop, 0)  },
                    { "push", new HassiumFunction(push, 1)  },
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
            }

            [DocStr(
                "@desc Adds the specified object to this list.",
                "@param obj The object to add.",
                "@returns null."
                )]
            [FunctionAttribute("func add (obj : object) : null")]
            public static HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                foreach (var arg in args)
                    Values.Add(arg);

                return Null;
            }

            [DocStr(
                "@desc Returns a boolean indicating if the specified object was found in this list.",
                "@param obj The object to check.",
                "@returns true if the object was found in the list, otherwise false."
                )]
            [FunctionAttribute("func contains (obj : object) : bool")]
            public static HassiumBool contains(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                foreach (var value in Values)
                    if (value == args[0] || args[0].EqualTo(vm, args[0], location, args[0]).Bool)
                        return True;
                return False;
            }

            [DocStr(
                "@desc Implements the == operator to determine if the specified list is equal to this list.",
                "@param l The list to compare.",
                "@returns true if the lists are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (l : list) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                var list = args[0].ToList(vm, args[0], location).Values;
                if (list.Count != Values.Count)
                    return False;

                for (int i = 0; i < Values.Count; i++)
                    if (!contains(vm, self, location, list[i]).Bool)
                        return False;

                return True;
            }

            [DocStr(
                "@desc Returns a new list filled with the specified count of specified objects.",
                "@param count The amount of objects the new list should contain.",
                "@param obj The object to fill the list with."
                )]
            [FunctionAttribute("func fill (count : int, val : object) : list")]
            public static HassiumList fill(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumList list = new HassiumList(new HassiumObject[0]);

                int count = (int)args[0].ToInt(vm, args[0], location).Int;
                for (int i = 0; i < count; i++)
                    add(vm, list, location, args[1]);

                return list;
            }

            [DocStr(
                "@desc Formats this list using the specified format string and returns the string result.",
                "@param fmt The format string.",
                "@returns This list formatted as string."
                )]
            [FunctionAttribute("func format (fmt : string) : string")]
            public static HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                StringBuilder sb = new StringBuilder();
                string fmt = args[0].ToString(vm, args[0], location).String;
                byte[] bytes = ASCIIEncoding.ASCII.GetBytes(tostring(vm, self, location).String);
                foreach (var b in bytes)
                    sb.AppendFormat(fmt, b);

                return new HassiumString(sb.ToString());
            }

            [DocStr(
                "@desc Implements the [] operator to return the object at the specified 0-based index.",
                "@param i The 0-based index to get the object at.",
                "@returns The object at the index."
                )]
            [FunctionAttribute("func __index__ (i : int) : object")]
            public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                var index = args[0].ToInt(vm, args[0], location);
                if (index.Int < 0 || index.Int >= Values.Count)
                {
                    vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                    return Null;
                }
                return Values[(int)index.Int];
            }

            [DocStr(
                "@desc Implements the foreach loop, returning this list.",
                "@returns This list."
                )]
            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumList).IterIndex = 0;
                return self;
            }

            [DocStr(
                "@desc Implements the foreach loop, returning a boolean indicating if the loop has reached the end of the list.",
                "@returns true if the loop is at the end of the list, otherwise false."
                )]
            [FunctionAttribute("func __iterfull__ () : bool")]
            public static HassiumObject iterablefull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var list = self as HassiumList;
                return new HassiumBool(list.IterIndex >= list.Values.Count);
            }
            
            [DocStr(
                "@desc Implements the foreach loop, returning the next iterable object.",
                "@returns The next iterable object in this list."
                )]
            [FunctionAttribute("func __iternext__ () : bool")]
            public static HassiumObject iterablenext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                return Values[(self as HassiumList).IterIndex++];
            }

            [DocStr(
                "@desc Gets the readonly int representing the amount of elements in this list.",
                "@returns The amount of elements in the list as int."
                )]
            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                return new HassiumInt(Values.Count);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this list is not equal to the specified list.",
                "@param l The list to compare.",
                "@returns true if the lists are not equal, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (l : list) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return equalto(vm, self, location, args).LogicalNot(vm, self, location) as HassiumBool;
            }

            [DocStr(
                "@desc Returns the top value in this list.",
                "@returns The top value in this list."
                )]
            [FunctionAttribute("func peek () : object")]
            public static HassiumObject peek(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                if (Values.Count == 0)
                    vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, null, location, self, new HassiumInt(1)));
                return Values[Values.Count - 1];
            }

            [DocStr(
                "@desc Removes the top value from this list and returns it.",
                "@returns The top value in this list."
                )]
            [FunctionAttribute("func pop () : object")]
            public static HassiumObject pop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                try
                {
                    if (Values.Count == 0)
                        vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, null, location, self, new HassiumInt(1)));
                    return Values[Values.Count - 1];
                }
                finally
                {
                    Values.Remove(Values[Values.Count - 1]);
                }
            }

            [DocStr(
                "@desc Adds the specified object to the top of the list (same as list.add).",
                "@param obj The object to add.",
                "@returns null."
                )]
            [FunctionAttribute("func push (obj : object) : null")]
            public static HassiumNull push(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                Values.Add(args[0]);
                return Null;
            }

            [DocStr(
                "@desc Removes the specified object from this list.",
                "@param obj The object to remove.",
                "@returns null."
                )]
            [FunctionAttribute("func remove (obj : object) : null")]
            public static HassiumNull remove(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                foreach (var value in Values)
                {
                    if (value == args[0] || args[0].EqualTo(vm, args[0], location, args[0]).Bool)
                    {
                        Values.Remove(value);
                        return Null;
                    }
                }
                return Null;
            }

            [DocStr(
                "@desc Removes the object at the specified 0-based index.",
                "@param index The 0-based index to remove at.",
                "@returns nuil."
                )]
            [FunctionAttribute("func removeat (index : int) : null")]
            public static HassiumNull removeat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
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
                "@desc Returns a new list with the values in this list reversed.",
                "@returns A new list containing the elements from this list in reverse order."
                )]
            [FunctionAttribute("func reverse () : list")]
            public static HassiumList reverse(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                HassiumObject[] list = new HassiumObject[Values.Count];
                Values.CopyTo(list);
                list.Reverse();
                return new HassiumList(list);
            }

            [DocStr(
                "@desc Implements the []= operator, storing the specified object at the specified 0-based index.",
                "@param index The 0-based index to store at.",
                "@param obj The object to store at the index.",
                "@returns The object that was passed."
                )]
            [FunctionAttribute("func __storeindex__ (index : int, obj : object) : object")]
            public static HassiumObject storeindex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                var index = args[0].ToInt(vm, args[0], location);
                if (index.Int < 0 || index.Int >= Values.Count)
                {
                    vm.RaiseException(HassiumIndexOutOfRangeException.IndexOutOfRangeExceptionTypeDef._new(vm, self, location, self, index));
                    return Null;
                }
                Values[(int)index.Int] = args[1];
                return args[1];
            }

            [DocStr(
                "@desc Converts this list to an ASCII string and returns it.",
                "@returns The ASCII string value of this list."
                )]
            [FunctionAttribute("func toascii () : string")]
            public static HassiumString toascii(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                byte[] bytes = new byte[Values.Count];
                for (int i = 0; i < Values.Count; i++)
                    bytes[i] = (byte)Values[i].ToChar(vm, Values[i], location).Char;
                return new HassiumString(ASCIIEncoding.ASCII.GetString(bytes));
            }

            [DocStr(
                "@desc Converts this list to a BigInt and returns it.",
                "@returns The big integer value of this list."
                )]
            [FunctionAttribute("func tobigint () : BigInt")]
            public static HassiumBigInt tobigint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                return new HassiumBigInt() { BigInt = new BigInteger(ListToByteArr(vm, location, self as HassiumList)) };
            }

            [DocStr(
                "@desc Converts this list to a more optimized form of list called bytearr.",
                "@returns This list as an optimized byte arr."
                )]
            [FunctionAttribute("func tobytearr () : list")]
            public static HassiumByteArray tobytearr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                var arr = new HassiumByteArray(new byte[0], new HassiumObject[0]);

                foreach (var val in Values)
                    arr.Values.Add((byte)val.ToChar(vm, val, location).Char);

                return arr;
            }

            [DocStr(
                "@desc Converts this list to a hex string and returns it.",
                "@returns The hex value of this string."
                )]
            [FunctionAttribute("func tohex () : string")]
            public static HassiumString tohex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                byte[] bytes = new byte[Values.Count];
                for (int i = 0; i < Values.Count; i++)
                    bytes[i] = (byte)Values[i].ToChar(vm, Values[i], location).Char;
                return new HassiumString(BitConverter.ToString(bytes).Replace("-", string.Empty));
            }

            [DocStr(
                "@desc Returns this list.",
                "@returns This list."
                )]
            [FunctionAttribute("func tolist () : list")]
            public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumList;
            }

            [DocStr(
                "@desc Returns this list as a string formatted as [ val1, val2, ... ]",
                "@returns The string value of this list."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumList).Values;
                StringBuilder sb = new StringBuilder();

                sb.Append("[ ");
                foreach (var v in Values)
                    sb.AppendFormat("{0}, ", v.ToString(vm, v, location).String);
                if (Values.Count > 0)
                    sb.Remove(sb.Length - 2, 2);
                sb.Append(" ]");

                return new HassiumString(sb.ToString());
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
