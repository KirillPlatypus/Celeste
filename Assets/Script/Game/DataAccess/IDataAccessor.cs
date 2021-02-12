using DB;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Game
{
    public abstract class IDataAccessor
    {
        public abstract Command command {get; set;}

        public IDataAccessor() {}

        public abstract void UpdateData(object varRowOfRequest);

        public abstract void ReadData();
    }

    public interface IDataThread 
    {
        Thread dataThread {get; set;}
    }
}