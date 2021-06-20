using Mono.Data.Sqlite;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System;


namespace DB
{
    
    public abstract class Command
    {

        protected Command(Vector2 _player, string scenename)
        {

        }
    }

    public class CoordinateCommand : Command
    {

        private static string path = Path.GetFullPath("Coordinate.sqlite3");
        private static Vector2 player;
                protected static ConnectDB connectDB = new ConnectDB(path);


        private static string sceneName;

        public CoordinateCommand(Vector2 _player, string scenename) : base(_player, scenename)
        {
            player = _player;
            sceneName = scenename;
        }

        public static async Task UpdateCoordinate(object name)
        {
            await ReadCoordinate();
            
                var update = $"UPDATE coordinate SET CoordinateX = @CoordinateX," +
                             $" CoordinateY = @CoordinateY," +
                             $" Name = @Name WHERE Id = {ModuleDB.coordinateTable.Id};";

            using (var commandSQl = new SqliteCommand(update, connectDB.connection))
            {
                connectDB.OpenConnection();

                commandSQl.Parameters.AddWithValue("@CoordinateX", player.x);
                commandSQl.Parameters.AddWithValue("@CoordinateY", player.y);
                commandSQl.Parameters.AddWithValue("@Name", (string)name);

                var result = await commandSQl.ExecuteNonQueryAsync();

                await ReadCoordinate();

                connectDB.CloseConnection();
            }
        }

        public static async Task<Vector2> ReadCoordinate()
        {
            string add = "SELECT coo. *, sc.SceneName FROM coordinate coo LEFT JOIN Scene sc ON sc.Id = coo.IdScene";

            using (var commandSQl = new SqliteCommand(add, connectDB.connection))
            {

                connectDB.OpenConnection();

                var result = await commandSQl.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        if(sceneName == (string)result["SceneName"])
                        {
                            ModuleDB.coordinateTable.Id = (long)result["Id"];
                            ModuleDB.coordinateTable.Name = (string)result["Name"];
                            ModuleDB.coordinateTable.CoordinateX = (double)result["CoordinateX"];
                            ModuleDB.coordinateTable.CoordinateY = (double)result["CoordinateY"];

                            ModuleDB.sceneTable.Id = (long)result["Id"];
                            ModuleDB.sceneTable.SceneName = (string)result["SceneName"];


                            return new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);
                        }

                    }
                }

                connectDB.CloseConnection();
            }
            return new Vector2(0, 0);
        }
    }


    public class SceneCommand : Command
     {
                 private static string path = Path.GetFullPath("Coordinate.sqlite3");

        private static Vector2 player;

        protected static ConnectDB connectDB = new ConnectDB(path);

        private static string sceneName;

        public SceneCommand(Vector2 _player, string scenename) : base(_player, scenename)
        {
            sceneName = scenename;
        }

        public static async Task UpdateScene(object activeScenebool)
        {
            await ReadScene();
            Debug.Log(activeScenebool);
            string update = $"UPDATE Scene SET ActiveScene = '{activeScenebool}' WHERE SceneName = '{sceneName}';";


            using (var commandSQl = new SqliteCommand(update, connectDB.connection))
            {

                connectDB.OpenConnection();

                var result = await commandSQl.ExecuteNonQueryAsync();


                connectDB.CloseConnection();

                await ReadScene();
            }
        }
         public static async Task <string> ReadScene()
        {
            string add = "SELECT * FROM Scene WHERE ActiveScene == 1";

            using (var commandSQl = new SqliteCommand(add, connectDB.connection))
            {

                connectDB.OpenConnection();

                var result = await commandSQl.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        ModuleDB.sceneTable.Id = (long)result["Id"];
                        ModuleDB.sceneTable.SceneName = (string)result["SceneName"];
                        ModuleDB.sceneTable.ActiveScene = (long)result["ActiveScene"];   
                        ModuleDB.sceneTable.buildIndex = (long)result["buildIndex"];
                    }    
                }

                connectDB.CloseConnection();

                return ModuleDB.sceneTable.SceneName;
            }
            return "";
        }
    }
}
