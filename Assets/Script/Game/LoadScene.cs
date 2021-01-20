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

        private IDataAccessor data ;

        static int i = 0;
        private void Start()
        {
            if(i == 0)
            {
                data = new SceneDataAccessor();

                data.ReadData();

                if(ModuleDB.sceneTable.SceneName != SceneManager.GetActiveScene().name)
                {
                    SceneManager.LoadScene((int)ModuleDB.sceneTable.buildIndex);
                }

                data = new CoordinateDataAccessor(Player);

                data.ReadData();

                Player.position = new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);
                i = 1;
            }
        }
        
        public IEnumerator Loading(string sceneName)
        {
            yield return new WaitForSeconds(1f);
            
            SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/MainScene/{sceneName}.unity"));
        }
    }
}
