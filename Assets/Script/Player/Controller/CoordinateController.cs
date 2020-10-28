using UnityEditor;
using Mono.Data.Sqlite;
using DB;
using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace Controller
{
    public sealed class CoordinateController : PlayerElement
    {
        ConnectDB connectDB;
        internal CoordinateTable coordinateTable;
        [SerializeField] private Transform player;
        public CoordinateController()
        {
            connectDB = new ConnectDB(string.Format("D:\\Documents\\unity projects\\Celeste\\Assets\\Coordinate.sqlite3"));
        }

        public async Task<string> SaveCoordinate(string name)
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

        public void UpdateCoordinate(string name)
        {
             ReadCoordinate();

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
                        coordinateTable = new CoordinateTable()
                        {
                            CoordinateX = (decimal)result["CoordinateX"],
                            CoordinateY = (decimal)result["CoordinateY"],
                            Name = (string)result["Name"],
                            Id = (long)result["Id"]

                        };
                    }
                    return new Vector2((float)coordinateTable.CoordinateX, (float)coordinateTable.CoordinateY);
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