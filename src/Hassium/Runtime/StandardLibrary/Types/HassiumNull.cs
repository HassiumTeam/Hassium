using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumNull: HassiumObject
    {
        public HassiumNull()
        {
            Types.Add(this.GetType().Name);
        }
    }
}

