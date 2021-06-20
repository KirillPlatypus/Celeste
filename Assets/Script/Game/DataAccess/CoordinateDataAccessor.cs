using DB;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game
{
    public class CoordinateDataAccessor : IDataAccessor, IDataThread
    {

        public Thread dataThread {get; set;}

        public override Command command {get; set;}

        private Vector2 Player;

        public CoordinateDataAccessor() : base() {}

        public CoordinateDataAccessor(Vector2 _player)
        {
            Player = _player;
        }
        public override async Task UpdateData(object varRowOfRequest)
        {
            command = new CoordinateCommand(Player, SceneManager.GetActiveScene().name);

            await CoordinateCommand.UpdateCoordinate((string)varRowOfRequest);
        }

        public override async void ReadData()
        {
            command = new CoordinateCommand(Player, SceneManager.GetActiveScene().name);

            await CoordinateCommand.ReadCoordinate();
        }
    }
}
