namespace Hassium.Runtime.Math
{
    public class HassiumMathModule : InternalModule
    {
        public HassiumMathModule() : base("Math")
        {
            AddAttribute("Math", HassiumMath.TypeDefinition);
            AddAttribute("Random", HassiumRandom.TypeDefinition);
        }
    }
}