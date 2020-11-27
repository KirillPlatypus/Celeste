using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Controller
{
    public class CoordinateController : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private CoordinateCommand coordinate = new CoordinateCommand();

        public void CoordinateUpdate(string name)
        {
            coordinate.UpdateCoordinate(name, player);
            coordinate.ReadCoordinate();
        }

        public Vector3 CoordinateRead()
        {
            coordinate.ReadCoordinate();
            return new Vector3((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY, 0) ;
        }
    }
}
