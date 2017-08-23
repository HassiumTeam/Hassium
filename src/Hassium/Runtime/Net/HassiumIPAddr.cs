﻿using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime.Net
{
    public class HassiumIPAddr : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new IPAddrTypeDef();

        public HassiumString Address { get; private set; }
        public HassiumInt Port { get; private set; }

        public HassiumIPAddr()
        {
            AddType(TypeDefinition);
        }

        public class IPAddrTypeDef : HassiumTypeDefinition
        {
            public IPAddrTypeDef() : base("IPAddr")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "address", new HassiumProperty(get_address) },
                    { INVOKE, new HassiumFunction(_new, 1, 2) },
                    { "port", new HassiumProperty(get_port) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [FunctionAttribute("func new (host : string) : IPaddr", "func new (host : string, port : int) : IPAddr")]
            public static HassiumIPAddr _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumIPAddr addr = new HassiumIPAddr();

                addr.Address = args[0].ToString(vm, args[0], location);
                addr.Port = args.Length == 2 ? args[1].ToInt(vm, args[1], location) : new HassiumInt(-1);

                return addr;
            }

            [FunctionAttribute("address { get; }")]
            public static HassiumString get_address(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumIPAddr).Address;
            }

            [FunctionAttribute("port { get; }")]
            public static HassiumInt get_port(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumIPAddr).Port;
            }

            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Address = (self as HassiumIPAddr).Address;
                var Port = (self as HassiumIPAddr).Port;

                if (Port.Int == -1)
                    return new HassiumString(Address.String);
                else
                    return new HassiumString(string.Format("{0}:{1}", Address.String, Port.Int));
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
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
