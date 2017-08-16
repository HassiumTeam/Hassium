using Hassium.Runtime;

namespace Hassium.Compiler
{
    public class HassiumWarning
    {
        public static bool CheckCasing(string identifier, HassiumCasingType casing)
        {
            switch (casing)
            {
                case HassiumCasingType.Camel:
                    return char.IsLower(identifier[0]);
                case HassiumCasingType.Lower:
                    return identifier == identifier.ToLower();
                case HassiumCasingType.Pascal:
                    return char.IsUpper(identifier[0]);
                default:
                    return false;
            }
        }

        public static void EnforceCasing(HassiumModule module, SourceLocation location, string identifier, HassiumCasingType casing)
        {
            if (CheckCasing(identifier, casing))
                return;

            switch (casing)
            {
                case HassiumCasingType.Camel:
                    module.AddWarning(location, "Expected casing type 'camelCase'!");
                    break;
                case HassiumCasingType.Lower:
                    module.AddWarning(location, "Expected casing type 'lowercase' for locals, funcs, and properties!");
                    break;
                case HassiumCasingType.Pascal:
                    module.AddWarning(location, "Expected casing type 'PascalCase' for classes, traits, and enums!");
                    break;
            }
        }

        public SourceLocation SourceLocation { get; private set; }
        public string WarningMessage { get; private set; }

        public HassiumWarning(SourceLocation location, string message)
        {
            SourceLocation = location;
            WarningMessage = message;
        }
    }

    public enum HassiumCasingType
    {
        Camel,
        Lower,
        Pascal
    }
}
