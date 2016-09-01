using System;
using System.Collections.Generic;
using System.Threading;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumThread: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("thread");

        public Thread Thread { get; private set; }
        public HassiumObject ReturnValue { get; private set; }

        public HassiumThread(VirtualMachine vm, HassiumMethod method, StackFrame.Frame frame)
        {
            VirtualMachine newVM = vm.Clone() as VirtualMachine;
            newVM.ExceptionReturns = new Dictionary<HassiumMethod, int>();
            newVM.Handlers = new Stack<HassiumExceptionHandler>();
            newVM.Stack = new Stack<HassiumObject>();
            newVM.StackFrame = new StackFrame();
            Thread = new Thread(() => ReturnValue = method.Invoke(newVM, frame));

            AddType(TypeDefinition);
            AddAttribute("isAlive",     new HassiumProperty(get_isAlive));
            AddAttribute("returnValue", new HassiumProperty(get_returnValue));
            AddAttribute("start",       start,  0);
            AddAttribute("stop",        stop,   0);
        }

        public HassiumBool get_isAlive(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Thread.IsAlive);
        }
        public HassiumObject get_returnValue(VirtualMachine vm, params HassiumObject[] args)
        {
            return ReturnValue;
        }
        public HassiumNull start(VirtualMachine vm, params HassiumObject[] args)
        {
            Thread.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, params HassiumObject[] args)
        {
            Thread.Abort();
            return HassiumObject.Null;
        }
    }
}