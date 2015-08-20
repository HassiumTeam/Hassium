using System;

namespace Hassium
{
    //This takes in a PRIVMSG and breaks it up into the essential pieces
    public class IRCMessage
    {
        public IRCMessage(string message, string type = "-1")
        {
            this.MsgType = type;

            if (type == "353" || type == "332")
            {
                this.Channel = message.Substring(message.IndexOf("#"), message.Substring(message.IndexOf("#")).IndexOf(" "));
                this.Message = message.Substring(message.IndexOf(this.Channel) + this.Channel.Length + 2);
            }
            else if (type == "JOIN")
            {
                string person = message.Substring(1, message.IndexOf("!") - 1);
                this.Channel = message.Substring(message.IndexOf("#"));
                this.Message = person + " has joined " + this.Channel;
            }
            else if (type == "PART")
            {
                string person = message.Substring(1, message.IndexOf("!") - 1);
                this.Channel = message.Substring(message.IndexOf("#"));
                this.Message = person + " has left " + this.Channel;
            }
            else if (type == "QUIT")
            {
                string person = message.Substring(1, message.IndexOf("!") - 1);
                this.Channel = message.Substring(message.IndexOf("#"));
                string msg = message.Substring(message.LastIndexOf(this.Channel));
                this.Message = person + " has quit: " + msg;
            }
            else
            {
                this.Channel = message.Substring(message.IndexOf("#"), message.Substring(message.IndexOf("#")).IndexOf(" "));
                this.Sender = message.Substring(message.IndexOf(":") + 1, message.IndexOf("!") - 1);
                this.Message = message.Substring(message.IndexOf(this.Channel) + this.Channel.Length + 2);
            }

        }

        public string Sender { get; set; }

        public string Message { get; set; }

        public string Channel { get; set; }

        public string MsgType { get; set; }
    }
}