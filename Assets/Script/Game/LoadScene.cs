using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using PlayerObject.Coin;
using Player;

namespace Game
{
    public class LoadScene  : MonoBehaviour
    {
        PlayerAplication player { get { return FindObjectOfType<PlayerAplication>();}}

        [SerializeField] GameObject DontDestroyPlayer;
        [SerializeField] GameObject DontDestroyCamera;

        static int i = 0;
        private void Awake() {
            StartGame();
        }

        public void StartGame()
        {              
            StartCoroutine(SetScene(new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name)));
            SetCoordinate(new CoordinateDataAccessor(player.transform));

            var playerArray = GameObject.FindGameObjectsWithTag("Player");
            var CinemahineArray = GameObject.FindGameObjectsWithTag("Camera");

            DestroyOnLoad(playerArray, DontDestroyPlayer);
            DestroyOnLoad(CinemahineArray, DontDestroyCamera);
        }

        private void DestroyOnLoad(GameObject[] array, GameObject destroyOrNot)
        {
            if(array.Length > 1)
                Destroy(destroyOrNot);

            DontDestroyOnLoad(destroyOrNot);

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
            //switch active scene on SQLite DB

            yield return new WaitForSeconds(0.1f);

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

            player.transform.position = new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);        

        }
    }
}
