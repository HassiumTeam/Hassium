using System.IO;
using System.Linq;

namespace Hassium.HassiumObjects
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
            this.Attributes.Add("delete", new InternalFunction(Delete));
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
            return File.Exists(args[0].ToString());
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

        public HassiumObject Delete(HassiumObject[] args)
        {
            File.Delete(args[0].ToString());
            return null;
        }
    }
}
