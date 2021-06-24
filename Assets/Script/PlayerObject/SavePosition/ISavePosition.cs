using UnityEngine;
using Game;
using Player;

namespace PlayerObject
{
    public abstract class ICheckPoint : MonoBehaviour
    {
        protected PlayerAplication playerAplication { get { return FindObjectOfType<PlayerAplication>(); } }
        

        protected LayerMask mask { get { return LayerMask.GetMask("Player"); } }

        protected virtual void SaveOnPoint(bool OnPoint, string nameObject, string lastNameObject, Vector2 savingPosition)
        {
            if (OnPoint && nameObject != lastNameObject)
            {
                var saveCoordinate = new CoordinateDataAccessor(savingPosition);
                saveCoordinate.UpdateData((string)nameObject);
            }
        }
    }
    public abstract class IScenePoint : MonoBehaviour
    {
        protected PlayerAplication playerAplication { get { return FindObjectOfType<PlayerAplication>(); } }
        

        protected LayerMask mask { get { return LayerMask.GetMask("Player"); } }

        protected virtual void Switch(LoadScene scene, string sceneName)
        {
            scene.Loading(sceneName);
        }
        
    }
    public interface IPoint
    {
        bool OnPoint {get; set;}
    }
}