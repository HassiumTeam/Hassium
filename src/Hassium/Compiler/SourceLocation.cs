using System;

namespace Hassium.Compiler
{
    public class SourceLocation
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public SourceLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Row, Column);
        }
    }
}

