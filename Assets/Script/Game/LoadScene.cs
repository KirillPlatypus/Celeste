using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;
using PlayerObject.Coin;
using Player;
using System.IO;
using DB;

namespace Game
{
    public class LoadScene  : MonoBehaviour
    {
        PlayerAplication player { get { return FindObjectOfType<PlayerAplication>();}}

        [SerializeField] GameObject[] DontDestroyObjects;
        List<GameObject[]> ObjectArray = new List<GameObject[]>();

        static bool startingLoad = true;
        static bool startingMenu = true;

        private void Awake() 
        {
            ExecutionContext.IsFlowSuppressed();
            if(SceneManager.GetActiveScene().name != "Menu")
            {
                if(startingMenu)
                {
                    var strMenu = "Menu";

                    Loading(strMenu);
                    startingMenu = false;
                }
                else
                {
                    SceneCommand.ReadScene();
                    LoadLastScene(new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name));
                    
                    SetLastScene(new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name));
                    
                    SetDontDestroyObject();

                    if(player != null)
                        SetCoordinate(new CoordinateDataAccessor(player.transform.position));

                }
            }
            else
            {
                startingMenu = false;
            }
        }
        private void SetDontDestroyObject()
        {
            if(GameObject.FindGameObjectsWithTag("Player") != null)
            {
                ObjectArray.Add(GameObject.FindGameObjectsWithTag("Player"));
                ObjectArray.Add(GameObject.FindGameObjectsWithTag("Camera"));
                ObjectArray.Add(GameObject.FindGameObjectsWithTag("Canvas"));
            }

            for (int i = 0; i < DontDestroyObjects.Length; i++)
            {
                DestroyOnLoad(ObjectArray[i], DontDestroyObjects[i]);
            }
        }

        private void DestroyOnLoad(GameObject[] array, GameObject destroyOrNot)
        {
            if(array.Length != 0)
            {
                if(array.Length > 1)
                    DestroyImmediate(destroyOrNot);

                DontDestroyOnLoad(destroyOrNot);
            }
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

        public void Loading(string sceneName)
        {
            SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/MainScene/{sceneName}.unity"));
        }
        public void ButtonStartGame()
        {
            LoadLastScene(new SceneDataAccessor(ModuleDB.sceneTable.SceneName, SceneManager.GetActiveScene().name));
        }

        private void LoadLastScene(IDataAccessor data)
        {
            if(startingLoad)
            {
                //load last scene (only when the game is starting ) 
                data.ReadData();

                if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
                {                
                    SceneManager.LoadSceneAsync((int)ModuleDB.sceneTable.buildIndex);
                }
                startingLoad = false;
            }
            if(SceneManager.GetActiveScene().name == "Menu")
                startingLoad = true;
        }

        private void SetLastScene(IDataAccessor data)
        {
            //switch active scene on SQLite DB
            data.ReadData();
            
            if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name && 
                SceneManager.GetActiveScene().name != "Menu")
            {                
                var thirdStartTask = Task.Run(() => data.UpdateData((long)0));
                var fourthStartTask = thirdStartTask.ContinueWith( (g) => data.UpdateData((long)1));
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
