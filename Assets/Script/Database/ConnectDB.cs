using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

namespace DB
{
    class ConnectDB
    {
        public SqliteConnection connection;
        public ConnectDB(string path)
        {
            connection = new SqliteConnection($"Data Source={path}");
            if (!File.Exists($"{path}"))
            {
                SqliteConnection.CreateFile(path);
            }
            else
            {
               // Debug.Log("Connect seccuess");
            }
        }
        internal void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();

            }
        }
        internal void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();

            }
        }
    }
}
