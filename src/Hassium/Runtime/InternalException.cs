using System;

namespace Hassium.Runtime
{
    public class InternalException : Exception
    {
        public static string ATTRIBUTE_ACCESS =     "Could not access private member {0} from {1}!";
        public static string ATTRIBUTE_NOT_FOUND =  "Could not find attribute {0} in {1}!";
        public static string CONVERSION_ERROR =     "Could not convert {0} to {1}!";
        public static string KEY_NOT_FOUND_ERROR =  "Could not find key \"{0}\"!";
        public static string OPERATOR_ERROR =       "Could not apply operator {0} to {1}!";
        public static string PARAMETER_ERROR =      "Expected parameter of type {0}, got {1}!";
        public static string RETURN_ERROR =         "Expected return type {0}, got {1}!";
        public static string VALUE_NOT_FOUND_ERROR= "Could not find value \"{0}\"!";
        public static string VARIABLE_ERROR =       "Variable {0} does not exist!";

        public new string Message { get; private set; }
        public VirtualMachine VM { get; private set; }

        public InternalException(VirtualMachine vm, string messageFormat, params object[] args)
        {
            Message = string.Format(messageFormat, args);
            VM = vm;
        }
    }
}

