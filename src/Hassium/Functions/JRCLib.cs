using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hassium
{
    public class IRC
    {
        private string server = "";
        private int port = 0;
        public string channel { get; set; }
        public string nick { get; set; }

        private TcpClient sock = new TcpClient();
        private TextReader input;
        private TextWriter output;

        public IRC(string server, int port, string channel, string nick)
        {
            //Set the values from the constructor to the instance of this connection
            this.server = server;
            this.channel = channel;
            this.nick = nick;
            this.port = port;
        }

        public void Connect()
        {
            //Connect and wait until we are in
            sock.Connect(server, port);
            Thread.Sleep(7000);

            //Something must have gone wrong and we didn't connect
            if (!sock.Connected)
            {
                return;
            }

            //Initialize our input and output streams
            input = new StreamReader(sock.GetStream());
            output = new StreamWriter(sock.GetStream());

            //Tell the server our user and nick
            output.Write(
                "USER " + this.nick + " 0 * :" + "Oikos" + "\r\n" +
                "NICK " + this.nick + "\r\n"
                );
            output.Flush();
        }

        //Gets the next message being sent by the server
        public IRCMessage GetMessage()
        {
            string buf;
            for (buf = input.ReadLine();; buf = input.ReadLine())
            {
                //Uncomment this to display everything from the server onto the console
                //Console.WriteLine(buf);

                //If we have a PRIVMSG we can return it right away
                if (buf.Contains("PRIVMSG"))
                {
                    return new IRCMessage(buf);
                }

                //Respond to pings with pongs
                else if (buf.StartsWith("PING "))
                {
                    output.Write(buf.Replace("PING", "PONG") + "\r\n");
                    output.Flush();
                }

                if (buf[0] != ':')
                    continue;

                //When we recieve the 001 we need to join the initial channel

                string type = buf.Split(' ')[1];
                if (type == "001")
                {   
                    output.Write("MODE " + this.nick + " +B\r\n" + "JOIN " + this.channel + "\r\n");
                    output.Flush();
                }
                else if (type == "353" || type == "JOIN" || type == "PART" || type == "QUIT" || type == "332")
                {
                    return new IRCMessage(buf, type);
                }
                else
                {
                    continue;
                }
            }
            return new IRCMessage("");
        }

        //Turns a message into parts that can fit inside a PRIVMSG to send
        private List<string> splitToChunks(string x, int maxLength)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < x.Length; i += maxLength)
            {
                if((i + maxLength) < x.Length)
                    a.Add(x.Substring(i, maxLength));
                else
                    a.Add(x.Substring(i));
            }

            return a;
        }

        //Sends raw commands in, useful for sending in server
        //commands that start with /
        public void SendRaw(string msg)
        {
            this.output.Write(msg + "\n\r");
            this.output.Flush();
        }

        //Sends a PRIVMSG to the current channel
        public void SendMsg(string msg)
        {
            List<string> parts = splitToChunks(msg, 400);
            string buffer = "";

            //These loops format text to break after a newline character
            for (int x = 0; x < parts.Count; x++)
            {
                for (int y = 0; y < parts[x].Length; y++)
                {
                    if (parts[x][y].ToString() != "\n" && parts[x][y].ToString() != "\r")
                    {
                        buffer += parts[x][y].ToString();
                    }
                    else
                    {
                        Thread buff = new Thread(() => SendMsg(buffer));
                        buff.Start();
                        buffer = "";
                    }
                }
                this.output.Write("PRIVMSG "+ this.channel +" :"+ buffer + "\n");
                this.output.Flush();
                buffer = "";
            }
        }

        public void Join(string channel)
        {
            SendRaw("JOIN " + channel);
            this.channel = channel;
        }
    }
}