using System;
using System.Net;
using Hassium.HassiumObjects;

namespace Hassium
{
    public class Constructors : ILibrary
    {
        [IntFunc("newclient")]
        public static HassiumObject NewClient(HassiumObject[] args)
        {
            return new HassiumClient(new WebClient());
        }

        [IntFunc("newfile")]
        public static HassiumObject File(HassiumObject[] args)
        {
            return new HassiumFile(args[0].ToString());
        }
    }
}

