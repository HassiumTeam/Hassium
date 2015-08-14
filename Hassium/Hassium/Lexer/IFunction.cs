using System;
using System.Collections.Generic;

namespace Hassium
{
    public interface IFunction
    {
        string Main(List<Token> line);
    }
}

