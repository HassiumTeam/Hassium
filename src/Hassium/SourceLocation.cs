using System;

namespace Hassium
{
    public class SourceLocation
    {
        public int Line { get; private set; }
        public int Letter { get; private set; }
        public SourceLocation(int line, int letter)
        {
            Line = line;
            Letter = letter;
        }

        public override string ToString()
        {
            return string.Format("[SourceLocation: Line={0}, Letter={1}]", Line, Letter);
        }
    }
}

