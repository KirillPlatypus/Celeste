using UnityEditor;
using Mono.Data.Sqlite;
using DB;
using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.IO;


namespace DB
{
    internal static class ModuleDB
    {
        public static CoordinateTable coordinateTable = new CoordinateTable();
    }

    internal class CoordinateCommand
    {
        private static string path = Path.GetFullPath("Coordinate.sqlite3");

        private static ConnectDB connectDB = new ConnectDB(path);


        public async Task<string> InsertCoordinate(string name, Transform player)
        {
            string add = "INSERT INTO coordinate ('CoordinateX', 'CoordinateY', 'Name', 'Id') VALUES (@CoordinateX, @CoordinateY, @Name, @Id)";

            using (var commandSQl = new SqliteCommand(add, connectDB.connection))
            {
                    
                await Task.Run(() => ReadCoordinate());
                connectDB.OpenConnection();

                commandSQl.Parameters.AddWithValue("@CoordinateX", player.position.x);
                commandSQl.Parameters.AddWithValue("@CoordinateY", player.position.y);
                commandSQl.Parameters.AddWithValue("@Name", name);
                commandSQl.Parameters.AddWithValue("@Id", 1);


                var result = commandSQl.ExecuteNonQuery();

                connectDB.CloseConnection();
            }
            return "Add";
        }

        public void UpdateCoordinate(string name, Transform player)
        {

            string update = $"UPDATE coordinate SET CoordinateX = '{player.position.x}'," +
                        $" CoordinateY = '{player.position.y}'," +
                        $" Name = '{name}' WHERE Id = 1;";


            using (var commandSQl = new SqliteCommand(update, connectDB.connection))
            {

                connectDB.OpenConnection();

                commandSQl.Parameters.AddWithValue("@CoordinateX", player.position.x);
                commandSQl.Parameters.AddWithValue("@CoordinateY", player.position.y);
                commandSQl.Parameters.AddWithValue("@Name", name);
                commandSQl.Parameters.AddWithValue("@Id", 1);


                var result = commandSQl.ExecuteNonQuery();

                connectDB.CloseConnection();
            }
        }

        public Vector2 ReadCoordinate()
        {
            string add = "SELECT * FROM coordinate";

            using (var commandSQl = new SqliteCommand(add, connectDB.connection))
            {

                connectDB.OpenConnection();

                var result = commandSQl.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {

                        ModuleDB.coordinateTable.CoordinateX = (decimal)result["CoordinateX"];
                        ModuleDB.coordinateTable.CoordinateY = (decimal)result["CoordinateY"];
                        ModuleDB.coordinateTable.Name = (string)result["Name"];
                        ModuleDB.coordinateTable.Id = (long)result["Id"];

                    }
                    return new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);
                }

                connectDB.CloseConnection();
            }
            return new Vector2(0, 0);
        }

        public async Task<string> DeleteCoordinate()
        {
            string del = "DELETE FROM coordinate WHERE Id = 1";

            await Task.Run(() => ReadCoordinate());

            connectDB.OpenConnection();

            var commandSQl = new SqliteCommand(del, connectDB.connection);

            var result = commandSQl.ExecuteNonQuery();

            connectDB.CloseConnection();
            return "Deleted";
        }
    }
}
