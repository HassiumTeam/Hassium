using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumInt: HassiumObject
    {
        public new Int64 Value { get; private set; }

        public static HassiumInt Create(HassiumObject obj)
        {
            if (!(obj is HassiumInt))
                throw new InternalException(string.Format("Cannot convert from {0} to HassiumInt!", obj.GetType().Name));
            return (HassiumInt)obj;
        }

        public HassiumInt(Int64 value)
        {
            Value = value;
            Attributes.Add("toBool",    new HassiumFunction(toBool, 0));
            Attributes.Add("toChar",    new HassiumFunction(toChar, 0));
            Attributes.Add("toDouble",  new HassiumFunction(toDouble, 0));
            Attributes.Add("toInt",     new HassiumFunction(toInt, 0));
            Attributes.Add(HassiumObject.ADD_FUNCTION,              new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.SUB_FUNCTION,              new HassiumFunction(__sub__, 1));
            Attributes.Add(HassiumObject.MUL_FUNCTION,              new HassiumFunction(__mul__, 1));
            Attributes.Add(HassiumObject.DIV_FUNCTION,              new HassiumFunction(__div__, 1));
            Attributes.Add(HassiumObject.MOD_FUNCTION,              new HassiumFunction(__mod__, 1));
            Attributes.Add(HassiumObject.XOR_FUNCTION,              new HassiumFunction(__xor__, 1));
            Attributes.Add(HassiumObject.OR_FUNCTION,               new HassiumFunction(__or__, 1));
            Attributes.Add(HassiumObject.XAND_FUNCTION,             new HassiumFunction(__xand__, 1));
            Attributes.Add(HassiumObject.EQUALS_FUNCTION,           new HassiumFunction(__equals__, 1));
            Attributes.Add(HassiumObject.NOT_EQUAL_FUNCTION,        new HassiumFunction(__notequal__, 1));
            Attributes.Add(HassiumObject.GREATER_FUNCTION,          new HassiumFunction(__greater__, 1));
            Attributes.Add(HassiumObject.GREATER_OR_EQUAL_FUNCTION, new HassiumFunction(__greaterorequal__, 1));
            Attributes.Add(HassiumObject.LESSER_FUNCTION,           new HassiumFunction(__lesser__, 1));
            Attributes.Add(HassiumObject.LESSER_OR_EQUAL_FUNCTION,  new HassiumFunction(__lesserorequal__, 1));
            Attributes.Add(HassiumObject.ENUMERABLE_FULL,           new HassiumFunction(__enumerablefull__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_NEXT,           new HassiumFunction(__enumerablenext__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_RESET,          new HassiumFunction(__enumerablereset__, 0));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION,         new HassiumFunction(__tostring__, 0));
            Types.Add(this.GetType().Name);
        }

        private HassiumBool toBool(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Value == 1);
        }
        private HassiumChar toChar(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(Convert.ToChar(Value));
        }
        private HassiumDouble toDouble(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(Value);
        }
        private HassiumInt toInt(VirtualMachine vm, HassiumObject[] args)
        {
            return this;
        }

        private HassiumObject __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumDouble(Value + ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumInt(Value + ((HassiumInt)args[0]).Value);
            else if (args[0] is HassiumString)
                return new HassiumString(Value + args[0].ToString(vm));
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __sub__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumDouble(Value - ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumInt(Value - ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __mul__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumDouble(Value * ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumInt(Value * ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __div__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumDouble(Value / ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumInt(Value / ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __mod__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumDouble(Value % ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumInt(Value % ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __xor__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value ^ HassiumInt.Create(args[0]).Value);
        }
        private HassiumObject __or__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value | HassiumInt.Create(args[0]).Value);
        }
        private HassiumObject __xand__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value & HassiumInt.Create(args[0]).Value);
        }
        private HassiumObject __equals__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value == ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value == ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __notequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value != ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value != ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __greater__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value > ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value > ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __greaterorequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value >= ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value >= ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __lesser__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value < ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value < ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private HassiumObject __lesserorequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumDouble)
                return new HassiumBool(Value <= ((HassiumDouble)args[0]).Value);
            else if (args[0] is HassiumInt)
                return new HassiumBool(Value <= ((HassiumInt)args[0]).Value);
            throw new InternalException("Cannot operate HassiumInt on " + args[0].GetType().Name);
        }
        private int enumerableIndex = 0;
        private HassiumBool __enumerablefull__(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(enumerableIndex >= Value);
        }
        private HassiumInt __enumerablenext__(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(enumerableIndex++);
        }
        private HassiumNull __enumerablereset__(VirtualMachine vm, HassiumObject[] args)
        {
            enumerableIndex = 0;
            return HassiumObject.Null;
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}