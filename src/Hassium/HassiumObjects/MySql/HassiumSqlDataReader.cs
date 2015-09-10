using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Sql
{
    public class HassiumSqlDataReader: HassiumObject
    {
        public MySqlDataReader Value { get; private set; }

        public HassiumSqlDataReader(MySqlDataReader value)
        {
            Value = value;
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("get", new InternalFunction(get, 1));
            Attributes.Add("depth", new InternalFunction(x => value.Depth, 0, true));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("getString", new InternalFunction(getString, 1));
            Attributes.Add("nextResult", new InternalFunction(nextResult, 0));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumBool(Value.Read());
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return new HassiumString(Value[args[0].ToString()].ToString());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();

            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();

            return null;
        }

        private HassiumObject getString(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumString(Value.GetString(args[0].ToString()));
            else if (args[0] is HassiumDouble || args[0] is HassiumInt)
                return new HassiumString(Value.GetString(args[0].HInt()));
            else
                throw new Exception("Argument to getString() was not the correct format");
        }

        private HassiumObject nextResult(HassiumObject[] args)
        {
            return new HassiumBool(Value.NextResult());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

