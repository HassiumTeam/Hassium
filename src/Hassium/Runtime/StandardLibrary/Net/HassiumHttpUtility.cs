using System;
using System.Web;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumHttpUtility: HassiumObject
    {
        public HassiumHttpUtility()
        {
            Attributes.Add("htmlDecode",    new HassiumFunction(htmlDecode, 1));
            Attributes.Add("htmlEncode",    new HassiumFunction(htmlEncode, 1));
            Attributes.Add("urlDecode",     new HassiumFunction(urlDecode, 1));
            Attributes.Add("urlEncode",     new HassiumFunction(urlEncode, 1));
            AddType("HttpUtility");
        }

        private HassiumString htmlDecode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.HtmlDecode(HassiumString.Create(args[0]).Value));
        }
        private HassiumString htmlEncode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.HtmlEncode(HassiumString.Create(args[0]).Value));
        }
        private HassiumString urlDecode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.UrlDecode(HassiumString.Create(args[0]).Value));
        }
        private HassiumString urlEncode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.UrlEncode(HassiumString.Create(args[0]).Value));
        }
    }
}

