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
                AddAttribute("match", match, 2);
            }

            [DocStr(
                "@desc Returns the matches of the specified input string that meet the specified regex string.",
                "@param re The regex string.",
                "@param str the Input string.",
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
