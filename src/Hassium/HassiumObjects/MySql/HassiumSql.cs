using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Sql
{
    public class HassiumSql: HassiumObject
    {
        public MySqlConnection Value { get; private set; }

        public HassiumSql(string server, string database, string user, string password)
        {
            Value = new MySqlConnection("Server=" + server + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";Pooling=false");
            Attributes.Add("open", new InternalFunction(open));
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("query", new InternalFunction(query));
            Attributes.Add("select", new InternalFunction(select));
        }

        private HassiumObject open(HassiumObject[] args)
        {
            Value.Open();

            return null;
        }

        private HassiumObject query(HassiumObject[] args)
        {
            MySqlCommand cmd = new MySqlCommand(args[0].ToString(), Value);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            return null;
        }

        private HassiumObject select(HassiumObject[] args)
        {
            MySqlCommand cmd = new MySqlCommand(args[0].ToString(), Value);
            cmd.Prepare();

            return new HassiumSqlDataReader(cmd.ExecuteReader());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();

            return null;
        }
    }
}

