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
            this.Attributes.Add("getFileNameWithoutExtension", new InternalFunction(getFileNameWithoutExtension));
            this.Attributes.Add("getFullPath", new InternalFunction(getFullPath));
            this.Attributes.Add("getPathRoot", new InternalFunction(getPathRoot));
            this.Attributes.Add("getRandomFileName", new InternalFunction(getRandomFileName));
            this.Attributes.Add("getTempPath", new InternalFunction(getTempPath));
            this.Attributes.Add("changeExtension", new InternalFunction(changeExtension));
            this.Attributes.Add("isPathRooted", new InternalFunction(isPathRooted));
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

        private HassiumObject getFileNameWithoutExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileNameWithoutExtension(args[0].ToString()));
        }

        private HassiumObject getFullPath(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFullPath(args[0]));
        }

        private HassiumObject getPathRoot(HassiumObject[] args)
        {
            return new HassiumString(Path.GetPathRoot(args[0].ToString()));
        }

        private HassiumObject getRandomFileName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetRandomFileName());
        }

        private HassiumObject getTempPath(HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempPath());
        }

        private HassiumObject changeExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.ChangeExtension(args[0].ToString(), args[1].ToString()));
        }

        private HassiumObject isPathRooted(HassiumObject[] args)
        {
            return new HassiumBool(Path.IsPathRooted(args[0].ToString()));
        }
    }
}

