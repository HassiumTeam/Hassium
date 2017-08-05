using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Emit
{
    public class HassiumInstruction
    {
        public SourceLocation SourceLocation { get; private set; }

        public InstructionType InstructionType { get; private set; }
        public int Arg { get; private set; }

        public HassiumInstruction(SourceLocation location, InstructionType instructionType, int arg)
        {
            SourceLocation = location;

            InstructionType = instructionType;
            Arg = arg;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", InstructionType, Arg);
        }
    }

    public enum InstructionType
    {
        BinaryOperation,
        BuildClosure,
        BuildDictionary,
        BuildKeyValuePair,
        BuildLabel,
        BuildList,
        BuildThread,
        BuildTuple,
        Call,
        Dereference,
        Dispose,
        Duplicate,
        EnforcedAssignment,
        Goto,
        Iter,
        IterableFull,
        IterableNext,
        Jump,
        JumpIfFalse,
        JumpIfTrue,
        Label,
        LoadAttribute,
        LoadGlobal,
        LoadGlobalVariable,
        LoadIterableElement,
        LoadLocal,
        multipleAssignment,
        Pop,
        PopHandler,
        Push,
        PushConstant,
        PushHandler,
        PushObject,
        Raise,
        Reference,
        Return,
        StartThread,
        SelfReference,
        SetInitialAttribute,
        StoreAttribute,
        StoreGlobalVariable,
        StoreIterableElement,
        StoreLocal,
        StoreReference,
        Swap,
        UnaryOperation
    }
}
