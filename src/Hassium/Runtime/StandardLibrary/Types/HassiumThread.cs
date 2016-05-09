using System;
using System.Threading;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumThread: HassiumObject
    {
        public Thread Thread { get; set; }
        public HassiumThread()
        {
            Attributes.Add("executeMethod", new HassiumFunction(executeMethod, 2));
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 2));
        }

        private HassiumThread _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumThread hassiumThread = new HassiumThread();

            hassiumThread.Thread = new Thread(() => args[0].Invoke(vm, HassiumList.Create(args[1]).Value.ToArray()));
            hassiumThread.Attributes.Add("sleep",   new HassiumFunction(hassiumThread.sleep, 1));
            hassiumThread.Attributes.Add("start",   new HassiumFunction(hassiumThread.start, 0));
            hassiumThread.Attributes.Add("stop",    new HassiumFunction(hassiumThread.stop, 0));

            return hassiumThread;
        }

        public HassiumNull executeMethod(VirtualMachine vm, HassiumObject[] args)
        {
            new Thread(() => args[0].Invoke(vm, HassiumList.Create(args[1]).Value.ToArray())).Start();
            return HassiumObject.Null;
        }
        public HassiumNull sleep(VirtualMachine vm, HassiumObject[] args)
        {
            Thread.Sleep((int)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull start(VirtualMachine vm, HassiumObject[] args)
        {
            Thread.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, HassiumObject[] args)
        {
            Thread.Abort();
            return HassiumObject.Null;
        }
    }
}