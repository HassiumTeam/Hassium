using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Emit
{
    public class SymbolTable
    {

        public int NextIndex { get; set; }
        public bool IsGlobalScope { get { return Scopes.Peek() == globalScope; } }

        public Stack<Scope> Scopes = new Stack<Scope>();
        private Scope globalScope = new Scope();
        private int nextGlobalIndex = 0;

        public SymbolTable()
        {
            NextIndex = 0;

            Scopes.Push(globalScope);
        }

        public void EnterScope()
        {
            Scopes.Push(new Scope());
        }

        public void LeaveScope()
        {
            Scopes.Pop();
            if (Scopes.Count == 2)
                NextIndex = 0;
        }

        public int GetSymbol(string name)
        {
            foreach (Scope scope in Scopes)
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
            foreach (Scope scope in Scopes)
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
            Scopes.Peek().Symbols.Add(name, NextIndex);
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
