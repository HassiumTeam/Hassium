﻿using System;
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

        [IntFunc("File", true)]
        public static HassiumObject File(HassiumObject[] args)
        {
            return new HassiumFile(args[0].ToString());
        }
    }
}

