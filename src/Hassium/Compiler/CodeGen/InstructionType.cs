using System;

namespace Hassium.Compiler.CodeGen
{
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
        LoadListElement,
        LoadLocal,
        Pop,
        PopHandler,
        Push,
        PushConstant,
        PushHandler,
        PushObject,
        Raise,
        Reference,
        Return,
        SelfReference,
        SetInitialAttribute,
        StoreAttribute,
        StoreGlobalVariable,
        StoreListElement,
        StoreLocal,
        StoreReference,
        UnaryOperation
    }
}

