using System;
using System.IO;

namespace Hassium.Compiler
{
    public class SourceLocation
    {
        public string File { get; private set; }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public SourceLocation(string file, int row, int column)
        {
            File = file;

            Row = row;
            Column = column;
        }

        public void PrintLocation()
        {
            var stream = new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            PrintLocation(stream);
        }

        public void PrintLocation(Stream stream)
        {
            var reader = new StreamReader(stream);

            string last = string.Empty;
            for (int i = 0; i < Row; i++)
                last = reader.ReadLine();

            Console.WriteLine(last.Replace("\t", "    "));

            for (int i = 1; i < Column - 1; i++)
                Console.Write(' ');
            Console.WriteLine("^");
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", File, Row, Column);
        }
    }
}
