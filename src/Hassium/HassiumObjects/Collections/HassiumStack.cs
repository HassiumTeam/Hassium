using System.Collections;
using System.Collections.Generic;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Collections
{
    public class HassiumStack: HassiumObject
    {
        public Stack<HassiumObject> Value { get; private set; }

        public HassiumStack(Stack<HassiumObject> value)
        {
            Value = value;
            Attributes.Add("clear", new InternalFunction(clear, 0));
            Attributes.Add("contains", new InternalFunction(contains, 1));
            Attributes.Add("peek", new InternalFunction(peek, 0));
            Attributes.Add("pop", new InternalFunction(pop, 0));
            Attributes.Add("push", new InternalFunction(push, 1));
            Attributes.Add("length", new HassiumProperty("length", x => Value.Count, null, true));
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            Value.Clear();
            return null;
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return Value.Contains(args[0]);
        }

        private HassiumObject peek(HassiumObject[] args)
        {
            return Value.Peek();
        }

        private HassiumObject pop(HassiumObject[] args)
        {
            return Value.Pop();
        }

        private HassiumObject push(HassiumObject[] args)
        {
            Value.Push(args[0]);
            return null;
        }
    }
}

