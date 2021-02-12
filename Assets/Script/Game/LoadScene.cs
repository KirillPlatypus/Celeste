using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using PlayerObject.Coin;

namespace Game
{
    public class LoadScene  : MonoBehaviour
    {
        [SerializeField] Transform Player;

        static int i = 0;

        private void Awake()
        {   
            StartCoroutine(SetScene(new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name)));
            SetCoordinate(new CoordinateDataAccessor(Player));
        }

        [SerializeField] private ActiveCoins activeCoin;

        private void Start() 
        {
            foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
            {
                foreach (var iKey in activeCoin.GetListForSpecificScene(SceneManager.GetActiveScene().name))
                {
                    if(coin.name == iKey)
                        Destroy(coin);
                }
            }
        }

        public IEnumerator Loading(string sceneName)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/MainScene/{sceneName}.unity"));
        }

        private IEnumerator SetScene(IDataAccessor data)
        {
            if(i == 0)
            {
                //load last scene (only when the game is starting ) 
                data.ReadData();

                if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
                {                
                    SceneManager.LoadSceneAsync((int)ModuleDB.sceneTable.buildIndex);
                }
                i = 1;
            }
            yield return new WaitForSeconds(0.1f);

            //switch active scene on SQLite DB
            
            data.ReadData();
            
            if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
            {                
                var thirdStartTask = Task.Run(() => data.UpdateData((long)0));
                var fourthStartTask = thirdStartTask.ContinueWith(task => data.UpdateData((long)1));
            }
        }

        private void SetCoordinate(IDataAccessor data)
        {
            //get and set last checkpoint`s coordinate 

            data.ReadData();

            Player.position = new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);        

        }
    }
}
