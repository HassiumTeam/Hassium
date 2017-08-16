using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Hassium.Runtime.Net
{
    public class HassiumCGI : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("CGI");

        public HassiumDictionary Get { get; private set; }
        public HassiumDictionary Post { get; private set; }

        public HassiumCGI()
        {
            AddType(TypeDefinition);

            Get = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            string query_string;
            if (Environment.GetEnvironmentVariables().Contains("QUERY_STRING"))
            {
                query_string = Environment.GetEnvironmentVariable("QUERY_STRING");

                foreach (var arg in query_string.Split('&'))
                {
                    if (!arg.Contains("=")) Get.Dictionary.Add(new HassiumString(arg), new HassiumString(string.Empty));
                    else
                    {
                        var key = arg.Split('=')[0];
                        var value = HttpUtility.UrlDecode(arg.Split('=')[1]);
                        Get.Dictionary.Add(new HassiumString(key), new HassiumString(value));
                    }
                }
            }
            Post = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            if (Environment.GetEnvironmentVariable("CONTENT_LENGTH") != null)
            {
                query_string = string.Empty;
                int PostedDataLength = Convert.ToInt32(Environment.GetEnvironmentVariable("CONTENT_LENGTH"));
                for (int i = 0; i < PostedDataLength; i++)
                    query_string += Convert.ToChar(Console.Read()).ToString();
            }
            else
            {
                var stdin = new StreamReader(Console.OpenStandardInput());
                query_string = string.Empty;//stdin.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(query_string))
            {
                foreach (var currentArg in query_string.Split('&'))
                {
                    if (!currentArg.Contains("=")) Post.Dictionary.Add(new HassiumString(currentArg), new HassiumString(""));
                    else
                    {
                        var key = currentArg.Split('=')[0];
                        var value = HttpUtility.UrlDecode(currentArg.Split('=')[1]);
                        Post.Dictionary.Add(new HassiumString(key), new HassiumString(value));
                    }
                }
            }

                Attributes.Add("documentRoot",
               new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("DOCUMENT_ROOT") ?? "undefined")));
            Attributes.Add("remote", new HassiumProperty((vm, location, args) =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("ip",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REMOTE_ADDR") ?? "undefined")));
                remote.Attributes.Add("host",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REMOTE_HOST") ?? "undefined")));
                remote.Attributes.Add("port",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REMOTE_PORT") ?? "undefined")));
                remote.Attributes.Add("user",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REMOTE_USER") ?? "undefined")));
                remote.Attributes.Add("ident",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REMOTE_IDENT") ?? "undefined")));
                return remote;
            }));
            Attributes.Add("request", new HassiumProperty((vm, location, args) =>
            {
                var request = new HassiumObject();
                request.Attributes.Add("method",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REQUEST_METHOD") ?? "undefined")));
                request.Attributes.Add("uri",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REQUEST_URI") ?? "undefined")));
                request.Attributes.Add("scheme",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("REQUEST_SCHEME") ?? "undefined")));
                return request;
            }));
            Attributes.Add("server", new HassiumProperty((vm, location, args) =>
            {
                var server = new HassiumObject();
                server.Attributes.Add("admin",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_ADMIN") ?? "undefined")));
                server.Attributes.Add("address",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_ADDR") ?? "undefined")));
                server.Attributes.Add("name",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_NAME") ?? "undefined")));
                server.Attributes.Add("port",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_PORT") ?? "undefined")));
                server.Attributes.Add("protocol",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_PROTOCOL") ?? "undefined")));
                server.Attributes.Add("software",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_SOFTWARE") ?? "undefined")));
                server.Attributes.Add("signature",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SERVER_SIGNATURE") ?? "undefined")));
                return server;
            }));
            Attributes.Add("http", new HassiumProperty((vm, location, args) =>
            {
                var http = new HassiumObject();
                http.Attributes.Add("accept", new HassiumProperty((vm1, location1, args1) =>
                {
                    var accept = new HassiumObject();
                    accept.Attributes.Add("mimeType",
                        new HassiumProperty((vm2, location2, args2) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_ACCEPT") ?? "undefined")));
                    accept.Attributes.Add("charset",
                        new HassiumProperty((vm2, location2, args2) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_ACCEPT_CHARSET") ?? "undefined")));
                    accept.Attributes.Add("encoding",
                        new HassiumProperty((vm2, location2, args2) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_ACCEPT_ENCODING") ?? "undefined")));
                    accept.Attributes.Add("language",
                        new HassiumProperty((vm2, location2, args2) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_ACCEPT_LANGUAGE") ?? "undefined")));
                    return accept;
                }));
                http.Attributes.Add("cacheControl",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_CACHE_CONTROL") ?? "undefined")));
                http.Attributes.Add("connection",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_CONNECTION") ?? "undefined")));
                http.Attributes.Add("cookie",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_COOKIE") ?? "undefined")));
                http.Attributes.Add("doNotTrack",
                    new HassiumProperty((vm1, location1, args1) => new HassiumBool(Environment.GetEnvironmentVariable("HTTP_DNT") == "1")));
                http.Attributes.Add("userMail",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_FROM") ?? "undefined")));
                http.Attributes.Add("host",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_HOST") ?? "undefined")));
                http.Attributes.Add("referer",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_REFERER") ?? "undefined")));
                http.Attributes.Add("userAgent",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("HTTP_USER_AGENT") ?? "undefined")));
                http.Attributes.Add("upgradeInsecureRequests",
                    new HassiumProperty(
                        (vm1, location1, args1) => new HassiumBool(Environment.GetEnvironmentVariable("HTTP_UPGRADE_INSECURE_REQUESTS") == "1")));
                return http;
            }));
            Attributes.Add("isHttps",
                new HassiumProperty((vm, location, args) => new HassiumBool(Environment.GetEnvironmentVariable("HTTP_COOKIE") == "on")));
            Attributes.Add("path",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("PATH") ?? "undefined")));
            Attributes.Add("script", new HassiumProperty((vm, location, args) =>
            {
                var script = new HassiumObject();
                script.Attributes.Add("fileName",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SCRIPT_FILENAME") ?? "undefined")));
                script.Attributes.Add("name",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("SCRIPT_NAME") ?? "undefined")));
                return script;
            }));
            Attributes.Add("gatewayInterface",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("GATEWAY_INTERFACE") ?? "undefined")));
            Attributes.Add("authType",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("AUTH_TYPE") ?? "undefined")));

            Attributes.Add("content", new HassiumProperty((vm, location, args) =>
            {
                var content = new HassiumObject();
                content.Attributes.Add("length",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("CONTENT_LENGTH") ?? "undefined")));
                content.Attributes.Add("type",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("CONTENT_TYPE") ?? "undefined")));
                return content;
            }));

            Attributes.Add("context", new HassiumProperty((vm, location, args) =>
            {
                var context = new HassiumObject();
                context.Attributes.Add("documentRoot",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("CONTEXT_DOCUMENT_ROOT") ?? "undefined")));
                context.Attributes.Add("prefix",
                    new HassiumProperty((vm1, location1, args1) => new HassiumString(Environment.GetEnvironmentVariable("CONTEXT_PREFIX") ?? "undefined")));
                return context;
            }));
            Attributes.Add("uniqueId",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("UNIQUE_ID") ?? "undefined")));
            Attributes.Add("currentDirectory",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("PWD") ?? "undefined")));
            Attributes.Add("shellLevel",
                new HassiumProperty((vm, location, args) => new HassiumString(Environment.GetEnvironmentVariable("SHLVL") ?? "undefined")));


        }

        public HassiumDictionary get_get(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Get;
        }
    }
}
 