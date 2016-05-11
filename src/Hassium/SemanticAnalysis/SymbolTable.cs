using System;
using System.Collections.Generic;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium
{
    public class SymbolTable
    {
        class Scope
        {
            private Dictionary<string, int> symbols = new Dictionary<string, int>();

            public int GetSymbol(string name)
            {
                return symbols[name];
            }

            public bool FindSymbol(string name)
            {
                return symbols.ContainsKey(name);
            }

            public void AddSymbol(string name, int index)
            {
                symbols[name] = index;
            }
        }

        private Stack<Scope> scopes = new Stack<Scope>();
        private Scope globalScope = new Scope();
        public int NextIndex { get { return nextIndex; } set { nextIndex = value; } }
        private int nextIndex = 0;
        private int nextGlobalIndex = 0;

        public bool InGlobalScope { get { return scopes.Peek() == globalScope; } }

        public SymbolTable()
        {
            scopes.Push(globalScope);
        }

        public void EnterScope()
        {
            scopes.Push(new Scope());
        }

        public void PopScope()
        {
            scopes.Pop();
            if (scopes.Count == 2)
                nextIndex = 0;
        }

        public int GetIndex(string name)
        {
            foreach (Scope scope in scopes)
                if (scope.FindSymbol(name))
                    return scope.GetSymbol(name);
            return -1;
        }
        public int GetGlobalIndex(string name)
        {
            return globalScope.GetSymbol(name);
        }

        public bool FindSymbol(string name)
        {
            foreach (Scope scope in scopes)
                if (scope.FindSymbol(name))
                    return true;
            return false;
        }
        public bool FindGlobalSymbol(string name)
        {
            return globalScope.FindSymbol(name);
        }

        public int AddSymbol(string name)
        {
            scopes.Peek().AddSymbol(name, nextIndex);
            return nextIndex++;
        }
        public int AddGlobalSymbol(string name)
        {
            globalScope.AddSymbol(name, nextGlobalIndex);
            return nextGlobalIndex++;
        }
    }
}