using System;
using System.Collections.Generic;

namespace Hassium
{
    public interface IFunction
    {
        HassiumObject Invoke(HassiumObject[] args);
    }
}

