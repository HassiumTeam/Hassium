using Hassium.Compiler;

using System.Collections.Generic;
using System.Threading;

using Iodine.Util;

namespace Hassium.Runtime.Types
{
    public class HassiumThread : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new ThreadTypeDef();

        public Thread Thread { get; private set; }
        public HassiumObject ReturnValue { get; private set; }

        public HassiumThread(VirtualMachine vm, SourceLocation location, HassiumMethod method, Dictionary<int, HassiumObject> frame)
        {
            VirtualMachine newVM = vm.Clone() as VirtualMachine;
            newVM.ExceptionReturns = new Dictionary<HassiumMethod, int>();
            newVM.Handlers = new Stack<HassiumExceptionHandler>();
            newVM.Stack = new LinkedStack<HassiumObject>();
            newVM.StackFrame = new StackFrame();

            Thread = new Thread(() => ReturnValue = method.Invoke(newVM, location, frame));
            ReturnValue = Null;

            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class representing a separate thread from the main entry point.",
            "@returns Thread."
            )]
        public class ThreadTypeDef : HassiumTypeDefinition
        {
            public ThreadTypeDef() : base("Thread")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "isalive", new HassiumProperty(get_isalive) },
                    { "returns", new HassiumProperty(get_returns) },
                    { "start", new HassiumFunction(start) },
                    { "stop", new HassiumFunction(stop) }
                };
            }

            [DocStr(
                "@desc Gets the readonly bool representing if this thread is currently running.",
                "@returns true if the thread is alive, otherwise false."
                )]
            [FunctionAttribute("isalive { get; }")]
            public static HassiumBool get_isalive(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumThread).Thread.IsAlive);
            }

            [DocStr(
                "@desc Gets the readonly return value of this thread (post run).",
                "@returns The return value of this thread as object."
                )]
            [FunctionAttribute("returns { get; }")]
            public static HassiumObject get_returns(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumThread).ReturnValue;
            }

            [DocStr(
                "@desc Starts this thread.",
                "@returns null."
                )]
            [FunctionAttribute("func start () : null")]
            public static HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumThread).Thread.Start();
                return Null;
            }

            [DocStr(
                "@desc Stops this thread.",
                "@returns null."
                )]
            [FunctionAttribute("func stop () : null")]
            public static HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumThread).Thread.Abort();
                return Null;
            }
        }
    }
}
