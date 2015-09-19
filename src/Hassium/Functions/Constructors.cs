using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Collections;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Networking.HTTP;
using Hassium.HassiumObjects.Random;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Drawing;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.Functions
{
    public class Constructors : ILibrary
    {
        [IntFunc("Object", true, 0)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }

        [IntFunc("Date", true, 0)]
        public static HassiumObject Date(HassiumObject[] args)
        {
            return new HassiumDate(DateTime.Now);
        }

        [IntFunc("Array", true, 1)]
        public static HassiumObject Array(HassiumObject[] args)
        {
            return new HassiumArray(new HassiumObject[args[0].HInt().Value]);
        }

        [IntFunc("Random", true, new[] {0, 1})]
        public static HassiumObject Random(HassiumObject[] args)
        {
            return args.Length > 0
                ? new HassiumRandom(new Random(args[0].HInt().Value))
                : new HassiumRandom(new Random());
        }


        [IntFunc("Event", true, -1)]
        public static HassiumObject Event(HassiumObject[] args)
        {
            var ret = new HassiumEvent();
            if (args.Length > 0)
            {
                args.All(x =>
                {
                    if (x is HassiumMethod) ret.AddHandler((HassiumMethod) x);
                    return true;
                });
            }
            return ret;
        }
    }
}