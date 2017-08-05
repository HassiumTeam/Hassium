using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumFloat : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("float");

        public double Float { get; private set; }

        public HassiumFloat(double val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Float = val;

            AddAttribute(ADD, Add, 1);
            AddAttribute(DIVIDE, Divide, 1);
            AddAttribute(EQUALTO, EqualTo, 1);
            AddAttribute(GREATERTHAN, GreaterThan, 1);
            AddAttribute(GREATERTHANOREQUAL, GreaterThanOrEqual, 1);
            AddAttribute(INTEGERDIVISION, IntegerDivision, 1);
            AddAttribute(LESSERTHAN, LesserThan, 1);
            AddAttribute(LESSERTHANOREQUAL, LesserThanOrEqual, 1);
            AddAttribute(MULTIPLY, Multiply, 1);
            AddAttribute(NEGATE, Negate, 0);
            AddAttribute(NOTEQUALTO, NotEqualTo, 1);
            AddAttribute(POWER, Power, 1);
            AddAttribute(SUBTRACT, Subtract, 1);
            AddAttribute(TOFLOAT, ToFloat, 0);
            AddAttribute(TOINT, ToInt, 0);
            AddAttribute(TOSTRING, ToString, 0);
        }

        [FunctionAttribute("func __add__ (num : number) : float")]
        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Float + args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __divide__ (num : number) : float")]
        public override HassiumObject Divide(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Float / args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __equals__ (num : number) : float")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float == args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public override HassiumObject GreaterThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float > args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float >= args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __intdivision__ (num : number) : int")]
        public override HassiumObject IntegerDivision(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((long)Float / args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public override HassiumObject LesserThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float < args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float <= args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __multiply__ (num : number) : float")]
        public override HassiumObject Multiply(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Float * args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __negate__ () : float")]
        public override HassiumObject Negate(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(-Float);
        }

        [FunctionAttribute("func __notequal__ (f : float) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Float != args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func __power__ (pow : number) : float")]
        public override HassiumObject Power(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Pow(Float, args[0].ToFloat(vm, location).Float));
        }

        [FunctionAttribute("func __subtract__ (num : number) : float")]
        public override HassiumObject Subtract(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Float - args[0].ToFloat(vm, location).Float);
        }

        [FunctionAttribute("func tofloat () : float")]
        public override HassiumFloat ToFloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((long)Float);
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Float.ToString());
        }
    }
}
