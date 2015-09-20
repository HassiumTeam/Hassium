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
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumDirectory : HassiumObject
    {
        public HassiumDirectory()
        {
            Attributes.Add("exists", new InternalFunction(Exists, 1));
            Attributes.Add("create", new InternalFunction(Create, 1));
            Attributes.Add("copy", new InternalFunction(Copy, 2));
            Attributes.Add("move", new InternalFunction(Move, 2));
            Attributes.Add("rename", new InternalFunction(Rename, 2));
            Attributes.Add("delete", new InternalFunction(Delete, 1));
            Attributes.Add("getCreationTime", new InternalFunction(GetCreationTime, 1));
            Attributes.Add("getLastAccessTime", new InternalFunction(GetLastAccessTime, 1));
            Attributes.Add("getLastWriteTime", new InternalFunction(GetLastWriteTime, 1));
            Attributes.Add("setLastAccessTime", new InternalFunction(SetLastWriteTime, 2));
            Attributes.Add("setLastWriteTime", new InternalFunction(SetLastWriteTime, 2));
            Attributes.Add("setCreationTime", new InternalFunction(SetCreationTime, 2));
            Attributes.Add("getParent", new InternalFunction(GetParent, 1));
            Attributes.Add("deleteDirectory", new InternalFunction(DeleteDirectory, 1));
            Attributes.Add("getDirectory", new InternalFunction(GetDirectory, 0));
            Attributes.Add("setDirectory", new InternalFunction(SetDirectory, 1));
            Attributes.Add("getFiles", new InternalFunction(GetFiles, 1));
            Attributes.Add("getDirectories", new InternalFunction(GetDirectories, 1));
        }

        public HassiumObject Exists(HassiumObject[] args)
        {
            return new HassiumBool(Directory.Exists(args[0]));
        }

        public HassiumObject Create(HassiumObject[] args)
        {
            Directory.CreateDirectory(args[0]);
            return null;
        }

        public HassiumObject Copy(HassiumObject[] args)
        {
            var dest = args[1].ToString();

            if (dest[dest.Length - 1] != Path.DirectorySeparatorChar)
                dest += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
            foreach (string Element in Directory.GetFileSystemEntries(args[0]))
            {
                var fname = Path.GetFileName(Element);
                if (fname != null)
                {
                    if (Directory.Exists(Element))
                        Copy(new HassiumObject[] {Element, Path.Combine(dest, fname)});
                    else
                        File.Copy(Element, Path.Combine(dest, fname), true);
                }
            }

            return null;
        }

        public HassiumObject Move(HassiumObject[] args)
        {
            Copy(new HassiumObject[] {args[0], args[1].ToString()});
            Directory.Delete(args[0], true);
            args[0] = args[1].ToString();
            return null;
        }

        public HassiumObject Rename(HassiumObject[] args)
        {
            var dname = Path.GetDirectoryName(args[0]);
            if (dname != null) Move(new HassiumObject[] {args[0], Path.Combine(dname, args[1].ToString())});
            return null;
        }

        public HassiumObject Delete(HassiumObject[] args)
        {
            Directory.Delete(args[0]);
            return null;
        }

        public HassiumObject GetCreationTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetCreationTime(args[0].ToString()));
        }

        public HassiumObject GetLastAccessTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetLastAccessTime(args[0].ToString()));
        }

        public HassiumObject GetLastWriteTime(HassiumObject[] args)
        {
            return new HassiumDate(Directory.GetLastWriteTime(args[0].ToString()));
        }

        public HassiumObject SetLastAccessTime(HassiumObject[] args)
        {
            Directory.SetLastAccessTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject SetLastWriteTime(HassiumObject[] args)
        {
            Directory.SetLastWriteTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject SetCreationTime(HassiumObject[] args)
        {
            Directory.SetCreationTime(args[0].ToString(), ((HassiumDate) args[1]).Value);
            return null;
        }

        public HassiumObject GetParent(HassiumObject[] args)
        {
            return new HassiumString(Directory.GetParent(args[0].ToString()).ToString());
        }

        public HassiumObject DeleteDirectory(HassiumObject[] args)
        {
            Directory.Delete(args[0].ToString());
            return null;
        }

        public HassiumObject GetDirectory(HassiumObject[] args)
        {
            return Directory.GetCurrentDirectory();
        }

        public HassiumObject SetDirectory(HassiumObject[] args)
        {
            Directory.SetCurrentDirectory(args[0].ToString());
            return null;
        }

        public HassiumObject GetFiles(HassiumObject[] args)
        {
            return Directory.GetFiles(args[0].ToString());
        }

        public HassiumObject GetDirectories(HassiumObject[] args)
        {
            return Directory.GetDirectories(args[0].ToString());
        }
    }
}