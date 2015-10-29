using System;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Interpreter
{
    public class HassiumGC: HassiumObject
    {
        public HassiumGC()
        {
            Attributes.Add("addMemoryPressure", new InternalFunction(addMemoryPressure, 1));
            Attributes.Add("cancelFullGCNotification", new InternalFunction(cancelFullGCNotification, 0));
            Attributes.Add("collect", new InternalFunction(collect, -1));
            Attributes.Add("collectionCount", new InternalFunction(collectionCount, 1));
            Attributes.Add("getGeneration", new InternalFunction(getGeneration, 1));
            Attributes.Add("getTotalMemory", new InternalFunction(getTotalMemory, 0));
            Attributes.Add("keepAlive", new InternalFunction(keepAlive, 1));
            Attributes.Add("removeMemoryPressure", new InternalFunction(removeMemoryPressure, 1));
            Attributes.Add("supressFinalize", new InternalFunction(supressFinalize, 1));
            Attributes.Add("waitForFullGCApproach", new InternalFunction(waitForFullGCApproach, 0));
            Attributes.Add("waitForFullGCComplete", new InternalFunction(waitForFullGCComplete, 0));
            Attributes.Add("pendingFinalizers", new InternalFunction(waitForPendingFinalizers, 0));
        }

        public HassiumObject addMemoryPressure(HassiumObject[] args)
        {
            GC.AddMemoryPressure(Convert.ToInt64(((HassiumDouble)args[0]).Value));

            return null;
        }

        public HassiumObject cancelFullGCNotification(HassiumObject[] args)
        {
            GC.CancelFullGCNotification();

            return null;
        }

        public HassiumObject collect(HassiumObject[] args)
        {
            if (args.Length <= 0)
                GC.Collect();
            else
                GC.Collect(((HassiumInt)args[0]).Value);

            return null;
        }

        public HassiumObject collectionCount(HassiumObject[] args)
        {
            GC.CollectionCount(((HassiumInt)args[0]).Value);

            return null;
        }

        public HassiumObject getGeneration(HassiumObject[] args)
        {
            return GC.GetGeneration(args[0]);
        }

        public HassiumObject getTotalMemory(HassiumObject[] args)
        {
            return GC.GetTotalMemory(true);
        }

        public HassiumObject keepAlive(HassiumObject[] args)
        {
            GC.KeepAlive(args[0]);

            return null;
        }

        public HassiumObject removeMemoryPressure(HassiumObject[] args)
        {
            GC.RemoveMemoryPressure((long)((HassiumDouble)args[0]).Value);

            return null;
        }

        public HassiumObject supressFinalize(HassiumObject[] args)
        {
            GC.SuppressFinalize(args[0]);

            return null;
        }

        public HassiumObject waitForFullGCApproach(HassiumObject[] args)
        {
            return new HassiumString(GC.WaitForFullGCApproach().ToString());
        }

        public HassiumObject waitForFullGCComplete(HassiumObject[] args)
        {
            return new HassiumString(GC.WaitForFullGCComplete().ToString());
        }

        public HassiumObject waitForPendingFinalizers(HassiumObject[] args)
        {
            GC.WaitForPendingFinalizers();

            return null;
        }
    }
}
