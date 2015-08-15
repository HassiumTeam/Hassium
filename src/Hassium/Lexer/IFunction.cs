using System;
using System.Collections.Generic;

namespace Hassium
{
    public interface IFunction
    {
        object Invoke(object[] args);
    }
}

