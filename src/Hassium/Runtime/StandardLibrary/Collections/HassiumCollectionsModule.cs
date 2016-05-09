using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Collections
{
    public class HassiumCollectionsModule : InternalModule
    {
        public HassiumCollectionsModule() : base("Collections")
        {
            Attributes.Add("Stack", new HassiumStack());
        }
    }
}

