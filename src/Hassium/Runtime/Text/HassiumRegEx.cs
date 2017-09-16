using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hassium.Runtime.Text
{
    public class HassiumRegEx : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new RegExTypeDef();

        public Regex Regex { get; private set; }

        public HassiumRegEx()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class containing methods for interacting with regular expressions",
            "@returns RegEx."
            )]
        public class RegExTypeDef : HassiumTypeDefinition
        {
            public RegExTypeDef() : base("RegEx")
            {
                AddAttribute("ismatch", ismatch, 2);
                AddAttribute("match", match, 2);
                AddAttribute("replace", replace, 3);
                AddAttribute("split", split, 2);
            }

            [DocStr(
                "@desc Returns a boolean indicating if the specified input string matches the specified regex string.",
                "@param re The regex string.",
                "@param str The input string.",
                "@returns true if the string matches the pattern, otherwise false."
                )]
            [FunctionAttribute("func ismatch (re : string, str : string) : bool")]
            public static HassiumBool ismatch(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool(Regex.IsMatch(args[1].ToString(vm, args[1], location).String, args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Returns the matches of the specified input string that meet the specified regex string.",
                "@param re The regex string.",
                "@param str the input string.",
                "@returns A list containing the substrings that matched the pattern."
                )]
            [FunctionAttribute("func match (re : string, str : string) : list")]
            public static HassiumList match(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Match match = Regex.Match(args[1].ToString(vm, args[1], location).String, args[0].ToString(vm, args[0], location).String);

                HassiumList list = new HassiumList(new HassiumObject[0]);

                while (match.Success)
                {
                    HassiumList.ListTypeDef.add(vm, list, location, new HassiumString(match.Value));
                    match = match.NextMatch();
                }

                return list;
            }

            [DocStr(
                "@desc Replaces in the specified input string with the specified replacement string using the specified regex string.",
                "@param re The regex string.",
                "@param inp The input string.",
                "@param repl The replacement string.",
                "@returns The string with replaced values."
                )]
            [FunctionAttribute("func replace (re : regex, inp : string, repl : string) : string")]
            public static HassiumString replace(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Regex.Replace(args[1].ToString(vm, args[1], location).String, args[0].ToString(vm, args[0], location).String, args[2].ToString(vm, args[2], location).String));
            }

            [DocStr(
                "@desc Splits the specified input string at the specified regex string and returns a list of substrings.",
                "@param re The regex string.",
                "@param str The input string.",
                "@returns The list of split substrings."
                )]
            [FunctionAttribute("func split (re : regex, str : string) : list")]
            public static HassiumList split(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumList list = new HassiumList(new HassiumObject[0]);

                foreach (var str in Regex.Split(args[1].ToString(vm, args[1], location).String, args[0].ToString(vm, args[0], location).String))
                    HassiumList.ListTypeDef.add(vm, list, location, new HassiumString(str));

                return list;
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
