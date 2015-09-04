using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumStringBuilder: HassiumObject
    {
        public StringBuilder Value { get; private set; }

        public HassiumStringBuilder(StringBuilder value)
        {
            Value = value;
            Attributes.Add("append", new InternalFunction(append));
            Attributes.Add("appendLine", new InternalFunction(appendLine));
            Attributes.Add("clear", new InternalFunction(clear));
            Attributes.Add("insert", new InternalFunction(insert));
            Attributes.Add("remove", new InternalFunction(remove));
            Attributes.Add("replace", new InternalFunction(replace));
            Attributes.Add("toString", new InternalFunction(toString));
            Attributes.Add("length", new InternalFunction(x => Value.Length, true));
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
            Value.Insert(((HassiumNumber)args[0]).ValueInt, args[1].ToString());
            return null;
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            Value.Remove(((HassiumNumber)args[0]).ValueInt, ((HassiumNumber)args[1]).ValueInt);
            return null;
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            Value.Replace(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

