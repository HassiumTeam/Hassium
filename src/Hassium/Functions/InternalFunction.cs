using System;
using Hassium.HassiumObjects;

namespace Hassium
{
    public delegate HassiumObject HassiumFunctionDelegate (HassiumObject[] arguments);

    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target, bool prop = false)
        {
            this.target = target;
            IsProperty = prop;
        }

        public bool IsProperty { get; set; }


        public override string ToString()
        {
            return ((object) this).ToString();
        }

        public override HassiumObject Invoke(HassiumObject[] args)
        {
            return target(args);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public IntFunc(string name)
        {
            Name = name;
            Alias = "";
        }

        public IntFunc(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }
    }
}

