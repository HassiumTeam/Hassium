using Hassium.HassiumObjects;

namespace Hassium.Lexer
{
    public interface IFunction
    {
        HassiumObject Invoke(HassiumObject[] args);
    }
}