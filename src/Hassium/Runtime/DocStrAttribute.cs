using System;
using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class DocStrAttribute : Attribute
    {
        public string Author { get; private set; }
        public string Description { get; private set; }

        public List<string> RequiredParams { get; private set; }
        public List<string> OptionalParams { get; private set; }

        public string Returns { get; private set; }

        public DocStrAttribute(params string[] lines)
        {
            Description = string.Empty;

            RequiredParams = new List<string>();
            OptionalParams = new List<string>();

            Returns = string.Empty;

            foreach (var line in lines)
                if (line.Trim().StartsWith("@author"))
                    Author = line.Trim();
                else if (line.Trim().StartsWith("@desc"))
                    Description = line.Trim();
                else if (line.Trim().StartsWith("@param"))
                    RequiredParams.Add(line.Trim());
                else if (line.Trim().StartsWith("@optional"))
                    OptionalParams.Add(line.Trim());
                else if (line.Trim().StartsWith("@returns"))
                    Returns = line.Trim();
        }
    }
}
