using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Linq;

namespace SwapQLib
{
    public abstract class ISwapQL
    {
        public const string ConnectionString = "Server={0};Port={1};Database={2};Uid={3};Password={4}";

        public abstract DbProviderFactory database { get; }
        public DbConnection Connection { get; private set; }

        public DbConnection Connect(IPAddress host, int port, string databasename, string user, string password)
        {
            Connection = database.CreateConnection();
            Connection.ConnectionString = string.Format(ConnectionString, host, port, databasename, user, password);
            Connection.Open();

            return Connection;
        }

        public List<string> CreateInserts(string tblName)
        {
            //Läuft für jede Tabelle einmal
            //Datentypen und Spaltennamen einlesen
            //Für jede Zeile ein Insert
            //Datentypen: Für ''
            //Datareader Select * ansehen

            List<string> statements = new List<string>();

            // Die Spalten der angegeben Tabelle bekommen
            string[] restrictions = new string[] { null, null, tblName, null };
            DataTable dt = Connection.GetSchema("Columns", restrictions);
            List<DataColumn> cols = dt.Columns.AsGeneric().ToList();

            //Den Datentyp jeder Spalte auslesen
            List<string> colTypes = new List<string>();
            foreach (var item in cols)
            {
                string datatype = item.DataType.Name;
                colTypes.Add(datatype);
            }

            //Alle Zeilen der Quell-Tabelle lesen
            var comm = Connection.CreateCommand();
            comm.CommandText = $"Select * from {tblName}";
            DbDataReader reader = comm.ExecuteReader();

            //Für jede Zeile ein INSERT statement generieren
            string generalStatement = $"INSERT INTO {tblName} VALUES(";
            while(reader.Read())
            {
                string statement = generalStatement;

                //Alle Spalten der Zeile durchgehen
                for (int colIndex = 0; colIndex < cols.Count; colIndex++)
                {
                    //TODO: In Hilfsfunktion auslagern
                    if (colTypes[colIndex] == "int") //TODO: Nach anderen numerischen Datentypen checken
                    {
                        statement += $"{reader.GetString(colIndex)}";
                    }
                    else
                    {
                        statement += $"\"{reader.GetString(colIndex)}\"";
                    }
                    if (colIndex != colTypes.Count - 1) //Checken, ob letzte Spalte in Zeile
                    {
                        statement += ", ";
                    }
                }
                statement += ");";
                statements.Add(statement);
            }

            return statements;
        }
    }
}
