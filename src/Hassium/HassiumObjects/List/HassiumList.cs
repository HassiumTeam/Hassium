﻿using System.Collections.Generic;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.List
{
    public class HassiumList: HassiumObject
    {
        public List<HassiumObject> Value { get; private set; }

        public HassiumList(List<HassiumObject> value)
        {
            Value = value;
            Attributes.Add("add", new InternalFunction(add));
            Attributes.Add("count", new InternalFunction(count));
            Attributes.Add("get", new InternalFunction(get));
            Attributes.Add("set", new InternalFunction(set));
        }

        private HassiumObject add(HassiumObject[] args)
        {
            Value.Add(args[0]);
            return null;

        }

        private HassiumObject count(HassiumObject[] args)
        {
            return new HassiumNumber(Value.Count);
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return Value[((HassiumNumber)args[0]).ValueInt];
        }

        private HassiumObject set(HassiumObject[] args)
        {
            return Value[((HassiumNumber)args[0]).ValueInt] = args [1];
        }
    }
}

