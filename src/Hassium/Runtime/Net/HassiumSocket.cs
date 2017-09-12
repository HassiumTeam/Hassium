using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;

namespace Hassium.Runtime.Net
{
	public class HassiumSocket : HassiumObject
	{
		public static new HassiumTypeDefinition TypeDefinition = new SocketTypeDef();

		public TcpClient Client { get; set; }
		public BinaryReader Reader { get; set; }
		public BinaryWriter Writer { get; set; }

		public StreamReader StreamReader { get; set; }
		public StreamWriter StreamWriter { get; set; }

		public bool AutoFlush { get; set; }
		public bool Closed { get; set; }

		public HassiumSocket()
		{
			AddType(TypeDefinition);
			Closed = false;
			AutoFlush = true;
		}

		public class SocketTypeDef : HassiumTypeDefinition
		{
			public SocketTypeDef() : base("Socket")
			{
				BoundAttributes = new Dictionary<string, HassiumObject>()
				{
					{ "autoflush", new HassiumProperty(get_autoflush, set_autoflush)  },
					{ "close", new HassiumFunction(close, 0)  },
					{ "connect", new HassiumFunction(connect, -1)  },
					{ ENTER, new HassiumFunction(enter, 0) },
					{ EXIT, new HassiumFunction(exit, 0) },
					{ "fromip", new HassiumProperty(get_fromip)  },
					{ "flush", new HassiumFunction(flush, 0)  },
					{ INVOKE, new HassiumFunction(_new, 0, 1, 2) },
					{ "isconnected", new HassiumProperty(get_isconnected)  },
					{ "readbyte", new HassiumFunction(readbyte, 0)  },
					{ "readbytes", new HassiumFunction(readbytes, 1)  },
					{ "readint", new HassiumFunction(readint, 0)  },
					{ "readline", new HassiumFunction(readline, 0)  },
					{ "readlong", new HassiumFunction(readlong, 0)  },
					{ "readshort", new HassiumFunction(readshort, 0)  },
					{ "readstring", new HassiumFunction(readstring, 0)  },
					{ "toip", new HassiumProperty(get_toip)  },
					{ "writebyte", new HassiumFunction(writebyte, 1)  },
					{ "writefloat", new HassiumFunction(writefloat, 1)  },
					{ "writeint", new HassiumFunction(writeint, 1)  },
					{ "writeline", new HassiumFunction(writeline, 1)  },
					{ "writelist", new HassiumFunction(writelist, 1)  },
					{ "writelong", new HassiumFunction(writelong, 1)  },
					{ "writeshort", new HassiumFunction(writeshort, 1)  },
					{ "writestring", new HassiumFunction(writestring, 1)  },
				};
			}

			[DocStr(
				"@desc Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.",
				"@optional ip The Net.IPAddr object that has the ip and port.",
				"@optional ip The string ip address.",
				"@optional port The int port.",
				"@optional ssl The bool indicating if the Socket will use ssl.",
				"@returns The new Socket object."
				)]
			[FunctionAttribute("func new () : Socket", "func new (ip : IPAddr) : Socket", "func new (ip : string, port : int) : Socket", "func new (ip : string, port : int, ssl : bool) : Socket")]
			public HassiumObject _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				HassiumSocket socket = new HassiumSocket();
				Stream stream = null;

				switch (args.Length)
				{
					case 0:
						socket.Client = new TcpClient();
						stream = new MemoryStream();
						break;
					case 1:
						if (args[0] is HassiumIPAddr)
						{
							var ipAddr = args[0] as HassiumIPAddr;
							socket.Client = new TcpClient(ipAddr.Address.String, (int)ipAddr.Port.Int);
							stream = socket.Client.GetStream();
						}
						else
							return _new(vm, self, location, HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, args[0].ToString(vm, args[0], location)));
						break;
					case 2:
						socket.Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
						stream = socket.Client.GetStream();
						break;
					case 3:
						socket.Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
						if (args[2].ToBool(vm, args[2], location).Bool)
							stream = new SslStream(socket.Client.GetStream(), false, new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true), null);
						else
							stream = socket.Client.GetStream();
						break;
				}
				socket.Reader = new BinaryReader(stream);
				socket.Writer = new BinaryWriter(stream);
				socket.StreamReader = new StreamReader(stream);
				socket.StreamWriter = new StreamWriter(stream);

				return socket;
			}

			[DocStr(
				"@desc Gets the mutable bool indicating if the socket will autoflush.",
				"@returns True if the stream will automatically flush, otherwise false."
				)]
			[FunctionAttribute("autoflush { get; }")]
			public HassiumBool get_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				return new HassiumBool((self as HassiumSocket).AutoFlush);
			}
			[DocStr(
				"@desc Sets the mutable bool determining if the socket will autoflush.",
				"@returns null."
				)]
			[FunctionAttribute("autofluah { set; }")]
			public HassiumNull set_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				(self as HassiumSocket).AutoFlush = args[0].ToBool(vm, args[0], location).Bool;

				return Null;
			}

			[DocStr(
				"@desc Closes the socket.",
				"@returns null."
				)]
			[FunctionAttribute("func close () : null")]
			public HassiumNull close(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				(self as HassiumSocket).Client.Close();
				return Null;
			}

			[DocStr(
				"@desc Connects the socket to either the specified Net.IPAddr object or the specified string ip and int port.",
				"@optional ip The Net.IPAddr object to connect to.",
				"@optional ip The string ip address to connect to.",
				"@optional port The port to connect to.",
				"@returns null."
				)]
			[FunctionAttribute("func connect (ip : IPAddr) : null", "func connect (ip : string, port : int) : null")]
			public HassiumNull connect(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var Client = (self as HassiumSocket).Client;
				switch (args.Length)
				{
					case 1:
						if (args[0] is HassiumIPAddr)
						{
							var ipAddr = args[0] as HassiumIPAddr;
							Client = new TcpClient(ipAddr.Address.String, (int)ipAddr.Port.Int);
							return Null;
						}
						else
							return connect(vm, self, location, HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, args[0].ToString(vm, args[0], location)));
					case 2:
						Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
						return Null;
				}
				return Null;
			}

			[DocStr(
				"@desc Implements the 'enter' portion of the with statement. Does nothing.",
				"@returns null."
				)]
			[FunctionAttribute("func __enter__ () : null")]
			public HassiumNull enter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				return Null;
			}

			[DocStr(
				"@desc Implements the 'exit' portion of the with statement. Closes the socket.",
				"@returns null."
				)]
			[FunctionAttribute("func __exit__ () : null")]
			public HassiumNull exit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				return close(vm, self, location, args);
			}

			[DocStr(
				"@desc Gets the readonly Net.IPAddr of the ip the socket is connecting from (local ip).",
				"@returns The Net.IPAddr object of the from address."
				)]
			[FunctionAttribute("fromip { get; }")]
			public HassiumObject get_fromip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}
				string[] parts = (socket.Client.Client.LocalEndPoint as IPEndPoint).ToString().Split(':');
				return HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, new HassiumString(parts[0]), new HassiumString(parts[1]));
			}

			[DocStr(
				"@desc Flushes the socket stream.",
				"@returns null."
				)]
			[FunctionAttribute("func flush () : null")]
			public HassiumNull flush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Gets a readonly bool indicating if the socket is currently connected.",
				"@returns true if the socket is connected, otherwise false."
				)]
			[FunctionAttribute("isconnected { get; }")]
			public HassiumBool get_isconnected(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				return new HassiumBool((self as HassiumSocket).Client.Connected);
			}

			[DocStr(
				"@desc Reads a single byte from the stream and returns it as a char.",
				"@returns The byte as char."
				)]
			[FunctionAttribute("func readbyte () : char")]
			public HassiumObject readbyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumChar((char)socket.Reader.ReadBytes(1)[0]);
			}

			[DocStr(
				"@desc Reads the specified count of bytes from the stream and returns them in a list.",
				"@param count The amount of bytes to read.",
				"@returns A list containing the specified amount of bytes."
				)]
			[FunctionAttribute("func readbytes (count : int) : list")]
			public HassiumObject readbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				HassiumByteArray list = new HassiumByteArray(new byte[0], new HassiumObject[0]);
				int count = (int)args[0].ToInt(vm, args[0], location).Int;
				for (int i = 0; i < count; i++)
					HassiumList.add(vm, list, location, new HassiumChar((char)socket.Reader.ReadBytes(1)[0]));

				return list;
			}

			[DocStr(
				"@desc Reads a single float from the stream and returns it.",
				"@returns The read float."
				)]
			[FunctionAttribute("func readfloat () : float")]
			public HassiumObject readfloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);

				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumFloat(socket.Reader.ReadDouble());
			}

			[DocStr(
				"@desc Reads a single 32-bit integer from the stream and returns it.",
				"@returns The read 32-bit int."
				)]
			[FunctionAttribute("func readint () : int")]
			public HassiumObject readint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);

				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumInt(socket.Reader.ReadInt32());
			}

			[DocStr(
				"@desc Reads a line from the stream and returns it as a string.",
				"@returns The read line string."
				)]
			[FunctionAttribute("func readline () : string")]
			public HassiumObject readline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);

				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumString(socket.StreamReader.ReadLine());
			}

			[DocStr(
				"@desc Reads a single 64-bit integer from the stream and returns it.",
				"@returns The read 64-bit int."
				)]
			[FunctionAttribute("func readlong () : int")]
			public HassiumObject readlong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumInt(socket.Reader.ReadInt64());
			}

			[DocStr(
				"@desc Reads a single 16-bit integer from the stream and returns it.",
				"@returns The read 16-bit int."
				)]
			[FunctionAttribute("func readshort () : int")]
			public HassiumObject readshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumInt(socket.Reader.ReadInt16());
			}

			[DocStr(
				"@desc Reads a single string from the stream and returns it.",
				"@returns The read string."
				)]
			[FunctionAttribute("func readstring () : string")]
			public HassiumObject readstring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				return new HassiumString(socket.Reader.ReadString());
			}

			[DocStr(
				"@desc Gets the readonly Net.IPAddr of the ip the socket is connecting to.",
				"@returns The Net.IPAddr object of the to address."
				)]
			[FunctionAttribute("toip { get; }")]
			public HassiumObject get_toip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}
				string[] parts = (socket.Client.Client.RemoteEndPoint as IPEndPoint).ToString().Split(':');
				return HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, new HassiumString(parts[0]), new HassiumString(parts[1]));
			}

			[DocStr(
				"@desc Writes the given single byte to the file stream.",
				"@param b The char to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writebyte (b : char) : null")]
			public HassiumNull writebyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write((byte)args[0].ToChar(vm, args[0], location).Char);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the given single float to the file stream.",
				"@param f The float to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writefloat (f : float) : null")]
			public HassiumNull writefloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write(args[0].ToFloat(vm, args[0], location).Float);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the given single 32-bit integer to the file stream.",
				"@param i The 32-bit int to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writeint (i : int) : null")]
			public HassiumNull writeint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write((int)args[0].ToInt(vm, args[0], location).Int);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the given string line to the file stream, followed by a newline.",
				"@param str The string to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writeline (str : string) : null")]
			public HassiumNull writeline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				string str = args[0].ToString(vm, args[0], location).String;

				foreach (var c in str)
					socket.Writer.Write(c);
				socket.Writer.Write('\r');
				socket.Writer.Write('\n');

				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the byte value of each element in the given list to the file stream.",
				"@param l The list to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writelist (l : list) : null")]
			public HassiumNull writelist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				foreach (var i in args[0].ToList(vm, args[0], location).Values)
					writeHassiumObject(socket.Writer, i, vm, location);

				return Null;
			}

			[DocStr(
				"@desc Writes the given 64-bit integer to the file stream.",
				"@param l The 64-bit int to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writelong (l : int) : null")]
			public HassiumNull writelong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write(args[0].ToInt(vm, args[0], location).Int);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the given 16-bit integer to the file stream.",
				"@param s The 16-bit int to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writeshort (s : int) : null")]
			public HassiumNull writeshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write((short)args[0].ToInt(vm, args[0], location).Int);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			[DocStr(
				"@desc Writes the given string to the file stream.",
				"@param str The string to write.",
				"@returns null."
				)]
			[FunctionAttribute("func writestring (str : string) : null")]
			public HassiumNull writestring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
			{
				var socket = (self as HassiumSocket);
				if (socket.Closed)
				{
					vm.RaiseException(HassiumSocketClosedException.SocketClosedExceptionTypeDef._new(vm, null, location, this));
					return Null;
				}

				socket.Writer.Write(args[0].ToString(vm, args[0], location).String);
				if (socket.AutoFlush)
					socket.Writer.Flush();
				return Null;
			}

			private void writeHassiumObject(BinaryWriter writer, HassiumObject obj, VirtualMachine vm, SourceLocation location)
			{
				var type = obj.Type();

				if (type == HassiumBool.TypeDefinition)
					writer.Write(obj.ToBool(vm, obj, location).Bool);
				else if (type == HassiumChar.TypeDefinition)
					writer.Write((byte)obj.ToChar(vm, obj, location).Char);
				else if (type == HassiumFloat.TypeDefinition)
					writer.Write(obj.ToFloat(vm, obj, location).Float);
				else if (type == HassiumInt.TypeDefinition)
					writer.Write(obj.ToInt(vm, obj, location).Int);
				else if (type == HassiumList.TypeDefinition)
					foreach (var item in obj.ToList(vm, obj, location).Values)
						writeHassiumObject(writer, item, vm, location);
				else if (type == HassiumString.TypeDefinition)
					writer.Write(obj.ToString(vm, obj, location).String);
				else if (type == HassiumTuple.TypeDefinition)
					foreach (var item in obj.ToTuple(vm, obj, location).Values)
						writeHassiumObject(writer, item, vm, location);
			}
		}

		public override bool ContainsAttribute(string attrib)
		{
			return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
		}

		public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
		{
			if (BoundAttributes.ContainsKey(attrib))
				return BoundAttributes[attrib];
			else
				return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
		}

		public override Dictionary<string, HassiumObject> GetAttributes()
		{
			foreach (var pair in TypeDefinition.BoundAttributes)
				if (!BoundAttributes.ContainsKey(pair.Key))
					BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
			return BoundAttributes;
		}
	}
}