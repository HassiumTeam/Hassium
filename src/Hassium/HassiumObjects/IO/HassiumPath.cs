using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumPath : HassiumObject
    {
        public HassiumPath()
        {
            Attributes.Add("combine", new InternalFunction(combine, 2));
            Attributes.Add("getDirectoryName", new InternalFunction(getDirectoryName, 1));
            Attributes.Add("getExtension", new InternalFunction(getExtension, 1));
            Attributes.Add("getFileName", new InternalFunction(getFileName, 1));
            Attributes.Add("getFileNameWithoutExtension", new InternalFunction(getFileNameWithoutExtension, 1));
            Attributes.Add("getFullPath", new InternalFunction(getFullPath, 1));
            Attributes.Add("getPathRoot", new InternalFunction(getPathRoot, 1));
            Attributes.Add("getRandomFileName", new InternalFunction(getRandomFileName, 0));
            Attributes.Add("getTempPath", new InternalFunction(getTempPath, 0));
            Attributes.Add("changeExtension", new InternalFunction(changeExtension, 2));
            Attributes.Add("isPathRooted", new InternalFunction(isPathRooted, 1));
        }

        public HassiumObject combine(HassiumObject[] args)
        {
            return new HassiumString(Path.Combine(args[0], args[1]));
        }

        public HassiumObject getDirectoryName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetDirectoryName(args[0]));
        }

        public HassiumObject getExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.GetExtension(args[0]));
        }

        public HassiumObject getFileName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(args[0]));
        }

        public HassiumObject getFileNameWithoutExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileNameWithoutExtension(args[0].ToString()));
        }

        public HassiumObject getFullPath(HassiumObject[] args)
        {
            return new HassiumString(Path.GetFullPath(args[0]));
        }

        public HassiumObject getPathRoot(HassiumObject[] args)
        {
            return new HassiumString(Path.GetPathRoot(args[0].ToString()));
        }

        public HassiumObject getRandomFileName(HassiumObject[] args)
        {
            return new HassiumString(Path.GetRandomFileName());
        }

        public HassiumObject getTempPath(HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempPath());
        }

        public HassiumObject changeExtension(HassiumObject[] args)
        {
            return new HassiumString(Path.ChangeExtension(args[0].ToString(), args[1].ToString()));
        }

        public HassiumObject isPathRooted(HassiumObject[] args)
        {
            return new HassiumBool(Path.IsPathRooted(args[0].ToString()));
        }
    }
}