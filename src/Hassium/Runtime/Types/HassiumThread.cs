using Hassium.Compiler;

using System.Collections.Generic;
using System.Threading;

using Iodine.Util;

namespace Hassium.Runtime.Types
{
    public class HassiumThread : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Thread");

        public Thread Thread { get; private set; }
        public HassiumObject ReturnValue { get; private set; }

        public HassiumThread(VirtualMachine vm, SourceLocation location, HassiumMethod method, StackFrame.Frame frame)
        {
            VirtualMachine newVM = vm.Clone() as VirtualMachine;
            newVM.ExceptionReturns = new Dictionary<HassiumMethod, int>();
            newVM.Handlers = new Stack<HassiumExceptionHandler>();
            newVM.Stack = new LinkedStack<HassiumObject>();
            newVM.StackFrame = new StackFrame();

            Thread = new Thread(() => ReturnValue = method.Invoke(newVM, location, frame));
            ReturnValue = Null;

            AddType(TypeDefinition);

            AddAttribute("isalive", new HassiumProperty(get_isalive));
            AddAttribute("returns", new HassiumProperty(get_returns));
            AddAttribute("start", start, 0);
            AddAttribute("stop", stop, 0);
        }

        [FunctionAttribute("isalive { get; }")]
        public HassiumBool get_isalive(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Thread.IsAlive);
        }

        [FunctionAttribute("returns { get; }")]
        public HassiumObject get_returns(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return ReturnValue;
        }

        [FunctionAttribute("func start () : null")]
        public HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Thread.Start();
            return Null;
        }

        [FunctionAttribute("func stop () : null")]
        public HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Thread.Abort();
            return Null;
        }
    }
}
