using System;
using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class DocStrAttribute : Attribute
    {
        public string Description { get; private set; }

        public List<string> RequiredParams { get; private set; }
        public List<string> OptionalParams { get; private set; }

        public string Returns { get; private set; }

        public DocStrAttribute(params string[] lines)
        {
            RequiredParams = new List<string>();
            OptionalParams = new List<string>();

            int index = 0;

            Description = lines[index++];
            while (lines[index].Trim().StartsWith("@param"))
                RequiredParams.Add(lines[index++]);
            while (lines[index].Trim().StartsWith("@optional"))
                OptionalParams.Add(lines[index++]);
            Returns = lines[index];
        }
    }
}
