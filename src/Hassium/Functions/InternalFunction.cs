using System;

namespace Hassium
{
    public delegate object HassiumFunctionDelegate (object[] arguments);

    public class InternalFunction : IFunction
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target)
        {
            this.target = target;
        }

        public object Invoke(object[] args)
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

