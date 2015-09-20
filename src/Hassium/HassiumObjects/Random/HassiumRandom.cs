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

using Hassium.Functions;

namespace Hassium.HassiumObjects.Random
{
    public class HassiumRandom : HassiumObject
    {
        public System.Random Value { get; private set; }

        public HassiumRandom(System.Random value)
        {
            Value = value;
            Attributes.Add("next", new InternalFunction(next, new[] {0, 1, 2}));
            Attributes.Add("nextDouble", new InternalFunction(nextDouble, 0));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        private HassiumObject next(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return Value.Next();
                case 1:
                    return Value.Next(args[0].HInt().Value);
                default:
                    return Value.Next(args[0].HInt().Value, args[1].HInt().Value);
            }
        }

        private HassiumObject nextDouble(HassiumObject[] args)
        {
            return Value.NextDouble();
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}