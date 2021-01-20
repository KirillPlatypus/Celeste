using DB;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Game
{
    public class CoordinateDataAccessor : IDataAccessor, IDataThread
    {

        public Thread dataThread {get; set;}

        public override Command command {get; set;}

        private Transform Player;

        public CoordinateDataAccessor() : base() {}

        public CoordinateDataAccessor(Transform _player) : base(_player)
        {
            Player = _player;

            dataThread = new Thread(new ParameterizedThreadStart(CoordinateCommand.UpdateCoordinate));
        }
        public override void UpdateData(object varRowOfRequest)
        {
            command = new CoordinateCommand(Player, SceneManager.GetActiveScene().name);

            if (dataThread.ThreadState != ThreadState.Running)
            {
                dataThread = new Thread(new ParameterizedThreadStart(CoordinateCommand.UpdateCoordinate));

                dataThread.Start(varRowOfRequest);
            }
        }

        public override void ReadData()
        {
            command = new CoordinateCommand(Player, SceneManager.GetActiveScene().name);

            CoordinateCommand.ReadCoordinate();
        }
    }
}
