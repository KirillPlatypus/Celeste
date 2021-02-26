using Mono.Data.Sqlite;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System;


namespace DB
{
    
    public abstract class Command
    {
        private static string path = Path.GetFullPath("Coordinate.sqlite3");

        protected static ConnectDB connectDB = new ConnectDB(path);

        protected Command(Transform _player, string scenename)
        {

        }
    }

    public class CoordinateCommand : Command
    {
        private static Vector2 player;

        private static string sceneName;

        public CoordinateCommand(Transform _player, string scenename) : base(_player, scenename)
        {
            player = _player.position;
            sceneName = scenename;
        }

        public static async void UpdateCoordinate(object name)
        {
       
            await ReadCoordinate();
            
            string update = $"UPDATE coordinate SET CoordinateX = '{player.x}'," +
                        $" CoordinateY = '{player.y}'," +
                        $" Name = '{(string)name}' WHERE Id = {ModuleDB.sceneTable.Id};";


            using (var commandSQl = new SqliteCommand(update, connectDB.connection))
            {

                connectDB.OpenConnection();

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
                            //result.GetFloat(result.GetOrdinal("CoordinateX"));
                            //result.GetFloat(result.GetOrdinal("CoordinateY"));
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
        private static Vector2 player;

        private static string sceneName;

        public SceneCommand(Transform _player, string scenename) : base(_player, scenename)
        {
            sceneName = scenename;
        }

        public static async void UpdateScene(object activeScenebool)
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
