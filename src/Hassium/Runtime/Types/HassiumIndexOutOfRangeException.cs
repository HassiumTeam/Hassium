using Hassium.Compiler;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Types
{
    public class HassiumIndexOutOfRangeException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new IndexOutOfRangeExceptionTypeDef();

        public HassiumObject Object { get;  set; }
        public HassiumInt RequestedIndex { get; set; }

        public HassiumIndexOutOfRangeException()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class representing an exception that is thrown if an index is out of the range of an object.",
            "@returns IndexOutOfRangeException."
            )]
        public class IndexOutOfRangeExceptionTypeDef : HassiumTypeDefinition
        {
            public IndexOutOfRangeExceptionTypeDef() : base("IndexOutOfRangeException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "index", new HassiumProperty(get_index) },
                    { INVOKE, new HassiumFunction(_new, 2) },
                    { "message", new HassiumProperty(get_message) },
                    { "object", new HassiumProperty(get_object) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [DocStr(
                "@desc Constructs a new IndexOutOfRangeException using the specified object and requested index.",
                "@param obj The object whose index was out of range.",
                "@param reqindex The int index that was not in range of the object.",
                "@returns The new IndexOutOfRangeException object."
                )]
            [FunctionAttribute("func new (obj : object, int reqindex) : IndexOutOfRangeException")]
            public static HassiumIndexOutOfRangeException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumIndexOutOfRangeException exception = new HassiumIndexOutOfRangeException();

                exception.Object = args[0];
                exception.RequestedIndex = args[1].ToInt(vm, args[1], location);

                return exception;
            }

            [DocStr(
                "@desc Gets the readonly integer index that was out of range.",
                "@returns The out of range index as int."
                )]
            [FunctionAttribute("index { get; }")]
            public static HassiumInt get_index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumIndexOutOfRangeException).RequestedIndex;
            }

            [DocStr(
                "@desc Gets the readonly string message of the exception.",
                "@returns The exception message string."
                )]
            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var exception = (self as HassiumIndexOutOfRangeException);
                return new HassiumString(string.Format("Out of range: Index '{0}' is less than 0 or greater than the size of the collection of type '{1}', with a max length of '{2}'", exception.RequestedIndex.Int, exception.Object.Type(), exception.Object.GetAttribute(vm, "length").Invoke(vm, location).ToString(vm, null, location).String));
            }

            [DocStr(
                "@desc Gets the readonly object whose index was out of range.",
                "@returns The object that was out of range."
                )]
            [FunctionAttribute("object { get; }")]
            public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumIndexOutOfRangeException).Object;
            }

            [DocStr(
                "@desc Returns the string value of the exception, including the message and callstack.",
                "@returns The string value of the exception."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(get_message(vm, self, location).String);
                sb.Append(vm.UnwindCallStack());

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
