using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Emit
{
    public class SymbolTable
    {

        public int NextIndex { get; set; }
        public bool IsGlobalScope { get { return scopes.Peek() == globalScope; } }

        private Stack<Scope> scopes = new Stack<Scope>();
        private Scope globalScope = new Scope();
        private int nextGlobalIndex = 0;

        public SymbolTable()
        {
            NextIndex = 0;

            scopes.Push(globalScope);
        }

        public void EnterScope()
        {
            scopes.Push(new Scope());
        }

        public void LeaveScope()
        {
            scopes.Pop();
            if (scopes.Count == 2)
                NextIndex = 0;
        }

        public int GetSymbol(string name)
        {
            foreach (Scope scope in scopes)
                if (scope.Symbols.ContainsKey(name))
                    return scope.Symbols[name];
            return -1;
        }
        public int GetGlobalSymbol(string name)
        {
            return globalScope.Symbols[name];
        }

        public bool ContainsSymbol(string name)
        {
            foreach (Scope scope in scopes)
                if (scope.Symbols.ContainsKey(name))
                    return true;
            return false;
        }
        public bool ContainsGlobalSymbol(string name)
        {
            return globalScope.Symbols.ContainsKey(name);
        }

        public int AddSymbol(string name)
        {
            scopes.Peek().Symbols.Add(name, NextIndex);
            return NextIndex++;
        }
        public int AddGlobalSymbol(string name)
        {
            globalScope.Symbols.Add(name, nextGlobalIndex);
            return nextGlobalIndex++;
        }

        public int HandleSymbol(string name)
        {
            if (!ContainsSymbol(name))
                AddSymbol(name);
            return GetSymbol(name);
        }
    }
}
