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

namespace Hassium.HassiumObjects
{
    /// <summary>
    /// Delegate for a HassiumInstance.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public delegate HassiumObject HassiumInstanceFunctionDelegate(HassiumObject self, params HassiumObject[] arguments);

    /// <summary>
    /// Class for a HassiumProperty.
    /// </summary>
    public class HassiumProperty : HassiumObject
    {
        /// <summary>
        /// Delegate for set.
        /// </summary>
        public HassiumInstanceFunctionDelegate SetValue;
        /// <summary>
        /// Delegatae for get.
        /// </summary>
        public HassiumFunctionDelegate GetValue;
        /// <summary>
        /// Name of property.
        /// </summary>
        public string Name;
        /// <summary>
        /// Returns if property is read-only.
        /// </summary>
        public bool ReadOnly;

        /// <summary>
        /// Initializes a new HassiumProperty using the name, get, set, and optional ro values.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <param name="ro"></param>
        public HassiumProperty(string name, HassiumFunctionDelegate get, HassiumInstanceFunctionDelegate set, bool ro = false)
        {
            Name = name;
            GetValue = get;
            SetValue = set;
            ReadOnly = ro;
        }

        /// <summary>
        /// Overrides the invoke to return get.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumObject return value.</returns>
        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            return GetValue(args);
        }
    }
}