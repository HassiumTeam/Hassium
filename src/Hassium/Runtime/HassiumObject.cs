using System;
using System.Collections.Generic;

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumObject : ICloneable
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("object");

        public bool IsPrivate = false;
        public static HassiumNull Null = new HassiumNull();
        public HassiumClass Parent { get; set; }

        public static HassiumBool False {  get { return InternalModule.InternalModules["Types"].GetAttribute("false") as HassiumBool; } }
        public static HassiumBool True { get { return InternalModule.InternalModules["Types"].GetAttribute("true") as HassiumBool; } }

        public static HassiumTypeDefinition Number = new HassiumTypeDefinition("number");

        public static string INVOKE = "__invoke__";
        public static string ADD = "__add__";
        public static string SUBTRACT = "__subtract__";
        public static string MULTIPLY = "__multiply__";
        public static string DIVIDE = "__divide__";
        public static string MODULUS = "__modulus__";
        public static string POWER = "__power__";
        public static string INTEGERDIVISION = "__intdivision__";
        public static string BITSHIFTLEFT = "__bitshiftleft__";
        public static string BITSHIFTRIGHT = "__bitshiftright__";
        public static string EQUALTO = "__equals__";
        public static string NOTEQUALTO = "__notequal__";
        public static string GREATERTHAN = "__greater__";
        public static string GREATERTHANOREQUAL = "__greaterorequal__";
        public static string LESSERTHAN = "__lesser__";
        public static string LESSERTHANOREQUAL = "__lesserorequal__";
        public static string BITWISEAND = "__bitwiseand__";
        public static string BITWISEOR = "__bitwiseor__";
        public static string BITWISENOT = "__bitwisenot__";
        public static string LOGICALAND = "__logicaland__";
        public static string LOGICALOR = "__logicalor__";
        public static string LOGICALNOT = "__logicalnot__";
        public static string NEGATE = "__negate__";
        public static string INDEX = "__index__";
        public static string STOREINDEX = "__storeindex__";
        public static string ITER = "__iter__";
        public static string ITERABLEFULL = "__iterfull__";
        public static string ITERABLENEXT = "__iternext__";
        public static string DISPOSE = "dispose";
        public static string TOBIGINT = "tobigint";
        public static string TOBOOL = "tobool";
        public static string TOCHAR = "tochar";
        public static string TOINT = "toint";
        public static string TOFLOAT = "tofloat";
        public static string TOLIST = "tolist";
        public static string TOSTRING = "tostring";
        public static string TOTUPLE = "totuple";
        public static string XOR = "__xor__";

        public Dictionary<string, HassiumObject> Attributes = new Dictionary<string, HassiumObject>();

        public List<HassiumTypeDefinition> Types = new List<HassiumTypeDefinition>()
        {
            TypeDefinition
        };
        
        public HassiumTypeDefinition Type()
        {
            return Types[Types.Count - 1] as HassiumTypeDefinition;
        }

        public void AddAttribute(string name, HassiumObject value)
        {
            if (Attributes.ContainsKey(name))
                Attributes.Remove(name);
            Attributes.Add(name, value);
        }
        public void AddAttribute(string name, HassiumFunctionDelegate func, params int[] paramLengths)
        {
            AddAttribute(name, new HassiumFunction(func, paramLengths));
        }
        public void AddAttribute(string name, HassiumFunctionDelegate func, int paramLength = -1)
        {
            AddAttribute(name, func, new int[] { paramLength });
        }

        public HassiumObject GetAttribute(string name)
        {
            var ret = Attributes[name];
            if (ret is HassiumFunction)
                (ret as HassiumFunction).Self = this;
            else if (ret is HassiumProperty)
            {
                var prop = (ret as HassiumProperty);
                (prop.Get as HassiumFunction).Self = this;
                if (prop.Set != null)
                    (prop.Set as HassiumFunction).Self = this;
            }
            return ret;
        }

        public HassiumObject AddType(HassiumTypeDefinition typeDefinition)
        {
            Types.Add(typeDefinition);
            return this;
        }

        public virtual HassiumObject Add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(ADD))
                return GetAttribute(ADD).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(ADD)));
            return Null;
        }
        public virtual HassiumObject Subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(SUBTRACT))
                return GetAttribute(SUBTRACT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location,this, new HassiumString(SUBTRACT)));
            return Null;
        }
        public virtual HassiumObject Multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(MULTIPLY))
                return GetAttribute(MULTIPLY).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(MULTIPLY)));
            return Null;
        }
        public virtual HassiumObject Divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(DIVIDE))
                return GetAttribute(DIVIDE).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(DIVIDE)));
            return Null;
        }
        public virtual HassiumObject Modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(MODULUS))
                return GetAttribute(MODULUS).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(MODULUS)));
            return Null;
        }
        public virtual HassiumObject Power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(POWER))
                return GetAttribute(POWER).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(POWER)));
            return Null;
        }
        public virtual HassiumObject IntegerDivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(INTEGERDIVISION))
                return GetAttribute(INTEGERDIVISION).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(INTEGERDIVISION)));
            return Null;
        }
        public virtual HassiumObject BitshiftLeft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(BITSHIFTLEFT))
                return GetAttribute(BITSHIFTLEFT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(BITSHIFTLEFT)));
            return Null;
        }
        public virtual HassiumObject BitshiftRight(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(BITSHIFTRIGHT))
                return GetAttribute(BITSHIFTRIGHT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(BITSHIFTRIGHT)));
            return Null;
        }
        public virtual HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(EQUALTO))
                return GetAttribute(EQUALTO).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(EQUALTO)));
            return False;
        }
        public virtual HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(NOTEQUALTO))
                return GetAttribute(NOTEQUALTO).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(NOTEQUALTO)));
            return False;
        }
        public virtual HassiumObject GreaterThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(GREATERTHAN))
                return GetAttribute(GREATERTHAN).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(GREATERTHAN)));
            return Null;
        }
        public virtual HassiumObject GreaterThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(GREATERTHANOREQUAL))
                return GetAttribute(GREATERTHANOREQUAL).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(GREATERTHANOREQUAL)));
            return Null;
        }
        public virtual HassiumObject LesserThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(LESSERTHAN))
                return GetAttribute(LESSERTHAN).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(LESSERTHAN)));
            return Null;
        }
        public virtual HassiumObject LesserThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(LESSERTHANOREQUAL))
                return GetAttribute(LESSERTHANOREQUAL).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(LESSERTHANOREQUAL)));
            return Null;
        }
        public virtual HassiumObject BitwiseAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(BITWISEAND))
                return GetAttribute(BITWISEAND).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(BITWISEAND)));
            return Null;
        }
        public virtual HassiumObject BitwiseOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(BITWISEOR))
                return GetAttribute(BITWISEOR).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(BITWISEOR)));
            return Null;
        }
        public virtual HassiumObject BitwiseNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(BITWISENOT))
                return GetAttribute(BITWISENOT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(BITWISENOT)));
            return Null;
        }
        public virtual HassiumObject LogicalAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(LOGICALAND))
                return GetAttribute(LOGICALAND).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(LOGICALAND)));
            return Null;
        }
        public virtual HassiumObject LogicalOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(LOGICALOR))
                return GetAttribute(LOGICALOR).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(LOGICALOR)));
            return Null;
        }
        public virtual HassiumObject LogicalNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(LOGICALNOT))
                return GetAttribute(LOGICALNOT).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(LOGICALNOT)));
            return Null;
        }
        public virtual HassiumObject Negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(NEGATE))
                return GetAttribute(NEGATE).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(NEGATE)));
            return Null;
        }
        public virtual HassiumObject Index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(INDEX))
                return GetAttribute(INDEX).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(INDEX)));
            return Null;
        }
        public virtual HassiumObject StoreIndex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(STOREINDEX))
                return GetAttribute(STOREINDEX).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(STOREINDEX)));
            return Null;
        }
        public virtual HassiumObject Iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(ITER))
                return GetAttribute(ITER).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(ITER)));
            return Null;
        }
        public virtual HassiumObject IterableFull(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(ITERABLEFULL))
                return GetAttribute(ITERABLEFULL).Invoke(vm, location, args).ToBool(vm, self, location);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(ITERABLEFULL)));
            return Null;
        }
        public virtual HassiumObject IterableNext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(ITERABLENEXT))
                return GetAttribute(ITERABLENEXT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(ITERABLENEXT)));
            return Null;
        }
        public virtual HassiumObject Dispose(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(DISPOSE))
                return GetAttribute(DISPOSE).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(DISPOSE)));
            return Null;
        }
        public virtual HassiumBigInt ToBigInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOBIGINT))
                return GetAttribute(TOBIGINT).Invoke(vm, location, args) as HassiumBigInt;
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOBIGINT)));
            return HassiumBigInt._new(vm, self, location, new HassiumInt(-1));
        }
        public virtual HassiumBool ToBool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOBOOL))
                return (HassiumBool)GetAttribute(TOBOOL).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOBOOL)));
            return False;
        }
        public virtual HassiumChar ToChar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOCHAR))
                return (HassiumChar)GetAttribute(TOCHAR).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOCHAR)));
            return new HassiumChar('\0');
        }
        public virtual HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOINT))
                return (HassiumInt)GetAttribute(TOINT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOINT)));
            return new HassiumInt(-1);
        }
        public virtual HassiumFloat ToFloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOFLOAT))
                return (HassiumFloat)GetAttribute(TOFLOAT).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOFLOAT)));
            return new HassiumFloat(-1);
        }
        public virtual HassiumList ToList(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOLIST))
                return (HassiumList)GetAttribute(TOLIST).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOLIST)));
            return new HassiumList(new HassiumObject[0]);
        }
        public virtual HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOSTRING))
                return GetAttribute(TOSTRING).Invoke(vm, location, args) as HassiumString;
            return new HassiumString(Type().TypeName);
        }
        public virtual HassiumTuple ToTuple(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(TOTUPLE))
                return (HassiumTuple)GetAttribute(TOTUPLE).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(TOTUPLE)));
            return new HassiumTuple(new HassiumObject[0]);
        }
        
        public virtual HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(INVOKE))
                return GetAttribute(INVOKE).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(INVOKE)));
            return Null;
        }
        
        public virtual HassiumObject Xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey(XOR))
                return GetAttribute(XOR).Invoke(vm, location, args);
            vm.RaiseException(HassiumAttribNotFoundException.Attribs[INVOKE].Invoke(vm, location, this, new HassiumString(XOR)));
            return Null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static byte[] ListToByteArr(VirtualMachine vm, SourceLocation location, HassiumList list)
        {
            if (list is HassiumByteArray)
                return (list as HassiumByteArray).Values.ToArray();
            byte[] bytes = new byte[list.Values.Count];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list.Values[i].ToChar(vm, list.Values[i], location).Char;

            return bytes;
        }
    }
}
