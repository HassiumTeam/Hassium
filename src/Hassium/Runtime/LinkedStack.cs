/**
  * Copyright (c) 2015, GruntTheDivine All rights reserved.

  * Redistribution and use in source and binary forms, with or without modification,
  * are permitted provided that the following conditions are met:
  * 
  *  * Redistributions of source code must retain the above copyright notice, this list
  *    of conditions and the following disclaimer.
  * 
  *  * Redistributions in binary form must reproduce the above copyright notice, this
  *    list of conditions and the following disclaimer in the documentation and/or
  *    other materials provided with the distribution.

  * Neither the name of the copyright holder nor the names of its contributors may be
  * used to endorse or promote products derived from this software without specific
  * prior written permission.
  * 
  * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
  * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
  * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
  * SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
  * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
  * TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
  * BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
  * CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
  * ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
  * DAMAGE.
**/
using System;

namespace Iodine.Util
{
    /// <summary>
    /// My own stack implementation, I don't know how well this will perform however I wrote this
    /// as I am currently convinced that System.Collections.Generic.Stack doesn't cut it for Iodine
    /// </summary>
    public class LinkedStack<T>
    {
        class StackItem<E>
        {
            public readonly E Item;
            public readonly StackItem<E> Next;

            public StackItem(E item)
            {
                Item = item;
            }

            public StackItem(E item, StackItem<E> parent)
            {
                Item = item;
                Next = parent;
            }
        }

        private StackItem<T> top;

        public int Count { private set; get; }

        public LinkedStack()
        {
        }

#if DOTNET_45
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
#endif
        public void Push(T obj)
        {
            if (Count > 7278)
            {

                throw new Exception();
            }
            if (top == null)
            {
                top = new StackItem<T>(obj);
            }
            else
            {
                top = new StackItem<T>(obj, top);
            }
            Count++;
        }

#if DOTNET_45
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
#endif
        public T Pop()
        {
            Count--;
            T ret = top.Item;
            top = top.Next;
            return ret;
        }

#if DOTNET_45
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
#endif
        public T Peek()
        {
            return top.Item;
        }

        public void Clear()
        {
            top = null;
        }
    }
}
