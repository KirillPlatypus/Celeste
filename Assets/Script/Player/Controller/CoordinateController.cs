using DB;
using UnityEngine;
using System.Threading;

namespace Player.Controller
{
    public class CoordinateController : PlayerElement
    {

        private Thread dataThread;

        CoordinateCommand command;

        void Start()
        {
            dataThread = new Thread(new ParameterizedThreadStart(CoordinateCommand.UpdateCoordinate));
        }
        public void CoordinateUpdate(object name)
        {
            command = new CoordinateCommand(aplication.transform);

            if (dataThread.ThreadState != ThreadState.Running)
            {
                dataThread = new Thread(new ParameterizedThreadStart(CoordinateCommand.UpdateCoordinate));

                dataThread.Start(name);
            }
        }

        public Vector3 CoordinateRead()
        {
            CoordinateCommand.ReadCoordinate();
            return new Vector3((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY, 0) ;
        }
    }
}
