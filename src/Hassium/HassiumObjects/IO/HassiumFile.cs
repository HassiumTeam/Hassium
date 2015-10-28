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

using System.IO;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumFile : HassiumObject
    {
        //public string FilePath { get; set; }

        /// public bool Exists { get { return File.Exists(FilePath); } }
        public HassiumFile()
        {
            Attributes.Add("writeText", new InternalFunction(PutContent, 2));
            Attributes.Add("readText", new InternalFunction(ReadContent, 1));
            Attributes.Add("readLines", new InternalFunction(ReadLines, 1));
            Attributes.Add("exists", new InternalFunction(Exists, 1));
            Attributes.Add("create", new InternalFunction(Create, 1));
            Attributes.Add("append", new InternalFunction(Append, 2));
            Attributes.Add("appendLines", new InternalFunction(AppendLines, 2));
            Attributes.Add("copy", new InternalFunction(Copy, 2));
            Attributes.Add("move", new InternalFunction(Move, 2));
            Attributes.Add("rename", new InternalFunction(Rename, 2));
            Attributes.Add("deleteFile", new InternalFunction(DeleteFile, 1));
            Attributes.Add("readBytes", new InternalFunction(ReadBytes, 1));
            Attributes.Add("createText", new InternalFunction(createText, 1));
            Attributes.Add("openText", new InternalFunction(openText, 1));
            Attributes.Add("getCreationTime", new InternalFunction(getCreationTime, 1));
            Attributes.Add("getLastAccessTime", new InternalFunction(getLastAccessTime, 1));
            Attributes.Add("getLastWriteTime", new InternalFunction(getLastWriteTime, 1));
            Attributes.Add("setLastAccessTime", new InternalFunction(setLastWriteTime, 2));
            Attributes.Add("setCreationTime", new InternalFunction(setCreationTime, 2));
        }


        public HassiumObject Create(HassiumObject[] args)
        {
            File.Create(args[0].ToString());
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            File.Copy(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            File.Move(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject Exists(HassiumObject[] args)
        {
            return File.Exists(args[0].ToString()) || Directory.Exists(args[0].ToString());
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            var dname = Path.GetDirectoryName(args[0].ToString());
            if (dname != null) File.Move(args[0].ToString(), Path.Combine(dname, args[1].ToString()));
            return null;
        }

        public HassiumObject Append(HassiumObject[] args)
        {
            File.AppendAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject AppendLines(HassiumObject[] args)
        {
            File.AppendAllLines(args[0].ToString(), args[1].HArray().Value.Select(x => x.ToString()));
            return null;
        }

        public HassiumObject PutContent(HassiumObject[] args)
        {
            File.WriteAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public HassiumObject ReadContent(HassiumObject[] args)
        {
            return File.ReadAllText(args[0].ToString());
        }

        public HassiumObject ReadLines(HassiumObject[] args)
        {
            return File.ReadAllLines(args[0].ToString());
        }

        public HassiumObject ReadBytes(HassiumObject[] args)
        {
            return new HassiumArray(File.ReadAllBytes(args[0].ToString()).Select(b => new HassiumByte(b)));
        }

        public HassiumObject DeleteFile(HassiumObject[] args)
        {
            File.Delete(args[0].ToString());
            return null;
        }


        public HassiumObject createText(HassiumObject[] args)
        {
            return new HassiumTextWriter(File.CreateText(args[0].ToString()));
        }

        public HassiumObject openText(HassiumObject[] args)
        {
            return new HassiumTextReader(File.OpenText(args[0].ToString()));
        }

        public HassiumObject getCreationTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetCreationTime(args[0].ToString()));
        }

        public HassiumObject getLastAccessTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetLastAccessTime(args[0].ToString()));
        }

        public HassiumObject getLastWriteTime(HassiumObject[] args)
        {
            return new HassiumDate(File.GetLastWriteTime(args[0].ToString()));
        }

        public HassiumObject setLastAccessTime(HassiumObject[] args)
        {
            File.SetLastAccessTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject setLastWriteTime(HassiumObject[] args)
        {
            File.SetLastWriteTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject setCreationTime(HassiumObject[] args)
        {
            File.SetCreationTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }
    }
}