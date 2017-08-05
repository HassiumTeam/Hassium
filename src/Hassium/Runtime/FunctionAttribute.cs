using System;
using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class FunctionAttribute : Attribute
    {
        public string SourceRepresentation { get { return SourceRepresentations[0]; } }

        public List<string> SourceRepresentations { get; private set; }

        public FunctionAttribute(params string[] sourceRep)
        {
            SourceRepresentations = new List<string>(sourceRep);
        }
    }
}
