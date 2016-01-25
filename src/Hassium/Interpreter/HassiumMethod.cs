﻿// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Lexer;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    /// <summary>
    /// Class for a HassiumMethod.
    /// </summary>
    public class HassiumMethod : HassiumObject
    {
        /// <summary>
        /// Implements 'this'.
        /// </summary>
        public HassiumObject SelfReference { get; set; }
        /// <summary>
        /// The interpreter being used.
        /// </summary>
        public Interpreter Interpreter;
        /// <summary>
        /// The local scope.
        /// </summary>
        public LocalScope LocalScope;
        /// <summary>
        /// The AST Node.
        /// </summary>
        public FuncNode FuncNode;
        /// <summary>
        /// The stack frame.
        /// </summary>
        public StackFrame stackFrame;

        /// <summary>
        /// Returns the name of the FuncNode.
        /// </summary>
        public string Name
        {
            get { return FuncNode.Name; }
        }

        /// <summary>
        /// Returns if function is a lambda.
        /// </summary>
        public bool IsLambda { get; private set; }

        /// <summary>
        /// Returns if function is static.
        /// </summary>
        public bool IsStatic
        {
            get { return !FuncNode.Parameters.Contains("this"); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Returns if function is a constructor
        /// </summary>
        public bool IsConstructor
        {
            get { return Name == "new"; }
        }

        /// <summary>
        /// Initializes a new HassiumMethod using the interpreter, funcNode, localScope, self, and optional lambda.
        /// </summary>
        /// <param name="interpreter"></param>
        /// <param name="funcNode"></param>
        /// <param name="localScope"></param>
        /// <param name="self"></param>
        /// <param name="lambda"></param>
        public HassiumMethod(Interpreter interpreter, FuncNode funcNode, LocalScope localScope, HassiumObject self, bool lambda = false) : this(interpreter, funcNode, (StackFrame)null, self, lambda)
        {
            /*SelfReference = self;
            Interpreter = interpreter;
            FuncNode = funcNode;
            LocalScope = localScope;
            stackFrame = null;
            IsLambda = lambda;*/
            LocalScope = localScope;
        }

        /// <summary>
        /// Initializes a new HassiumMethod using the interpreter, funcNode, stackFrame, self, and optional lambda.
        /// </summary>
        /// <param name="interpreter"></param>
        /// <param name="funcNode"></param>
        /// <param name="stackFrame"></param>
        /// <param name="self"></param>
        /// <param name="lambda"></param>
        public HassiumMethod(Interpreter interpreter, FuncNode funcNode, StackFrame stackFrame, HassiumObject self, bool lambda = false)
        {
            SelfReference = self;
            Interpreter = interpreter;
            FuncNode = funcNode;
            LocalScope = stackFrame == null ? null : stackFrame.Scope;
            this.stackFrame = stackFrame;
            IsLambda = lambda;
        }

        /// <summary>
        /// Invokes a HassiumEventHandler.
        /// </summary>
        /// <param name="mt"></param>
        public static implicit operator HassiumEventHandler(HassiumMethod mt)
        {
            return mt.Invoke;
        }

        /// <summary>
        /// Invokes a Hassium Function.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumObject return value.</returns>
        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            if (stackFrame == null ||
                (stackFrame.Locals.Count == 0 || FuncNode.Parameters.Any(x => stackFrame.Locals.ContainsKey(x))))
                stackFrame = new StackFrame(LocalScope, (IsStatic && !IsConstructor) ? null : SelfReference);
            if (!IsStatic || IsConstructor)
                stackFrame.Locals["this"] = SelfReference;

            Interpreter.IsInFunction++;
            var parms = FuncNode.Parameters.Select(x => x).ToList();


            if (parms.Contains("this")) parms.Remove("this");

            if (parms.Count != args.Length)
                throw new Exception("Incorrect arguments for " +
                    (IsLambda ? "lambda function" : "function " + Name ) + ": Expected " + parms.Count + " args, got " + args.Length);

            for (int x = 0; x < parms.Count; x++)
                stackFrame.Locals[parms[x]] = args[x];

            Interpreter.CallStack.Push(stackFrame);

            if (FuncNode.Body is FuncNode) ((FuncNode) FuncNode.Body).Body.Visit(Interpreter);
            else FuncNode.Body.Visit(Interpreter);

            HassiumObject ret = Interpreter.CallStack.ReturnValue;

            Interpreter.CallStack.Pop();

            /*if (ret is HassiumArray)
                ret = ((HassiumArray) ret).Cast<object>()
                    .Select((s, i) => new {s, i})
                    .ToDictionary(x => (object) x.i, x => (object) x.s);*/
            Interpreter.IsInFunction--;
            Interpreter.ReturnFunc = false;
            return ret;
        }

        /// <summary>
        /// Returns a string representation of the HassiumMethod.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("[HassiumMethod: {0}`{1} SelfReference={2} {3}]", Name, (FuncNode.InfParams ? "i" : FuncNode.Parameters.Count.ToString()),
                SelfReference ?? "null", IsLambda ? "Lambda" : "");
        }

        /// <summary>
        ///     Converts an <see cref="IFunction" /> to a <see cref="Func{TResult}" />
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction" /> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject}" /></returns>
        public static Func<HassiumObject> GetFuncVoid(HassiumObject internalFunction)
        {
            return () => (internalFunction).Invoke();
        }

        /// <summary>
        ///     Converts an <see cref="IFunction" /> to a <see typeref="Func" /> &lt;<see cref="HassiumObject" />,
        ///     <see cref="HassiumObject" />&gt;
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction" /> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject}" /></returns>
        public static Func<HassiumObject, HassiumObject> GetFunc1(HassiumObject internalFunction)
        {
            return arg1 => internalFunction.Invoke(arg1);
        }

        /// <summary>
        ///     Converts an <see cref="IFunction" /> to a <see cref="T:Func{HassiumArray, HassiumObject}" />
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction" /> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject}" /></returns>
        public static Func<HassiumObject[], HassiumObject> GetFunc1Arr(HassiumObject internalFunction)
        {
            return (internalFunction).Invoke;
        }

        /// <summary>
        ///     Converts an <see cref="IFunction" /> to a <see cref="Func{HassiumObject, HassiumObject, HassiumObject}" />
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction" /> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject, HassiumObject}" /></returns>
        public static Func<HassiumObject, HassiumObject, HassiumObject> GetFunc2(HassiumObject internalFunction)
        {
            return (arg1, arg2) => (internalFunction).Invoke(arg1, arg2);
        }

        /// <summary>
        ///     Converts an <see cref="IFunction" /> to a
        ///     <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}" />
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction" /> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}" /></returns>
        public static Func<HassiumObject, HassiumObject, HassiumObject, HassiumObject> GetFunc3(
            HassiumObject internalFunction)
        {
            return (arg1, arg2, arg3) => (internalFunction).Invoke(arg1, arg2, arg3);
        }
    }
}