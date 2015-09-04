﻿using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumDirectory : HassiumObject
    {
        public HassiumDirectory()
        {
            Attributes.Add("exists", new InternalFunction(Exists));
            Attributes.Add("create", new InternalFunction(Create));
            Attributes.Add("copy", new InternalFunction(Copy));
            Attributes.Add("move", new InternalFunction(Move));
            Attributes.Add("rename", new InternalFunction(Rename));
            Attributes.Add("delete", new InternalFunction(Delete));
            Attributes.Add("getCreationTime", new InternalFunction(GetCreationTime));
            Attributes.Add("getLastAccessTime", new InternalFunction(GetLastAccessTime));
            Attributes.Add("getLastWriteTime", new InternalFunction(GetLastWriteTime));
            Attributes.Add("setLastAccessTime", new InternalFunction(SetLastWriteTime));
            Attributes.Add("setLastWriteTime", new InternalFunction(SetLastWriteTime));
            Attributes.Add("setCreationTime", new InternalFunction(SetCreationTime));
            Attributes.Add("getParent", new InternalFunction(GetParent));
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
            Copy(new HassiumObject[]{args[0], args[1].ToString()});
            Directory.Delete(args[0], true);
            args[0] = args[1].ToString();
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            var dname = Path.GetDirectoryName(args[0]);
            if(dname != null) Move(new HassiumObject[] { args[0], Path.Combine(dname, args[1].ToString())});
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
