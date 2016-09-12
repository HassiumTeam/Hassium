using System;
using System.Web;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumHttpUtility: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("HttpUtility");

        public HassiumHttpUtility()
        {
            AddType(TypeDefinition);
            AddAttribute("htmlDecode",    htmlDecode, 1);
            AddAttribute("htmlEncode",    htmlEncode, 1);
            AddAttribute("urlDecode",     urlDecode,  1);
            AddAttribute("urlEncode",     urlEncode,  1);
        }

        public HassiumString htmlDecode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.HtmlDecode(args[0].ToString(vm).String));
        }
        public HassiumString htmlEncode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.HtmlEncode(args[0].ToString(vm).String));
        }
        public HassiumString urlDecode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.UrlDecode(args[0].ToString(vm).String));
        }
        public HassiumString urlEncode(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(HttpUtility.UrlEncode(args[0].ToString(vm).String));
        }
    }
}