using Hassium.Runtime;

namespace Hassium.Compiler.Emit
{
    public class HassiumInstruction
    {
        public SourceLocation SourceLocation { get; private set; }

        public InstructionType InstructionType { get; private set; }

        public int Arg { get; private set; }
        public string Constant { get; private set; }
        public HassiumObject Object { get; private set; }

        public HassiumInstruction(SourceLocation location, InstructionType instructionType, int arg = -1, string constant = "", HassiumObject obj = null)
        {
            SourceLocation = location;

            InstructionType = instructionType;

            Arg = arg;
            Constant = constant;
            Object = obj;
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
		EnterWith,
		ExitWith,
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
        StoreGlobal,
        StoreGlobalVariable,
        StoreIterableElement,
        StoreLocal,
        StoreReference,
        Swap,
        UnaryOperation
    }
}
