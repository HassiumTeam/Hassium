using System.IO;
using System.Linq;

namespace Hassium.HassiumObjects
{
    public class HassiumFile : HassiumObject
    {
        public string FilePath { get; set; }

        public bool Exists { get { return File.Exists(FilePath); } }

        public HassiumFile(string fpath)
        {
            FilePath = fpath;
            this.Attributes.Add("puts", new InternalFunction(PutContent));
            this.Attributes.Add("readf", new InternalFunction(ReadContent));
            this.Attributes.Add("readfarr", new InternalFunction(ReadLines));
            this.Attributes.Add("exists", new InternalFunction(x => Exists, true));
            this.Attributes.Add("create", new InternalFunction(Create));
            this.Attributes.Add("append", new InternalFunction(Append));
            this.Attributes.Add("appendarr", new InternalFunction(AppendLines));
            this.Attributes.Add("copy", new InternalFunction(Copy));
            this.Attributes.Add("move", new InternalFunction(Move));
            this.Attributes.Add("rename", new InternalFunction(Rename));
            this.Attributes.Add("delete", new InternalFunction(Delete));
        }

        public HassiumFile(HassiumObject[] args) : this(args[0].ToString())
        {
        }

        public HassiumObject Create(HassiumObject[] args)
        {
            File.Create(FilePath);
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            File.Copy(FilePath, args[0].ToString());
            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            File.Move(FilePath, args[0].ToString());
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            File.Move(FilePath, Path.Combine(Path.GetDirectoryName(FilePath), args[0].ToString()));
            return null;
        }

        public HassiumObject Append(HassiumObject[] args)
        {
            File.AppendAllText(FilePath, args[0].ToString());
            return null;
        }

        public HassiumObject AppendLines(HassiumObject[] args)
        {
            File.AppendAllLines(FilePath, args[0].HArray().Value.Select(x => x.ToString()));
            return null;
        }

        public HassiumObject PutContent(HassiumObject[] args)
        {
            File.WriteAllText(FilePath, args[0].ToString());
            return null;
        }

        public HassiumObject ReadContent(HassiumObject[] args)
        {
            return File.ReadAllText(FilePath);
        }

        public HassiumObject ReadLines(HassiumObject[] args)
        {
            return File.ReadAllLines(FilePath);
        }

        public HassiumObject Delete(HassiumObject[] args)
        {
            File.Delete(FilePath);
            return null;
        }
    }
}
