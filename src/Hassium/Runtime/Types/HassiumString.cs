using Hassium.Compiler;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumString : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new StringTypeDef();
        
        public string String { get; private set; }

        public HassiumString(string val)
        {
            AddType(TypeDefinition);
            String = val;
        }

        [DocStr(
            "@desc A class represneting an array of characters.",
            "@returns string."
            )]
        public class StringTypeDef : HassiumTypeDefinition
        {
            public StringTypeDef() : base("string")
            {
                AddAttribute(ADD, add, 1);
                AddAttribute("contains", contains, 1);
                AddAttribute(EQUALTO, equalto, 1);
                AddAttribute("endswith", endswith, 1);
                AddAttribute("format", format, -1);
                AddAttribute(GREATERTHAN, greaterthan, 1);
                AddAttribute(GREATERTHANOREQUAL, greaterthanorequal, 1);
                AddAttribute(INDEX, index, 1);
                AddAttribute("indexof", indexof, 1);
                AddAttribute(ITER, iter, 0);
                AddAttribute("length", new HassiumProperty(get_length));
                AddAttribute(LESSERTHAN, lesserthan, 1);
                AddAttribute(LESSERTHANOREQUAL, lesserthanorequal, 1);
                AddAttribute(MODULUS, modulus, 1);
                AddAttribute(NOTEQUALTO, notequalto, 1);
                AddAttribute("split", split, 1);
                AddAttribute("startswith", startsswith, 1);
                AddAttribute("substring", substring, 1, 2);
                AddAttribute(TOCHAR, tochar, 0);
                AddAttribute(TOFLOAT, tofloat, 0);
                AddAttribute(TOINT, toint, 0);
                AddAttribute(TOLIST, tolist, 0);
                AddAttribute("tolower", tolower, 0);
                AddAttribute(TOSTRING, tostring, 0);
                AddAttribute("toupper", toupper, 0);
            }

            [DocStr(
                "@desc Constructs a new string object using the specified value.",
                "@param val The value.",
                "@returns The new string object."
                )]
            [FunctionAttribute("func new (val : object) : string")]
            public static HassiumString _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args[0] is HassiumList)
                    return HassiumList.ListTypeDef.toascii(vm, args[0], location);
                return new HassiumString(args[0].ToString(vm, args[0], location).String);
            }

            [DocStr(
                "@desc Implements the + operator to return the specified string appended to this string.",
                "@param str The string to append.",
                "@returns A new string with the value of this string appended with the string."
                )]
            [FunctionAttribute("func __add__ (str : string) : string")]
            public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumString(String + args[0].ToString(vm, args[0], location).String);
            }

            [DocStr(
                "@desc Returns a boolean indicating if this string contains the specified string.",
                "@param str The string to check.",
                "@returns true if this string contains the specified string, otherwise false."
                )]
            [FunctionAttribute("func contains (str : string) : bool")]
            public static HassiumBool contains(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(String.Contains(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Implements the == operator to determine if the specified string is equal to this string.",
                "@param str The string to compare.",
                "@returns true if the strings are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (str : string) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(String == args[0].ToString(vm, args[0], location).String);
            }

            [DocStr(
                "@desc Returns a boolean indicating if this string ends with the specified string.",
                "@param str The string to check.",
                "@returns true if this string does end with the string, otherwise false."
                )]
            [FunctionAttribute("func endswith (str : string) : bool")]
            public static HassiumBool endswith(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumString).String.EndsWith(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Treats this string as a format string, using the given list of format args to format and return a new string.",
                "@optional params fargs The format args to format this string with.",
                "@returns A new formatted string using this string and the format args."
                )]
            [FunctionAttribute("func format (params fargs) : string")]
            public static HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;

                string[] strargs = new string[args.Length];
                for (int i = 0; i < strargs.Length; i++)
                    strargs[i] = args[i].ToString(vm, args[i], location).String;
                return new HassiumString(string.Format(String, strargs));
            }

            [DocStr(
                "@desc Implements the > operator to determine if this string is greater than the specified string.",
                "@param str The string to compare.",
                "@returns true if this string is greater than the string, otherwise false."
                )]
            [FunctionAttribute("func __greater__ (str : string) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) == 1);
            }

            [DocStr(
                "@desc Implements the >= operator to determine if this string is greater than or equal to the specified string.",
                "@param str The string to compare.",
                "@returns true if this string is greater than or equal to the string, otherwise false."
                )]
            [FunctionAttribute("func __greaterorequal__ (str : string) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) >= 0);
            }

            [DocStr(
                "@desc Implements the [] operator to get the char at the specified 0-based index.",
                "@param index The 0-based index to get the char at.",
                "@returns The char at the index."
                )]
            [FunctionAttribute("func __index__ (index : int) : char")]
            public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumChar(String[(int)args[0].ToInt(vm, args[0], location).Int]);
            }

            [DocStr(
                "@desc Returns the first 0-based index of the specified string in this string.",
                "@param str The string to get the index of.",
                "@returns The 0-based index of where the string starts in this string."
                )]
            [FunctionAttribute("func indexof (str : string) : int")]
            public static HassiumInt indexof(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumString).String.IndexOf(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Implements the foreach loop to return a list of chars in this string.",
                "@returns A new list of the chars in this string."
                )]
            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                HassiumList list = new HassiumList(new HassiumObject[0]);
                foreach (var c in String)
                    HassiumList.add(vm, list, location, new HassiumChar(c));
                return list;
            }

            [DocStr(
                "@desc Gets the readonly int length of this string.",
                "@returns The length of this string as int."
                )]
            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumInt(String == null ? -1 : String.Length);
            }

            [DocStr(
                "@desc Implements the < operator to determine if this string is lesser than the specified string.",
                "@param str The string to compare.",
                "@returns true if this string is lesser than the string, otherwise false."
                )]
            [FunctionAttribute("func __lesser__ (str : string) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) == -1);
            }

            [DocStr(
                "@desc Implements the <= operator to determine if this string is lesser than or equal to the specified string.",
                "@param str the string to compare.",
                "@returns true if this string is lesser than or equal to the string, otherwise false."
                )]
            [FunctionAttribute("func __lesserorequal__ (str : string) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) <= 0);
            }

            [DocStr(
                "@desc Implements the % operator to use this string as a format string with the specified list or tuple as format args.",
                "@param listOrTuple The list or tuple object that will act as format args.",
                "@returns The formatted string."
                )]
            [FunctionAttribute("func __modulus__ (listOrTuple : object) : string")]
            public static HassiumString modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var vals = args[0].ToList(vm, args[0], location).Values;
                vals.Insert(0, self);
                return new HassiumString(GlobalFunctions.format(vm, null, location, vals.ToArray()).String);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this string is not equal to the specified string.",
                "@param str The string to compare.",
                "@returns true if this string is not equal to the string, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (str : string) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumBool(String != args[0].ToString(vm, args[0], location).String);
            }

            [DocStr(
                "@desc Divides the string into a list of substrings based on the specified separator.",
                "@param c The char to split on.",
                "@returns The list of substrings."
                )]
            [FunctionAttribute("func split (c : char) : list")]
            public static HassiumList split(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                var list = new HassiumList(new HassiumObject[0]);
                foreach (var part in String.Split(args[0].ToChar(vm, args[0], location).Char))
                    HassiumList.ListTypeDef.add(vm, list, location, new HassiumString(part));
                return list;
            }

            [DocStr(
                "@desc Returns a boolean indicating if this string starts with the specified string.",
                "@param str The string to check.",
                "@returns true if this string does start with the string, otherwise false."
            )]
            [FunctionAttribute("func startswith (str : string) : bool")]
            public static HassiumBool startsswith(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumString).String.StartsWith(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Takes the substring of this string at the specified start index and optional length.",
                "@param start The 0-based start index.",
                "@optional len The 0-based ending index.",
                "@returns The substring."
                )]
            [FunctionAttribute("func substring (start : int) : string", "func substring (start : int, len : int) : string")]
            public static HassiumString substring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args.Length == 2)
                    return new HassiumString((self as HassiumString).String.Substring((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int));
                else
                    return new HassiumString((self as HassiumString).String.Substring((int)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Converts this string to a character value.",
                "@returns This string as char."
                )]
            [FunctionAttribute("func tochar () : char")]
            public static HassiumChar tochar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumChar((self as HassiumString).String[0]);
            }

            [DocStr(
                "@desc Converts this string to a float value and returns it.",
                "@returns The float value of this string."
                )]
            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumFloat(Convert.ToDouble(String));
            }

            [DocStr(
                "@desc Converts this string to an integer value and returns it.",
                "@returns The int value of this string."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                try
                {
                    return new HassiumInt(Convert.ToInt64(String));
                }
                catch
                {
                    vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, tostring(vm, self, location), HassiumInt.TypeDefinition));
                    return new HassiumInt(-1);
                }
            }
               
            [DocStr(
                "@desc Converts this string to a list this string's chars.",
                "@returns A list containing each char in the string."
                )]
            [FunctionAttribute("func tolist () : list")]
            public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return iter(vm, self, location, args) as HassiumList;
            }

            [DocStr(
                "@desc Converts this string to lowercase and returns it.",
                "@returns This string with all lowercase values."
                )]
            [FunctionAttribute("func tolower () : string")]
            public static HassiumString tolower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumString(String.ToLower());
            }

            [DocStr(
                "@desc Returns this string.",
                "@returns This string."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumString;
            }

            [DocStr(
                "@desc Converts this string to uppercase and returns it.",
                "@returns This string with all uppercase values."
                )]
            [FunctionAttribute("func toupper () : string")]
            public static HassiumString toupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var String = (self as HassiumString).String;
                return new HassiumString(String.ToUpper());
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
