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
using System.Drawing;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumBitmap : HassiumObject
    {
        public Bitmap Value { get; private set; }

        public HassiumBitmap(HassiumObject[] cctr)
        {
            if (cctr[0] is HassiumString)
                Value = new Bitmap(((HassiumString) cctr).Value);
            else if (cctr[0] is HassiumImage)
                Value = new Bitmap(((HassiumImage) cctr).Value);
            else if (cctr[0] is HassiumInt)
                Value = new Bitmap(((HassiumInt) cctr[0]).Value, ((HassiumInt) cctr[1]).Value);
            else if (cctr[0] is HassiumDouble)
                Value = new Bitmap(((HassiumDouble) cctr[0]).ValueInt, ((HassiumDouble) cctr[1]).ValueInt);
            else
                throw new Exception("Unknown type " + cctr[0].GetType() + " in HassiumBitmap constructor");

            Attributes.Add("height", new HassiumProperty("height", x => Value.Height, x => null, true));
            Attributes.Add("width", new HassiumProperty("width", x => Value.Width, x => null, true));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("makeTransparent", new InternalFunction(makeTransparent, 0));
            Attributes.Add("save", new InternalFunction(save, 1));
            Attributes.Add("setPixel", new InternalFunction(setPixel, 3));
            Attributes.Add("setResolution", new InternalFunction(setResolution, 2));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();

            return null;
        }

        private HassiumObject makeTransparent(HassiumObject[] args)
        {
            if (args.Length <= 0)
                Value.MakeTransparent();
            else
                Value.MakeTransparent(((HassiumColor) args[0]).Value);

            return null;
        }

        private HassiumObject save(HassiumObject[] args)
        {
            Value.Save(((HassiumString) args[0]).Value);

            return null;
        }

        private HassiumObject setPixel(HassiumObject[] args)
        {
            Value.SetPixel(((HassiumDouble) args[0]).ValueInt, ((HassiumDouble) args[1]).ValueInt,
                ((HassiumColor) args[2]).Value);

            return null;
        }

        private HassiumObject setResolution(HassiumObject[] args)
        {
            Value.SetResolution(((float) ((HassiumDouble) args[0]).Value), ((float) ((HassiumDouble) args[1]).Value));

            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}