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
            this.Value = value;
            this.Attributes.Add("append", new InternalFunction(append));
            this.Attributes.Add("appendLine", new InternalFunction(appendLine));
            this.Attributes.Add("clear", new InternalFunction(clear));
            this.Attributes.Add("insert", new InternalFunction(insert));
            this.Attributes.Add("remove", new InternalFunction(remove));
            this.Attributes.Add("replace", new InternalFunction(replace));
            this.Attributes.Add("toString", new InternalFunction(toString));
            this.Attributes.Add("length", new InternalFunction(x => Value.Length, true));
        }

        private HassiumObject append(HassiumObject[] args)
        {
            this.Value.Append(args[0].ToString());
            return null;
        }

        private HassiumObject appendLine(HassiumObject[] args)
        {
            this.Value.AppendLine(args[0].ToString());
            return null;
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            this.Value.Clear();
            return null;
        }

        private HassiumObject insert(HassiumObject[] args)
        {
            this.Value.Insert(((HassiumNumber)args[0]).ValueInt, args[1].ToString());
            return null;
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            this.Value.Remove(((HassiumNumber)args[0]).ValueInt, ((HassiumNumber)args[1]).ValueInt);
            return null;
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            this.Value.Replace(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ToString());
        }
    }
}

