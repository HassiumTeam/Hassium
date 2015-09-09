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
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumBool(Value.Read());
        }

        private HassiumObject get(HassiumObject[] args)
        {
            return new HassiumString(Value[args[0].ToString()].ToString());
        }
    }
}

