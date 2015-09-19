using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.HassiumObjects;

namespace Hassium.Functions
{
    public delegate HassiumObject HassiumFunctionDelegate(params HassiumObject[] arguments);

    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target, int args, bool prop = false, bool constr = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
            Arguments = new[] {args};
        }

        public InternalFunction(HassiumFunctionDelegate target, int[] args, bool prop = false, bool constr = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
            Arguments = args;
        }

        public bool IsProperty { get; set; }

        public bool IsConstructor { get; set; }

        public int[] Arguments { get; set; }


        public override string ToString()
        {
            return string.Format("[InternalFunction: {0}`{1}]", target.Method.Name,
                target.Method.GetParameters().Count());
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            if (!Arguments.Contains(args.Length) && Arguments[0] != -1)
                throw new Exception("Function " + target.Method.Name + " has " + Arguments.Max() +
                                    " arguments, but is invoked with " + args.Length);
            return target(args);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Constructor { get; set; }
        public int[] Arguments { get; set; }

        public IntFunc(string name, int args) : this(name, args, "")
        {
        }

        public IntFunc(string name, int[] args) : this(name, args, "")
        {
        }

        public IntFunc(string name, int args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = new[] {args};
        }

        public IntFunc(string name, int[] args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = args;
        }

        public IntFunc(string name, bool constr, int args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = new[] {args};
        }

        public IntFunc(string name, bool constr, int[] args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = args;
        }
    }
}