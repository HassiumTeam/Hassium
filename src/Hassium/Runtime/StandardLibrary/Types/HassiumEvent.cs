using System;
using System.Collections.Generic;

using Hassium.Runtime.StandardLibrary;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumEvent: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Event");
        public HassiumList Handlers { get; set; }

        public HassiumEvent()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, new int[] { 0, 1 }));
        }

        private HassiumEvent _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumEvent hassiumEvent = new HassiumEvent();

            hassiumEvent.Handlers = args.Length == 0 ?  new HassiumList(new HassiumObject[0]) : HassiumList.Create(args[0]);
            hassiumEvent.Attributes.Add("add",          new HassiumFunction(hassiumEvent.add, -1));
            hassiumEvent.Attributes.Add("handle",       new HassiumFunction(hassiumEvent.handle, -1));
            hassiumEvent.Attributes.Add("remove",       new HassiumFunction(hassiumEvent.handle, -1));
            hassiumEvent.AddType(HassiumEvent.TypeDefinition);

            return hassiumEvent;
        }

        public HassiumNull add(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Handlers.Value.Add(obj);

            return HassiumObject.Null;
        }
        public HassiumList handle(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (HassiumObject obj in Handlers.Value)
                result.Value.Add(obj.Invoke(vm, args));

            return result;
        }
        public HassiumNull remove(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Handlers.Value.Remove(obj);

            return HassiumObject.Null;
        }
    }
}