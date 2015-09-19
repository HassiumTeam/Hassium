using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumStringBuilder : HassiumObject
    {
        public StringBuilder Value { get; private set; }

        public HassiumStringBuilder(StringBuilder value)
        {
            Value = value;
            Attributes.Add("append", new InternalFunction(append, 1));
            Attributes.Add("appendLine", new InternalFunction(appendLine, 1));
            Attributes.Add("clear", new InternalFunction(clear, 0));
            Attributes.Add("insert", new InternalFunction(insert, 2));
            Attributes.Add("remove", new InternalFunction(remove, 2));
            Attributes.Add("replace", new InternalFunction(replace, 2));
            Attributes.Add("toString", new InternalFunction(toString, 0));
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
        }

        private HassiumObject append(HassiumObject[] args)
        {
            Value.Append(args[0].ToString());
            return null;
        }

        private HassiumObject appendLine(HassiumObject[] args)
        {
            Value.AppendLine(args[0].ToString());
            return null;
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            Value.Clear();
            return null;
        }

        private HassiumObject insert(HassiumObject[] args)
        {
            Value.Insert(args[0].HInt().Value, args[1].ToString());
            return null;
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            Value.Remove(args[0].HInt().Value, args[1].HInt().Value);
            return null;
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            Value.Replace(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}