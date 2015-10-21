using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Hassium.Functions;
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
                    var query_string = "";
                    if (Environment.GetEnvironmentVariable("CONTENT_LENGTH") != null)
                    {
                        int PostedDataLength = Convert.ToInt32(Environment.GetEnvironmentVariable("CONTENT_LENGTH"));
                        for (int i = 0; i < PostedDataLength; i++)
                            query_string += Convert.ToChar(Console.Read()).ToString();
                    }
                    else
                    {
                        var stdin = new StreamReader(Console.OpenStandardInput());
                        query_string = stdin.ReadToEnd();
                    }
                    if (!string.IsNullOrWhiteSpace(query_string))
                    {
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
                var request = new HassiumObject();
                request.Attributes.Add("method",
                    new HassiumProperty("method",
                        x => Environment.GetEnvironmentVariable("REQUEST_METHOD") ?? "undefined", null, true));
                request.Attributes.Add("uri",
                    new HassiumProperty("uri", x => Environment.GetEnvironmentVariable("REQUEST_URI") ?? "undefined",
                        null, true));
                request.Attributes.Add("scheme",
                    new HassiumProperty("scheme",
                        x => Environment.GetEnvironmentVariable("REQUEST_SCHEME") ?? "undefined", null, true));
                return request;
            }, null, true));
            Attributes.Add("server", new HassiumProperty("server", arg =>
            {
                var server = new HassiumObject();
                server.Attributes.Add("admin",
                    new HassiumProperty("admin", x => Environment.GetEnvironmentVariable("SERVER_ADMIN") ?? "undefined",
                        null, true));
                server.Attributes.Add("address",
                    new HassiumProperty("address", x => Environment.GetEnvironmentVariable("SERVER_ADDR") ?? "undefined",
                        null, true));
                server.Attributes.Add("name",
                    new HassiumProperty("name", x => Environment.GetEnvironmentVariable("SERVER_NAME") ?? "undefined",
                        null, true));
                server.Attributes.Add("port",
                    new HassiumProperty("port", x => Environment.GetEnvironmentVariable("SERVER_PORT") ?? "undefined",
                        null, true));
                server.Attributes.Add("protocol",
                    new HassiumProperty("protocol",
                        x => Environment.GetEnvironmentVariable("SERVER_PROTOCOL") ?? "undefined",
                        null, true));
                server.Attributes.Add("software",
                    new HassiumProperty("software",
                        x => Environment.GetEnvironmentVariable("SERVER_SOFTWARE") ?? "undefined", null, true));
                server.Attributes.Add("signature",
                    new HassiumProperty("signature",
                        x => Environment.GetEnvironmentVariable("SERVER_SIGNATURE") ?? "undefined",
                        null, true));
                return server;
            }, null, true));
            Attributes.Add("http", new HassiumProperty("http", arg =>
            {
                var http = new HassiumObject();
                http.Attributes.Add("accept", new HassiumProperty("accept", arg2 =>
                {
                    var accept = new HassiumObject();
                    accept.Attributes.Add("mimeType",
                        new HassiumProperty("mimeType",
                            x => Environment.GetEnvironmentVariable("HTTP_ACCEPT") ?? "undefined", null, true));
                    accept.Attributes.Add("encoding",
                        new HassiumProperty("encoding",
                            x => Environment.GetEnvironmentVariable("HTTP_ACCEPT_ENCODING") ?? "undefined", null, true));
                    accept.Attributes.Add("language",
                        new HassiumProperty("language",
                            x => Environment.GetEnvironmentVariable("HTTP_ACCEPT_LANGUAGE") ?? "undefined", null, true));
                    return accept;
                }, null, true));
                http.Attributes.Add("cacheControl",
                    new HassiumProperty("cacheControl",
                        x => Environment.GetEnvironmentVariable("HTTP_CACHE_CONTROL") ?? "undefined",
                        null, true));
                http.Attributes.Add("connection",
                    new HassiumProperty("connection",
                        x => Environment.GetEnvironmentVariable("HTTP_CONNECTION") ?? "undefined",
                        null, true));
                http.Attributes.Add("cookie",
                    new HassiumProperty("cookie", x => Environment.GetEnvironmentVariable("HTTP_COOKIE") ?? "undefined",
                        null, true));
                http.Attributes.Add("doNotTrack",
                    new HassiumProperty("doNotTrack", x => Environment.GetEnvironmentVariable("HTTP_DNT") == "1",
                        null, true));
                http.Attributes.Add("userMail",
                    new HassiumProperty("userMail", x => Environment.GetEnvironmentVariable("HTTP_FORM") ?? "undefined",
                        null, true));
                http.Attributes.Add("host",
                    new HassiumProperty("host", x => Environment.GetEnvironmentVariable("HTTP_HOST") ?? "undefined",
                        null, true));
                http.Attributes.Add("referer",
                    new HassiumProperty("referer",
                        x => Environment.GetEnvironmentVariable("HTTP_REFERER") ?? "undefined", null, true));
                http.Attributes.Add("userAgent",
                    new HassiumProperty("userAgent",
                        x => Environment.GetEnvironmentVariable("HTTP_USER_AGENT") ?? "undefined", null, true));
                http.Attributes.Add("upgradeInsecureRequests",
                    new HassiumProperty("upgradeInsecureRequests",
                        x => Environment.GetEnvironmentVariable("HTTP_UPGRADE_INSECURE_REQUESTS") == "1",
                        null, true));
                return http;
            }, null, true));
            Attributes.Add("isHttps",
                new HassiumProperty("isHttps", x => Environment.GetEnvironmentVariable("HTTP_COOKIE") == "on", null,
                    true));
            Attributes.Add("path",
                new HassiumProperty("path", x => Environment.GetEnvironmentVariable("PATH") ?? "undefined", null, true));
            Attributes.Add("script", new HassiumProperty("script", arg =>
            {
                var script = new HassiumObject();
                script.Attributes.Add("fileName",
                    new HassiumProperty("fileName",
                        x => Environment.GetEnvironmentVariable("SCRIPT_FILENAME") ?? "undefined", null, true));
                script.Attributes.Add("name",
                    new HassiumProperty("name", x => Environment.GetEnvironmentVariable("SCRIPT_NAME") ?? "undefined",
                        null, true));
                return script;
            }, null, true));
            Attributes.Add("gatewayInterface",
                new HassiumProperty("gatewayInterface",
                    x => Environment.GetEnvironmentVariable("GATEWAY_INTERFACE") ?? "undefined", null, true));
            Attributes.Add("authType",
                new HassiumProperty("authType", x => Environment.GetEnvironmentVariable("AUTH_TYPE") ?? "undefined",
                    null, true));

            Attributes.Add("content", new HassiumProperty("content", arg =>
            {
                var content = new HassiumObject();
                content.Attributes.Add("length",
                    new HassiumProperty("length",
                        x => Environment.GetEnvironmentVariable("CONTENT_LENGTH") ?? "undefined", null, true));
                content.Attributes.Add("type",
                    new HassiumProperty("type", x => Environment.GetEnvironmentVariable("CONTENT_TYPE") ?? "undefined",
                        null, true));
                return content;
            }, null, true));

            Attributes.Add("context", new HassiumProperty("context", arg =>
            {
                var context = new HassiumObject();
                context.Attributes.Add("documentRoot",
                    new HassiumProperty("documentRoot",
                        x => Environment.GetEnvironmentVariable("CONTEXT_DOCUMENT_ROOT") ?? "undefined", null, true));
                context.Attributes.Add("prefix",
                    new HassiumProperty("prefix",
                        x => Environment.GetEnvironmentVariable("CONTEXT_PREFIX") ?? "undefined",
                        null, true));
                return context;
            }, null, true));
            Attributes.Add("uniqueId",
                new HassiumProperty("uniqueId", x => Environment.GetEnvironmentVariable("UNIQUE_ID") ?? "undefined",
                    null, true));
            Attributes.Add("currentDirectory",
                new HassiumProperty("currentDirectory", x => Environment.GetEnvironmentVariable("PWD") ?? "undefined",
                    null, true));
            Attributes.Add("shellLevel",
                new HassiumProperty("shellLevel", x => Environment.GetEnvironmentVariable("SHLVL") ?? "undefined", null,
                    true));

            /*Attributes.Add("session", new HassiumProperty("session", arg =>
            {
                var session = new HassiumDictionary(_sessionvalue.ToDictionary());
                session.Attributes.Add("start", new InternalFunction(x =>
                {
                    Console.WriteLine("Set-Cookie");
                    _sessionfolder = x[0].ToString();
                    _sessionvalue = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
                    session.Value = _sessionvalue.Value;
                    _sessionstarted = true;
                    return null;
                }, 1));
                session.Attributes.Add("stop", new InternalFunction(x =>
                {
                    _sessionstarted = false;
                    _sessionfolder = "";
                    _sessionvalue = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
                    session.Value = _sessionvalue.Value;
                    return null;
                }, 0));

                session.OnValueChanged +=
                    delegate {
                                 _sessionvalue = session;
                    };



                return session;
            }, null, true));*/
        }

        private static bool _sessionstarted = false;
        private static HassiumDictionary _sessionvalue = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
        private static string _sessionfolder = "";
    }
}
