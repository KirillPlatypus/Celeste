using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


namespace Game
{
    public class LoadScene  : MonoBehaviour
    {
        [SerializeField] Transform Player;

        private CoordinateDataAccessor coordinatedata ;
        private SceneDataAccessor sceneData;

        static int i = 0;

        private void Start()
        {   
            sceneData = new SceneDataAccessor();

            if(i == 0)
            {
                //load last scene (only when the game is starting ) 
                sceneData.ReadData();

                if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
                {                
                    SceneManager.LoadSceneAsync((int)ModuleDB.sceneTable.buildIndex);
                }
                i = 1;
            }

            //switch active scene on SQLite DB
            var secondStartTask = Task.Run(() => sceneData.ReadData());
            
            if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
            {
                sceneData = new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name);
                
                var thirdStartTask = secondStartTask.ContinueWith(task => sceneData.UpdateData((long)0));
                var fourthStartTask = thirdStartTask.ContinueWith(task => sceneData.UpdateData((long)1));
            }

            //get and set last checkpoint`s coordinate 
            coordinatedata = new CoordinateDataAccessor(Player);

            coordinatedata.ReadData();

            Player.position = new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);        
        }
        

        public IEnumerator Loading(string sceneName)
        {
            yield return new WaitForSeconds(1f);
            
            SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/MainScene/{sceneName}.unity"));
        }
    }
}
