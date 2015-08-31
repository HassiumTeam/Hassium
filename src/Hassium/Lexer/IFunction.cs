using System;
using System.Collections.Generic;
using Hassium.HassiumObjects;

namespace Hassium
{
    public interface IFunction
    {
        HassiumObject Invoke(HassiumObject[] args);
    }
}

