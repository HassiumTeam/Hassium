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

using System.Linq;
using System.Reflection;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Reflection
{
    public class HassiumAssembly : HassiumObject
    {
        public Assembly Value { get; private set; }

        public HassiumAssembly(Assembly ass)
        {
            Value = ass;
            Attributes.Add("entryPoint", new InternalFunction(x => Value.EntryPoint.ToString(), 0, true));
            Attributes.Add("fullName", new InternalFunction(x => Value.FullName, 0, true));
            Attributes.Add("getFile", new InternalFunction(getFile, 1));
            Attributes.Add("getFiles", new InternalFunction(getFiles, 0));
            Attributes.Add("getModule", new InternalFunction(getModule, 1));
            Attributes.Add("getModules", new InternalFunction(getModules, 0));
            Attributes.Add("getName", new InternalFunction(getName, 0));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        private HassiumObject getFile(HassiumObject[] args)
        {
            return new HassiumFileStream(Value.GetFile(args[0]));
        }

        private HassiumObject getFiles(HassiumObject[] args)
        {
            return new HassiumArray(Value.GetFiles().ToArray());
        }

        private HassiumObject getModule(HassiumObject[] args)
        {
            return new HassiumModule(Value.GetModule(args[0]));
        }

        private HassiumObject getModules(HassiumObject[] args)
        {
            return new HassiumArray(Value.GetModules().ToArray());
        }

        private HassiumObject getName(HassiumObject[] args)
        {
            return new HassiumString(Value.GetName().ToString());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}