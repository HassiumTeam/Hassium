using System;

namespace Hassium.Compiler.CodeGen
{
    public class Instruction
    {
        public SourceLocation SourceLocation { get; private set; }
        public InstructionType InstructionType { get; private set; }
        public int Argument { get; private set; }

        public Instruction(SourceLocation location, InstructionType instructionType, int argument = 0)
        {
            SourceLocation = location;
            InstructionType = instructionType;
            Argument = argument;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", InstructionType, Argument);
        }
    }
}

