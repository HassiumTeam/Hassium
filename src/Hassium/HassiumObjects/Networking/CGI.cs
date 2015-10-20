using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class CGI : HassiumObject
    {
        private HassiumDictionary _get = null;
        private HassiumDictionary _post = null;

        public CGI()
        {
            if (Environment.GetEnvironmentVariables().Contains("QUERY_STRING"))
            {
                var query_string = Environment.GetEnvironmentVariable("QUERY_STRING");
                var query_args = new Dictionary<string, string>();

                foreach (var currentArg in query_string.Split('&'))
                {
                    if (!currentArg.Contains('=')) query_args.Add(currentArg, "");
                    else
                    {
                        var key = currentArg.Split('=')[0];
                        var value = HttpUtility.UrlDecode(currentArg.Split('=')[1]);
                        query_args.Add(key, value);
                    }
                }

                _get = new HassiumDictionary(query_args.ToDictionary(y => new HassiumString(y.Key),
                    y => new HassiumString(y.Value)));
            }
            else _get = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());

            Attributes.Add("get", new HassiumProperty("get", x => _get, null, true));
            Attributes.Add("post", new HassiumProperty("post", x =>
            {
                if (_post == null)
                {
                    var stdin = new StreamReader(Console.OpenStandardInput());
                    var query_string2 = stdin.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(query_string2))
                    {
                        var query_args = new Dictionary<string, string>();

                        foreach (var currentArg in query_string2.Split('&'))
                        {
                            if (!currentArg.Contains('=')) query_args.Add(currentArg, "");
                            else
                            {
                                var key = currentArg.Split('=')[0];
                                var value = HttpUtility.UrlDecode(currentArg.Split('=')[1]);
                                query_args.Add(key, value);
                            }
                        }

                        _post = new HassiumDictionary(query_args.ToDictionary(y => new HassiumString(y.Key),
                            y => new HassiumString(y.Value)));
                    }
                    else _post = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
                }
                return _post;
            }, null, true));

            Attributes.Add("documentRoot",
                new HassiumProperty("documentRoot",
                    x => Environment.GetEnvironmentVariable("DOCUMENT_ROOT") ?? "undefined", null, true));
            Attributes.Add("remote", new HassiumProperty("remoteIp", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("ip",
                    new HassiumProperty("ip", x => Environment.GetEnvironmentVariable("REMOTE_ADDR") ?? "undefined",
                        null, true));
                remote.Attributes.Add("host",
                    new HassiumProperty("host", x => Environment.GetEnvironmentVariable("REMOTE_HOST") ?? "undefined",
                        null, true));
                remote.Attributes.Add("port",
                    new HassiumProperty("port", x => Environment.GetEnvironmentVariable("REMOTE_PORT") ?? "undefined",
                        null, true));
                remote.Attributes.Add("user",
                    new HassiumProperty("user", x => Environment.GetEnvironmentVariable("REMOTE_USER") ?? "undefined",
                        null, true));
                remote.Attributes.Add("ident",
                    new HassiumProperty("ident", x => Environment.GetEnvironmentVariable("REMOTE_IDENT") ?? "undefined",
                        null, true));
                return remote;
            }, null, true));
            Attributes.Add("request", new HassiumProperty("request", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("method",
                    new HassiumProperty("method",
                        x => Environment.GetEnvironmentVariable("REQUEST_METHOD") ?? "undefined", null, true));
                remote.Attributes.Add("uri",
                    new HassiumProperty("uri", x => Environment.GetEnvironmentVariable("REQUEST_URI") ?? "undefined",
                        null, true));
                return remote;
            }, null, true));
            Attributes.Add("server", new HassiumProperty("server", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("admin",
                    new HassiumProperty("admin", x => Environment.GetEnvironmentVariable("SERVER_ADMIN") ?? "undefined",
                        null, true));
                remote.Attributes.Add("name",
                    new HassiumProperty("name", x => Environment.GetEnvironmentVariable("SERVER_NAME") ?? "undefined",
                        null, true));
                remote.Attributes.Add("port",
                    new HassiumProperty("port", x => Environment.GetEnvironmentVariable("SERVER_PORT") ?? "undefined",
                        null, true));
                remote.Attributes.Add("protocol",
                    new HassiumProperty("protocol",
                        x => Environment.GetEnvironmentVariable("SERVER_PROTOCOL") ?? "undefined",
                        null, true));
                remote.Attributes.Add("software",
                    new HassiumProperty("software",
                        x => Environment.GetEnvironmentVariable("SERVER_SOFTWARE") ?? "undefined", null, true));
                return remote;
            }, null, true));
            Attributes.Add("http", new HassiumProperty("http", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("accept",
                    new HassiumProperty("accept", x => Environment.GetEnvironmentVariable("HTTP_ACCEPT") ?? "undefined",
                        null, true));
                remote.Attributes.Add("cookie",
                    new HassiumProperty("cookie", x => Environment.GetEnvironmentVariable("HTTP_COOKIE") ?? "undefined",
                        null, true));
                remote.Attributes.Add("userMail",
                    new HassiumProperty("userMail", x => Environment.GetEnvironmentVariable("HTTP_FORM") ?? "undefined",
                        null, true));
                remote.Attributes.Add("host",
                    new HassiumProperty("host", x => Environment.GetEnvironmentVariable("HTTP_HOST") ?? "undefined",
                        null, true));
                remote.Attributes.Add("referer",
                    new HassiumProperty("referer",
                        x => Environment.GetEnvironmentVariable("HTTP_REFERER") ?? "undefined", null, true));
                remote.Attributes.Add("userAgent",
                    new HassiumProperty("userAgent",
                        x => Environment.GetEnvironmentVariable("HTTP_USER_AGENT") ?? "undefined", null, true));
                return remote;
            }, null, true));
            Attributes.Add("isHttps",
                new HassiumProperty("isHttps", x => Environment.GetEnvironmentVariable("HTTP_COOKIE") == "on", null,
                    true));
            Attributes.Add("path",
                new HassiumProperty("path", x => Environment.GetEnvironmentVariable("PATH") ?? "undefined", null, true));
            Attributes.Add("script", new HassiumProperty("script", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("fileName",
                    new HassiumProperty("fileName",
                        x => Environment.GetEnvironmentVariable("SCRIPT_FILENAME") ?? "undefined", null, true));
                remote.Attributes.Add("name",
                    new HassiumProperty("name", x => Environment.GetEnvironmentVariable("SCRIPT_NAME") ?? "undefined",
                        null, true));
                return remote;
            }, null, true));
            Attributes.Add("gatewayInterface",
                new HassiumProperty("gatewayInterface",
                    x => Environment.GetEnvironmentVariable("GATEWAY_INTERFACE") ?? "undefined", null, true));
            Attributes.Add("authType",
                new HassiumProperty("authType", x => Environment.GetEnvironmentVariable("AUTH_TYPE") ?? "undefined",
                    null, true));

            Attributes.Add("content", new HassiumProperty("content", arg =>
            {
                var remote = new HassiumObject();
                remote.Attributes.Add("length",
                    new HassiumProperty("length",
                        x => Environment.GetEnvironmentVariable("CONTENT_LENGTH") ?? "undefined", null, true));
                remote.Attributes.Add("type",
                    new HassiumProperty("type", x => Environment.GetEnvironmentVariable("CONTENT_TYPE") ?? "undefined",
                        null, true));
                return remote;
            }, null, true));
        }
    }
}
