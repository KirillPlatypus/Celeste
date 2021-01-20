using DB;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneDataAccessor : IDataAccessor, IDataThread
    {
        public Thread dataThread {get; set;}

        public override Command command {get; set;}

        [SerializeField] Transform Player;
        static string Last, Active;

        public SceneDataAccessor(string LastScene, string ActiveScene)
        {
            Last = LastScene;
            Active = ActiveScene;
            
            //dataThread = new Thread(new ParameterizedThreadStart(SceneCommand.UpdateScene));
        }

        public SceneDataAccessor( ) : base() {}

        public SceneDataAccessor(Transform _player) : base(_player) {}

        public override void UpdateData(object varRowOfRequest)
        {
            command = (long)varRowOfRequest == 1 
                    ? new SceneCommand(Player, Active)
                    : new SceneCommand(Player, Last);  
            
            SceneCommand.UpdateScene((long)varRowOfRequest);
        }
        public override void ReadData()
        {
            SceneCommand.ReadScene();
        }

    }
}