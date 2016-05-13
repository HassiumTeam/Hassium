using System;
using System.Net;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumWebClient: HassiumObject
    {
        public WebClient WebClient { get; private set; }
        public HassiumWebClient()
        {
            WebClient = new WebClient();

            Attributes.Add("downloadData",      new HassiumFunction(downloadData, 1));
            Attributes.Add("downloadFile",      new HassiumFunction(downloadFile, 1));
            Attributes.Add("downloadString",    new HassiumFunction(downloadString, 1));
            Attributes.Add("uploadData",        new HassiumFunction(uploadData, new int[] { 2, 3 }));
            Attributes.Add("uploadFile",        new HassiumFunction(uploadFile, new int[] { 2, 3 }));
            Attributes.Add("uploadString",      new HassiumFunction(uploadString, new int[] { 2, 3 }));
            AddType("WebClient");
        }

        private HassiumList downloadData(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            byte[] bytes = WebClient.DownloadData(HassiumString.Create(args[0]).Value);
            foreach (byte b in bytes)
                list.Add(vm, new HassiumChar((char)b));

            return list;
        }
        private HassiumNull downloadFile(VirtualMachine vm, HassiumObject[] args)
        {
            WebClient.DownloadFile(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value);
            return HassiumObject.Null;
        }
        private HassiumString downloadString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(WebClient.DownloadString(HassiumString.Create(args[0]).Value));
        }
        private HassiumString uploadData(VirtualMachine vm, HassiumObject[] args)
        {
            if (args.Length == 2)
                return new HassiumString(WebClient.UploadString(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value));
            else
                return new HassiumString(WebClient.UploadString(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value, HassiumString.Create(args[2]).Value));
        }
        private HassiumList uploadFile(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] res;
            if (args.Length == 2)
                res = WebClient.UploadFile(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value);
            else
                res = WebClient.UploadFile(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value, HassiumString.Create(args[2]).Value);
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (byte b in res)
                list.Add(vm, new HassiumChar((char)b));

            return list;
        }
        private HassiumString uploadString(VirtualMachine vm, HassiumObject[] args)
        {
            if (args.Length == 2)
                return new HassiumString(WebClient.UploadString(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value));
            else
                return new HassiumString(WebClient.UploadString(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value, HassiumString.Create(args[2]).Value));
        }
    }
}

