using Hassium.Runtime;

namespace Hassium.Compiler.Parser
{
    public class FunctionParameter
    {
        public FunctionParameterType FunctionParameterType { get; private set; }

        public string Name { get; private set; }
        public AstNode Type { get; private set; }

        public HassiumMethod EnforcedType { get; set; }

        public FunctionParameter(FunctionParameterType functionParameterType, string name)
        {
            Name = name;
            Type = null;

            FunctionParameterType = functionParameterType;
        }

        public FunctionParameter(FunctionParameterType functionParameterType, string name, AstNode type)
        {
            Name = name;
            Type = type;

            FunctionParameterType = functionParameterType;
        }
    }

    public enum FunctionParameterType
    {
        Enforced,
        Normal,
        Variadic
    }
}
