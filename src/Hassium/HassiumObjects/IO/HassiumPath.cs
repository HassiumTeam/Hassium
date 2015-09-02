using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumPath: HassiumObject
    {
        public HassiumPath()
        {
            this.Attributes.Add("combine", new InternalFunction(combine));
            this.Attributes.Add("getDirectoryName", new InternalFunction(getDirectoryName));
            this.Attributes.Add("getExtension", new InternalFunction(getExtension));
            this.Attributes.Add("getFileName", new InternalFunction(getFileName));
            this.Attributes.Add("getFullPath", new InternalFunction(getFullPath));
        }

        private HassiumObject combine(HassiumObject[] args)
        {
            return new HassiumString(Path.Combine(args[0], args[1]));
        }

        private HassiumObject getDirectoryName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetDirectoryName(args[0]));
        }

        private HassiumObject getExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.GetExtension(args[0]));
        }

        private HassiumObject getFileName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(args[0]));
        }

        private HassiumObject getFullPath(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFullPath(args[0]));
        }
    }
}

