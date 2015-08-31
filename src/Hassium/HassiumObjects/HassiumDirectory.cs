using System;
using System.IO;
using System.Linq;

namespace Hassium.HassiumObjects
{
    public class HassiumDirectory : HassiumObject
    {
        public string FullPath { get; set; }

        public bool Exists { get { return Directory.Exists(FullPath); } }

        public HassiumDirectory(string fpath)
        {
            FullPath = fpath;
            this.Attributes.Add("exists", new InternalFunction(x => Exists, true));
            this.Attributes.Add("create", new InternalFunction(Create));
            this.Attributes.Add("copy", new InternalFunction(Copy));
            this.Attributes.Add("move", new InternalFunction(Move));
            this.Attributes.Add("rename", new InternalFunction(Rename));
            this.Attributes.Add("delete", new InternalFunction(Delete));
        }

        public HassiumObject Create(HassiumObject[] args)
        {
            Directory.CreateDirectory(FullPath);
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            var dest = args[0].ToString();

            if (dest[dest.Length - 1] != Path.DirectorySeparatorChar)
                dest += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
            foreach (string Element in Directory.GetFileSystemEntries(FullPath))
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
            Copy(new HassiumObject[]{FullPath, args[0].ToString()});
            Directory.Delete(FullPath, true);
            FullPath = args[0].ToString();
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            Move(new HassiumObject[] { FullPath, Path.Combine(Path.GetDirectoryName(FullPath), args[0].ToString())});
            return null;
        }

        public HassiumObject Delete(HassiumObject[] args)
        {
            Directory.Delete(FullPath);
            return null;
        }
    }
}
