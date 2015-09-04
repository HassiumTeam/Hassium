using System;
using System.Collections;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.List
{
    public class HassiumStack: HassiumObject
    {
        public Stack Value { get; private set; }

        public HassiumStack(Stack value)
        {
            this.Value = value;
            this.Attributes.Add("clear", new InternalFunction(clear));
            this.Attributes.Add("contains", new InternalFunction(contains));
            this.Attributes.Add("peek", new InternalFunction(peek));
            this.Attributes.Add("pop", new InternalFunction(pop));
            this.Attributes.Add("push", new InternalFunction(push));
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            this.Value.Clear();
            return null;
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return new HassiumBool(this.Value.Contains(args[0]));
        }

        private HassiumObject peek(HassiumObject[] args)
        {
            return ((HassiumObject)this.Value.Peek());
        }

        private HassiumObject pop(HassiumObject[] args)
        {
            return ((HassiumObject)this.Value.Pop());
        }

        private HassiumObject push(HassiumObject[] args)
        {
            this.Value.Push(args[0]);
            return null;
        }
    }
}

