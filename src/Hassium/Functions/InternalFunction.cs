using System;
using Hassium.HassiumObjects;

namespace Hassium.Functions
{
    public delegate HassiumObject HassiumFunctionDelegate (params HassiumObject[] arguments);

    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target, bool prop = false, bool constr = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
        }

        public bool IsProperty { get; set; }

        public bool IsConstructor { get; set; }


        public override string ToString()
        {
            return target.Method.Name;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            return target(args);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Constructor { get; set; }

        public IntFunc(string name) : this(name, "")
        {
        }

        public IntFunc(string name, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
        }

        public IntFunc(string name, bool constr)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
        }
    }
}

