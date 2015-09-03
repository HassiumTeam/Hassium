using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumDirectory : HassiumObject
    {
        public HassiumDirectory()
        {
            this.Attributes.Add("exists", new InternalFunction(Exists));
            this.Attributes.Add("create", new InternalFunction(Create));
            this.Attributes.Add("copy", new InternalFunction(Copy));
            this.Attributes.Add("move", new InternalFunction(Move));
            this.Attributes.Add("rename", new InternalFunction(Rename));
            this.Attributes.Add("delete", new InternalFunction(Delete));
            this.Attributes.Add("getCreationTime", new InternalFunction(GetCreationTime));
            this.Attributes.Add("getLastAccessTime", new InternalFunction(GetLastAccessTime));
            this.Attributes.Add("getLastWriteTime", new InternalFunction(GetLastWriteTime));
            this.Attributes.Add("setLastAccessTime", new InternalFunction(SetLastWriteTime));
            this.Attributes.Add("setLastWriteTime", new InternalFunction(SetLastWriteTime));
            this.Attributes.Add("setCreationTime", new InternalFunction(SetCreationTime));
            this.Attributes.Add("getParent", new InternalFunction(GetParent));
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
                if (Directory.Exists(Element))
                    Copy(new HassiumObject[] {Element, Path.Combine(dest, Path.GetFileName(Element))});
                else
                    File.Copy(Element, Path.Combine(dest, Path.GetFileName(Element)), true);
            }

            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            Copy(new HassiumObject[]{args[0], args[1].ToString()});
            Directory.Delete(args[0], true);
            args[0] = args[1].ToString();
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            Move(new HassiumObject[] { args[0], Path.Combine(Path.GetDirectoryName(args[0]), args[1].ToString())});
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
            Directory.SetLastAccessTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }

        public HassiumObject SetLastWriteTime(HassiumObject[] args)
        {
            Directory.SetLastWriteTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }

        public HassiumObject SetCreationTime(HassiumObject[] args)
        {
            Directory.SetCreationTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }

        public HassiumObject GetParent(HassiumObject[] args)
        {
            return new HassiumString(Directory.GetParent(args[0].ToString()).ToString());
        }
    }
}
