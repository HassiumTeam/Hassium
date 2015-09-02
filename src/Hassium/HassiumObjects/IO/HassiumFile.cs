using System.IO;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.HassiumObjects.Text;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumFile : HassiumObject
    {
        //public string FilePath { get; set; }

        ///public bool Exists { get { return File.Exists(FilePath); } }

        public HassiumFile()
        {
            this.Attributes.Add("writeText", new InternalFunction(PutContent));
            this.Attributes.Add("readText", new InternalFunction(ReadContent));
            this.Attributes.Add("readLines", new InternalFunction(ReadLines));
            this.Attributes.Add("exists", new InternalFunction(Exists));
            this.Attributes.Add("create", new InternalFunction(Create));
            this.Attributes.Add("append", new InternalFunction(Append));
            this.Attributes.Add("appendLines", new InternalFunction(AppendLines));
            this.Attributes.Add("copy", new InternalFunction(Copy));
            this.Attributes.Add("move", new InternalFunction(Move));
            this.Attributes.Add("rename", new InternalFunction(Rename));
            this.Attributes.Add("deleteFile", new InternalFunction(DeleteFile));
            this.Attributes.Add("deleteDirectory", new InternalFunction(DeleteDirectory));
            this.Attributes.Add("getDirectory", new InternalFunction(GetDirectory));
            this.Attributes.Add("setDirectory", new InternalFunction(SetDirectory));
            this.Attributes.Add("getFiles", new InternalFunction(GetFiles));
            this.Attributes.Add("getDirectories", new InternalFunction(GetDirectories));
            this.Attributes.Add("createText", new InternalFunction(createText));
            this.Attributes.Add("openText", new InternalFunction(openText));
            this.Attributes.Add("getCreationTime", new InternalFunction(getCreationTime));
            this.Attributes.Add("getLastAccessTime", new InternalFunction(getLastAccessTime));
            this.Attributes.Add("getLastWriteTime", new InternalFunction(getLastWriteTime));
            this.Attributes.Add("setLastAccessTime", new InternalFunction(setLastWriteTime));
            this.Attributes.Add("setCreationTime", new InternalFunction(setCreationTime));
        }
            

        public HassiumObject Create(HassiumObject[] args)
        {
            File.Create(args[0].ToString());
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            File.Copy(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            File.Move(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject Exists(HassiumObject[] args)
        {
            return File.Exists(args[0].ToString()) || Directory.Exists(args[0].ToString());
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            File.Move(args[0].ToString(), Path.Combine(Path.GetDirectoryName(args[0].ToString()), args[1].ToString()));
            return null;
        }

        public HassiumObject Append(HassiumObject[] args)
        {
            File.AppendAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject AppendLines(HassiumObject[] args)
        {
            File.AppendAllLines(args[0].ToString(), args[1].HArray().Value.Select(x => x.ToString()));
            return null;
        }

        public HassiumObject PutContent(HassiumObject[] args)
        {
            File.WriteAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject ReadContent(HassiumObject[] args)
        {
            return File.ReadAllText(args[0].ToString());
        }

        public HassiumObject ReadLines(HassiumObject[] args)
        {
            return File.ReadAllLines(args[0].ToString());
        }

        public HassiumObject DeleteFile(HassiumObject[] args)
        {
            File.Delete(args[0].ToString());
            return null;
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

        public HassiumObject createText(HassiumObject[] args)
        {
            return new HassiumTextWriter(File.CreateText(args[0].ToString()));
        }

        public HassiumObject openText(HassiumObject[] args)
        {
            return new HassiumTextReader(File.OpenText(args[0].ToString()));
        }

        public HassiumObject getCreationTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetCreationTime(args[0].ToString()));
        }

        public HassiumObject getLastAccessTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetLastAccessTime(args[0].ToString()));
        }

        public HassiumObject getLastWriteTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetLastWriteTime(args[0].ToString()));
        }

        public HassiumObject setLastAccessTime(HassiumObject[] args)
        {
            File.SetLastAccessTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }

        public HassiumObject setLastWriteTime(HassiumObject[] args)
        {
            File.SetLastWriteTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }

        public HassiumObject setCreationTime(HassiumObject[] args)
        {
            File.SetCreationTime(args[0].ToString(), ((HassiumDate)args[1]).Value);
            return null;
        }
    }
}
