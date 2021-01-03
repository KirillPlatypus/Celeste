using Mono.Data.Sqlite;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System;


namespace DB
{
    internal static class ModuleDB
    {
        public static CoordinateTable coordinateTable = new CoordinateTable();
    }


    public class CoordinateCommand
    {
        private static string path = Path.GetFullPath("Coordinate.sqlite3");

        private static ConnectDB connectDB = new ConnectDB(path);

        private static Vector2 player;

        public CoordinateCommand(Transform _player)
        {
            player = _player.position;
        }


        public async Task<string> InsertCoordinate(string name)
        {
            string add = "INSERT INTO coordinate ('CoordinateX', 'CoordinateY', 'Name', 'Id') VALUES (@CoordinateX, @CoordinateY, @Name, @Id)";

            using (var commandSQl = new SqliteCommand(add, connectDB.connection))
            {
                    
                await Task.Run(() => ReadCoordinate());
                connectDB.OpenConnection();

                commandSQl.Parameters.AddWithValue("@CoordinateX", player.normalized.x);
                commandSQl.Parameters.AddWithValue("@CoordinateY", player.normalized.y);
                commandSQl.Parameters.AddWithValue("@Name", name);
                commandSQl.Parameters.AddWithValue("@Id", 1);


                var result = commandSQl.ExecuteNonQuery();

                connectDB.CloseConnection();
            }
            return "Add";
        }

        public static void UpdateCoordinate(object name)
        {
       
        string update = $"UPDATE coordinate SET CoordinateX = '{player.x}'," +
                        $" CoordinateY = '{player.y}'," +
                        $" Name = '{(string)name}' WHERE Id = 1;";


            using (var commandSQl = new SqliteCommand(update, connectDB.connection))
            {

                connectDB.OpenConnection();

                commandSQl.Parameters.AddWithValue("@CoordinateX", player.x);
                commandSQl.Parameters.AddWithValue("@CoordinateY", player.y);
                commandSQl.Parameters.AddWithValue("@Name", (string)name);
                commandSQl.Parameters.AddWithValue("@Id", 1);


                var result = commandSQl.ExecuteNonQuery();

                ReadCoordinate();

                connectDB.CloseConnection();

            }
        }

        public static Vector2 ReadCoordinate()
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

                        //result.GetFloat(result.GetOrdinal("CoordinateX"));
                        //result.GetFloat(result.GetOrdinal("CoordinateY"));
                        ModuleDB.coordinateTable.CoordinateX = (float)result["CoordinateX"];
                        ModuleDB.coordinateTable.CoordinateY = (float)result["CoordinateY"];
                        ModuleDB.coordinateTable.Name = (string)result["Name"];
                        ModuleDB.coordinateTable.Id = (long)result["Id"];

                        Debug.Log(result["CoordinateX"]);
                        Debug.Log(result["CoordinateY"]);
                        Debug.Log(result.GetString(result.GetOrdinal("Name")));
                        Debug.Log(result["Id"]);
                    }
                    return new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);
                }

                connectDB.CloseConnection();
            }
            return new Vector2(0, 0);
        }

        public async Task DeleteCoordinate()
        {
            string del = "DELETE FROM coordinate WHERE Id = 1";

            await Task.Run(ReadCoordinate);

            connectDB.OpenConnection();

            var commandSQl = new SqliteCommand(del, connectDB.connection);

            var result = commandSQl.ExecuteNonQuery();

            connectDB.CloseConnection();
        }
    }
}
