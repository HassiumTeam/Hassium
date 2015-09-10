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
            Attributes.Add("read", new InternalFunction(read));
            Attributes.Add("get", new InternalFunction(get));
            Attributes.Add("depth", new InternalFunction(depth));
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("dispose", new InternalFunction(dispose));
            Attributes.Add("getString", new InternalFunction(getString));
            Attributes.Add("nextResult", new InternalFunction(nextResult));
            Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumBool(Value.Read());
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return new HassiumString(Value[args[0].ToString()].ToString());
        }

        private HassiumObject depth(HassiumObject[] args)
        {
            return new HassiumDouble(Value.Depth);
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
            else if (args[0] is HassiumDouble)
                return new HassiumString(Value.GetString(((HassiumDouble)args[0]).ValueInt));
            else if (args[0] is HassiumInt)
                return new HassiumString(Value.GetString(((HassiumInt)args[0]).Value));
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

