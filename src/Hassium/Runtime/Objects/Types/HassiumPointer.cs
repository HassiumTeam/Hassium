using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumPointer: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("pointer");

        public StackFrame.Frame Frame { get; private set; }
        public new int Index { get; private set; }

        public HassiumPointer(StackFrame.Frame frame, int index)
        {
            AddType(TypeDefinition);
            Frame = frame;
            Index = index;
        }

        public HassiumObject Dereference()
        {
            return Frame.GetVariable(Index);
        }

        public HassiumObject StoreReference(HassiumObject value)
        {
            Frame.Modify(Index, value);
            return value;
        }
    }
}

