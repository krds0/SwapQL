using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;


namespace Shared_Functions_Lib
{
    public static class SharedFunctions
    {
        public enum DatabaseKind
        {
            Mysql,
            Postgres
        }

        public static DbProviderFactory GetProvFact(DatabaseKind dbKind)
        {
            switch (dbKind)
            {
                case DatabaseKind.Mysql:
                    return MySqlClientFactory.Instance;
                case DatabaseKind.Postgres:
                    return NpgsqlFactory.Instance;
                default:
                    throw new InvalidOperationException($"Unsupported database kind {dbKind.ToString()}!");
            }
        }

        public static DbConnection createConnection(string conString)
        {
            MySqlConnection connection = new MySqlConnection(conString);
            return connection;
        }

        public static string ReadData(string query, DbConnection conn)
        {
            DbCommand command = conn.CreateCommand();
            command.CommandText = query;
            DbDataReader Reader;
            conn.Open();
            Reader = command.ExecuteReader();

            string dbResult = "";
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString() + ", ";
                dbResult += row;
            }
            conn.Close();
            return dbResult;

        }
    }
}
