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

using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;
using Hassium.Parser.Ast;

namespace Hassium.HassiumObjects
{
    public class HassiumTuple: HassiumObject
    {
        public TupleNode TupleNode { get; private set; }

        public List<HassiumObject> Items { get; private set; } 

        public HassiumTuple(TupleNode value, Hassium.Interpreter.Interpreter interpreter) : this(value.Children[0].Children.Select((v, i) => (HassiumObject)v.Visit(interpreter)).ToList())
        {
            TupleNode = value;
        }


        public override string ToString()
        {
            return "Tuple (" + string.Join(", ", Items.Select(x => x == null ? "null" : x.ToString())) + ")";
        }

        public HassiumTuple(IEnumerable<HassiumObject> value)
        {
            Items = value.ToList();

            refresh();

            Attributes.Add("add", new InternalFunction(add, -1));
            Attributes.Add("remove", new InternalFunction(remove, -1));
        }

        private void refresh()
        {
            Attributes =
                Items.Select((item, index) => new KeyValuePair<string, HassiumObject>("Item" + index, item))
                    .ToDictionary(x => x.Key, x => x.Value);
        }

        private HassiumObject add(HassiumObject[] args)
        {
            Items.AddRange(args);

            refresh();

            return null;
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            args.All(x => Items.Remove(x));

            refresh();

            return null;
        }
    }
}
