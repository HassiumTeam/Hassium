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
            return this.target(args);
        }
    }
}

