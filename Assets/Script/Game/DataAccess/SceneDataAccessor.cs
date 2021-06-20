using DB;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneDataAccessor : IDataAccessor, IDataThread
    {
        public Thread dataThread {get; set;}

        public override Command command {get; set;}

        Vector2 Player;
        static string Last, Active;

        public SceneDataAccessor(string LastScene, string ActiveScene)
        {
            Last = LastScene;
            Active = ActiveScene;
            
            //dataThread = new Thread(new ParameterizedThreadStart(SceneCommand.UpdateScene));
        }

        public SceneDataAccessor( ) : base() {}

        public override async Task UpdateData(object varRowOfRequest)
        {
            command = (long)varRowOfRequest == 1 
                    ? new SceneCommand(Player, Active)
                    : new SceneCommand(Player, Last);  
            
            await SceneCommand.UpdateScene((long)varRowOfRequest);
        }
        public override async void ReadData()
        {
            await SceneCommand.ReadScene();
        }

    }
}