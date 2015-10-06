// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Interpreter
{
    public class HassiumStopWatch: HassiumObject
    {
        public Stopwatch Value { get; private set; }

        public HassiumStopWatch()
        {
            Value = new Stopwatch();
            Attributes.Add("elapsedMilliseconds", new InternalFunction(elapsedMilliseconds, 0));
            Attributes.Add("elapsedTicks", new InternalFunction(elapsedTicks, 0));
            Attributes.Add("isRunning", new InternalFunction(isRunning, 0));
            Attributes.Add("start", new InternalFunction(start, 0));
            Attributes.Add("reset", new InternalFunction(reset, 0));
            Attributes.Add("restart", new InternalFunction(restart, 0));
            Attributes.Add("stop", new InternalFunction(stop, 0));
        }

        private HassiumObject elapsedMilliseconds(HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Value.ElapsedMilliseconds));
        }

        private HassiumObject elapsedTicks(HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Value.ElapsedTicks));
        }

        private HassiumObject isRunning(HassiumObject[] args)
        {
            return new HassiumBool(Value.IsRunning);
        }

        private HassiumObject start(HassiumObject[] args)
        {
            Value.Start();

            return null;
        }

        private HassiumObject reset(HassiumObject[] args)
        {
            Value.Reset();

            return null;
        }

        private HassiumObject restart(HassiumObject[] args)
        {
            Value.Restart();
            
            return null;
        }

        private HassiumObject stop(HassiumObject[] args)
        {
            Value.Stop();

            return null;
        }
    }
}
