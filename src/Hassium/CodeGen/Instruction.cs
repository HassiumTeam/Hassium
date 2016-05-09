using System;

namespace Hassium.CodeGen
{
    public class Instruction
    {
        public InstructionType InstructionType { get; private set; }
        public double Argument { get; private set; }
        public SourceLocation SourceLocation { get; private set; }

        public Instruction(InstructionType instructionType, double argument, SourceLocation location)
        {
            InstructionType = instructionType;
            Argument = argument;
            SourceLocation = location;
        }
    }

    public enum InstructionType
    {
        Binary_Operation,
        Build_Closure,
        Call,
        Create_List,
        Create_Tuple,
        Dup,
        Enumerable_Full,
        Enumerable_Next,
        Enumerable_Reset,
        Jump,
        Jump_If_True,
        Jump_If_False,
        Key_Value_Pair,
        Label,
        Load_Attribute,
        Load_Global,
        Load_List_Element,
        Load_Local,
        Pop,
        Push,
        Push_Bool,
        Push_Object,
        Return,
        Self_Reference,
        Store_Attribute,
        Store_Global,
        Store_Local,
        Store_List_Element,
        Unary_Operation,
    }
}

