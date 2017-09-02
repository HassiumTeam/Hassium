using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumModule : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Module");

        public Dictionary<int, HassiumObject> Globals { get; private set; }
        public List<HassiumWarning> Warnings { get; private set; }

        public HassiumModule()
        {
            Globals = new Dictionary<int, HassiumObject>();
            Warnings = new List<HassiumWarning>();

            AddType(TypeDefinition);
        }

        public void AddWarning(SourceLocation location, string message)
        {
            Warnings.Add(new HassiumWarning(location, message));
        }

        public void DisplayWarnings()
        {
            foreach (var warning in Warnings)
                Console.WriteLine("--- Warning at [{0}], {1} ---", warning.SourceLocation, warning.WarningMessage);

            if (Warnings.Count > 0)
                Console.WriteLine("\n");
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            vm.Execute(this, new HassiumList(args));
            return Null;
        }
    }
}
