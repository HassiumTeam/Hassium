using System;
using System.Net;
using Hassium.HassiumObjects;

namespace Hassium
{
    public class Constructors : ILibrary
    {
        [IntFunc("WebClient", true)]
        public static HassiumObject WebClient(HassiumObject[] args)
        {
            return new HassiumClient(new WebClient());
        }

        [IntFunc("Object", true)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }
    }
}

