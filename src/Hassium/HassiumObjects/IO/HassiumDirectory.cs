using System.IO;
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
    }
}
