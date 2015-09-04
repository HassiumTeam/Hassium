using System;
using System.Collections.Generic;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.List
{
    public class HassiumList: HassiumObject
    {
        public List<HassiumObject> Value { get; private set; }

        public HassiumList(List<HassiumObject> value)
        {
            this.Value = value;
            this.Attributes.Add("add", new InternalFunction(add));
            this.Attributes.Add("count", new InternalFunction(count));
            this.Attributes.Add("get", new InternalFunction(get));
        }

        private HassiumObject add(HassiumObject[] args)
        {
            this.Value.Add(args[0]);
            return null;

        }

        private HassiumObject count(HassiumObject[] args)
        {
            return new HassiumNumber(this.Value.Count);
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return this.Value[((HassiumNumber)args[0]).ValueInt];
        }
    }
}

