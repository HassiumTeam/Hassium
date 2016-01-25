using System.Collections.Generic;
using System.Linq;
using Hassium.HassiumObjects;

namespace Hassium.Interpreter
{
    /// <summary>
    /// Class containing the call stack.
    /// </summary>
    public class CallStack
    {
        private Stack<StackFrame> frames = new Stack<StackFrame>();

        /// <summary>
        /// Returns the top StackFrame.
        /// </summary>
        /// <returns>StackFrame</returns>
        public StackFrame Peek()
        {
            return frames.Peek();
        }

        /// <summary>
        /// Pushes a StackFrame to the call stack.
        /// </summary>
        /// <param name="st"></param>
        public void Push(StackFrame st)
        {
            frames.Push(st);
        }

        /// <summary>
        /// Returns true if the call stack is not empty.
        /// </summary>
        /// <returns>bool</returns>
        public bool Any()
        {
            return frames.Count > 0;
        }

        /// <summary>
        /// Pops the top StackFrame off.
        /// </summary>
        /// <returns>StackFrame</returns>
        public StackFrame Pop()
        {
            return frames.Pop();
        }

        /// <summary>
        /// The return value of a Hassium Function.
        /// </summary>
        public HassiumObject ReturnValue
        {
            get { return frames.Peek().ReturnValue; }
        }

        /// <summary>
        /// Returns the value from a variable name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="st"></param>
        /// <returns>HassiumObject</returns>
        public HassiumObject GetVariable(string name, bool st = false)
        {
            HassiumObject ret = null;
            if (st)
            {
                if(frames.Any(
                           x => x.Locals.Any(y => y.Key.Contains("`") && y.Key.Substring(0, y.Key.IndexOf("`")) == name)))
                ret =
                    frames.First(
                        x =>
                            x.Locals.Any(y => y.Key.Contains("`") && y.Key.Substring(0, y.Key.IndexOf("`")) == name))
                        .Locals.First(y => y.Key.Contains("`") && y.Key.Substring(0, y.Key.IndexOf("`")) == name)
                        .Value;
            }
            if (HasVariable(name) || ret == null) return frames.First(x => x.Locals.ContainsKey(name)).Locals[name];
            return ret;
        }

        /// <summary>
        /// Returns a dictionary containing all the local variables and their names.
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        public Dictionary<string, HassiumObject> GetLocals(bool all = false)
        {
            return all ? frames.SelectMany(x => x.Locals).ToDictionary(x => x.Key, x => x.Value) : frames.Peek().Locals;
        }
        
        /// <summary>
        /// Sets a variable at name to value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetVariable(string name, HassiumObject value)
        {
            if (frames.Any(x => x.Locals.ContainsKey(name)))
                frames.First(x => x.Locals.ContainsKey(name)).Locals[name] = value;
            else Peek().Locals[name] = value;
        }

        /// <summary>
        /// Frees a variable off the call stack.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="st"></param>
        public void FreeVariable(string name, bool st = false)
        {
            if (frames.Any(x => x.Locals.ContainsKey(name)))
                if (st)
                    frames.First(x => x.Locals.Any(y => y.Key.StartsWith(name))).Locals.Remove(name);
                else
                    frames.First(x => x.Locals.ContainsKey(name)).Locals.Remove(name);
        }

        /// <summary>
        /// Returns true if the variable exists in the current context.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="st"></param>
        /// <returns>bool</returns>
        public bool HasVariable(string name, bool st = false)
        {
            if (frames.Count == 0) return false;
            bool ret = false;
            if (st)
                ret = Peek().Scope.Symbols.Any(y => y.Contains("`") && y.Substring(0, y.IndexOf("`")) == name) ||
                       frames.Any(
                           x => x.Locals.Any(y => y.Key.Contains("`") && y.Key.Substring(0, y.Key.IndexOf("`")) == name));
            if(ret == false) ret = Peek().Scope.Symbols.Contains(name) || frames.Any(x => x.Locals.ContainsKey(name));
            return ret;
        }
    }
}
