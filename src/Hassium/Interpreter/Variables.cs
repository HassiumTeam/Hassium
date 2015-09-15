using System;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Functions;
using Hassium.Parser;

namespace Hassium.Interpreter
{
    public static class Variables
    {
        public static HassiumObject GetVariable(Interpreter inter, string name, AstNode node)
        {
            if (inter.Constants.ContainsKey(name))
                return inter.Constants[name];
            if (inter.CallStack.Count > 0 && inter.CallStack.Peek().Locals.ContainsKey(name))
                return inter.CallStack.Peek().Locals[name];
            if (inter.Globals.ContainsKey(name))
                return inter.Globals[name];
            else throw new ParseException("The variable '" + name + "' doesn't exist.", node);
        }

        public static bool HasVariable(Interpreter inter, string name, bool onlyglobal = false)
        {
            return onlyglobal
                ? inter.Globals.ContainsKey(name) || inter.Constants.ContainsKey(name)
                    : inter.Globals.ContainsKey(name) || inter.Constants.ContainsKey(name) ||
                (inter.CallStack.Count > 0 && (inter.CallStack.Peek().Scope.Symbols.Contains(name) || inter.CallStack.Peek().Locals.ContainsKey(name)));
        }

        public static void SetGlobalVariable(Interpreter inter, string name, HassiumObject value, AstNode node)
        {
            if (inter.Constants.ContainsKey(name))
                throw new ParseException("Can't change the value of the internal constant '" + name + "'.", node);

            inter.Globals[name] = value;
        }

        public static void SetLocalVariable(Interpreter inter, string name, HassiumObject value, AstNode node)
        {
            if (inter.Constants.ContainsKey(name))
                throw new ParseException("Can't change the value of the internal constant '" + name + "'.", node);

            if (inter.CallStack.Count > 0)
                inter.CallStack.Peek().Locals[name] = value;
        }

        public static void SetVariable(Interpreter inter, string name, HassiumObject value, AstNode node, bool forceglobal = false, bool onlyexist = false)
        {
            if (!forceglobal && inter.CallStack.Count > 0 && (!onlyexist || (inter.CallStack.Peek().Scope.Symbols.Contains(name) || inter.CallStack.Peek().Locals.ContainsKey(name))) && !inter.Globals.ContainsKey(name))
                SetLocalVariable(inter, name, value, node);
            else
                SetGlobalVariable(inter, name, value, node);
        }

        public static void FreeVariable(Interpreter inter, string name, AstNode node, bool forceglobal = false)
        {
            if(inter.Constants.ContainsKey(name)) throw new ParseException("Can't delete internal constant '" + name + "'.", node);
            if (forceglobal)
            {
                if(!inter.Globals.ContainsKey(name)) throw new ParseException("The global variable '" + name + "' doesn't exist.", node);
                inter.Globals.Remove(name);
            }
            else
            {
                if(!HasVariable(inter, name)) throw new ParseException("The variable '" + name + "' doesn't exist.", node);
                if (inter.CallStack.Count > 0 && (inter.CallStack.Peek().Scope.Symbols.Contains(name) || inter.CallStack.Peek().Locals.ContainsKey(name)))
                    inter.CallStack.Peek().Locals.Remove(name);
                else
                    inter.Globals.Remove(name);
            }
        }
    }
}

