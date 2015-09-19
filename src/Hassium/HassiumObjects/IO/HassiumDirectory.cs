using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumDirectory : HassiumObject
    {
        public HassiumDirectory()
        {
            Attributes.Add("exists", new InternalFunction(Exists, 1));
            Attributes.Add("create", new InternalFunction(Create, 1));
            Attributes.Add("copy", new InternalFunction(Copy, 2));
            Attributes.Add("move", new InternalFunction(Move, 2));
            Attributes.Add("rename", new InternalFunction(Rename, 2));
            Attributes.Add("delete", new InternalFunction(Delete, 1));
            Attributes.Add("getCreationTime", new InternalFunction(GetCreationTime, 1));
            Attributes.Add("getLastAccessTime", new InternalFunction(GetLastAccessTime, 1));
            Attributes.Add("getLastWriteTime", new InternalFunction(GetLastWriteTime, 1));
            Attributes.Add("setLastAccessTime", new InternalFunction(SetLastWriteTime, 2));
            Attributes.Add("setLastWriteTime", new InternalFunction(SetLastWriteTime, 2));
            Attributes.Add("setCreationTime", new InternalFunction(SetCreationTime, 2));
            Attributes.Add("getParent", new InternalFunction(GetParent, 1));
            Attributes.Add("deleteDirectory", new InternalFunction(DeleteDirectory, 1));
            Attributes.Add("getDirectory", new InternalFunction(GetDirectory, 0));
            Attributes.Add("setDirectory", new InternalFunction(SetDirectory, 1));
            Attributes.Add("getFiles", new InternalFunction(GetFiles, 1));
            Attributes.Add("getDirectories", new InternalFunction(GetDirectories, 1));
        }

        public HassiumObject Exists(HassiumObject[] args)
        {
            return new HassiumBool(Directory.Exists(args[0]));
        }

        public HassiumObject Create(HassiumObject[] args)
        {
            Directory.CreateDirectory(args[0]);
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            var dest = args[1].ToString();

            if (dest[dest.Length - 1] != Path.DirectorySeparatorChar)
                dest += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
            foreach (string Element in Directory.GetFileSystemEntries(args[0]))
            {
                var fname = Path.GetFileName(Element);
                if (fname != null)
                {
                    if (Directory.Exists(Element))
                        Copy(new HassiumObject[] {Element, Path.Combine(dest, fname)});
                    else
                        File.Copy(Element, Path.Combine(dest, fname), true);
                }
            }

            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            Copy(new HassiumObject[] {args[0], args[1].ToString()});
            Directory.Delete(args[0], true);
            args[0] = args[1].ToString();
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            var dname = Path.GetDirectoryName(args[0]);
            if (dname != null) Move(new HassiumObject[] {args[0], Path.Combine(dname, args[1].ToString())});
            return null;
        }

        public HassiumObject Delete(HassiumObject[] args)
        {
            Directory.Delete(args[0]);
            return null;
        }

        public HassiumObject GetCreationTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetCreationTime(args[0].ToString()));
        }

        public HassiumObject GetLastAccessTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetLastAccessTime(args[0].ToString()));
        }

        public HassiumObject GetLastWriteTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetLastWriteTime(args[0].ToString()));
        }

        public HassiumObject SetLastAccessTime(HassiumObject[] args)
        {
            Directory.SetLastAccessTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject SetLastWriteTime(HassiumObject[] args)
        {
            Directory.SetLastWriteTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject SetCreationTime(HassiumObject[] args)
        {
            Directory.SetCreationTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject GetParent(HassiumObject[] args)
        {
            return new HassiumString(Directory.GetParent(args[0].ToString()).ToString());
        }

        public HassiumObject DeleteDirectory(HassiumObject[] args)
        {
            Directory.Delete(args[0].ToString());
            return null;
        }

        public HassiumObject GetDirectory(HassiumObject[] args)
        {
            return Directory.GetCurrentDirectory();
        }

        public HassiumObject SetDirectory(HassiumObject[] args)
        {
            Directory.SetCurrentDirectory(args[0].ToString());
            return null;
        }

        public HassiumObject GetFiles(HassiumObject[] args)
        {
            return Directory.GetFiles(args[0].ToString());
        }

        public HassiumObject GetDirectories(HassiumObject[] args)
        {
            return Directory.GetDirectories(args[0].ToString());
        }
    }
}