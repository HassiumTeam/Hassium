using System;
using System.Net;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumWebClient: HassiumObject
    {
        public WebClient WebClient { get; private set; }
        public HassiumWebClient()
        {
            WebClient = new WebClient();

            AddAttribute("downloadData",      downloadData,                 1);
            AddAttribute("downloadFile",      downloadFile,                 1);
            AddAttribute("downloadString",    downloadString,               1);
            AddAttribute("uploadData",        uploadData,  new int[] { 2, 3 });
            AddAttribute("uploadFile",        uploadFile,  new int[] { 2, 3 });
            AddAttribute("uploadString",      uploadString,new int[] { 2, 3 });
        }

        private HassiumList downloadData(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            byte[] bytes = WebClient.DownloadData(args[0].ToString(vm).String);
            foreach (byte b in bytes)
                list.Add(vm, new HassiumChar((char)b));

            return list;
        }
        private HassiumNull downloadFile(VirtualMachine vm, HassiumObject[] args)
        {
            WebClient.DownloadFile(args[0].ToString(vm).String, args[1].ToString(vm).String);
            return HassiumObject.Null;
        }
        private HassiumString downloadString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(WebClient.DownloadString(args[0].ToString(vm).String));
        }
        private HassiumString uploadData(VirtualMachine vm, HassiumObject[] args)
        {
            if (args.Length == 2)
                return new HassiumString(WebClient.UploadString(args[0].ToString(vm).String, args[1].ToString(vm).String));
            else
                return new HassiumString(WebClient.UploadString(args[0].ToString(vm).String, args[1].ToString(vm).String, args[2].ToString(vm).String));
        }
        private HassiumList uploadFile(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] res;
            if (args.Length == 2)
                res = WebClient.UploadFile(args[0].ToString(vm).String, args[1].ToString(vm).String);
            else
                res = WebClient.UploadFile(args[0].ToString(vm).String, args[1].ToString(vm).String, args[2].ToString(vm).String);
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (byte b in res)
                list.Add(vm, new HassiumChar((char)b));

            return list;
        }
        private HassiumString uploadString(VirtualMachine vm, HassiumObject[] args)
        {
            if (args.Length == 2)
                return new HassiumString(WebClient.UploadString(args[0].ToString(vm).String, args[1].ToString(vm).String));
            else
                return new HassiumString(WebClient.UploadString(args[0].ToString(vm).String, args[1].ToString(vm).String, args[2].ToString(vm).String));
        }
    }
}
