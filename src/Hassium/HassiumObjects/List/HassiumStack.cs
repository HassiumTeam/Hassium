using System.Collections;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.List
{
    public class HassiumStack: HassiumObject
    {
        public Stack Value { get; private set; }

        public HassiumStack(Stack value)
        {
            Value = value;
            Attributes.Add("clear", new InternalFunction(clear));
            Attributes.Add("contains", new InternalFunction(contains));
            Attributes.Add("peek", new InternalFunction(peek));
            Attributes.Add("pop", new InternalFunction(pop));
            Attributes.Add("push", new InternalFunction(push));
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            Value.Clear();
            return null;
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return new HassiumBool(Value.Contains(args[0]));
        }

        private HassiumObject peek(HassiumObject[] args)
        {
            return ((HassiumObject)Value.Peek());
        }

        private HassiumObject pop(HassiumObject[] args)
        {
            return ((HassiumObject)Value.Pop());
        }

        private HassiumObject push(HassiumObject[] args)
        {
            Value.Push(args[0]);
            return null;
        }
    }
}

