using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumList: HassiumObject
    public List<HassiumObject> Value { get; private set; }
    {

        public HassiumList()
        {
            this.Value = new List<HassiumObject>();

            this.Attributes.Add("count", new InternalFunction(count, 0));
            this.Attributes.Add("add", new InternalFunction(add, -1));
            this.Attributes.Add("get", new InternalFunction(get, 1));
            this.Attributes.Add("remove", new InternalFunction(remove, -1));
        }

        private HassiumObject count(HassiumObject[] args)
        {
            return new HassiumDouble(this.Value.Count);
        }

        private HassiumObject add(HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                this.Value.Add(obj);

            return null;
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return this.Value[((HassiumDouble)args[0]).ValueInt];
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
            {
                if (!this.Value.Contains(obj.ToString()))
                    throw new Exception("Entry " + args + " does not exist in list!");

                this.Value.Remove(obj.ToString());
            }

            return null;
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return new HassiumBool(this.Value.Contains(args[0]));
        }
    }
}
