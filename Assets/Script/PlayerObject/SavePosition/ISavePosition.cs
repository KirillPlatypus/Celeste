using UnityEngine;
using Game;
using Player;

namespace PlayerObject
{
    public abstract class ISavePosition : MonoBehaviour
    {
        protected PlayerAplication playerAplication { get { return FindObjectOfType<PlayerAplication>(); } }
        public abstract bool OnPoint {get; set;}

        protected LayerMask mask { get { return LayerMask.GetMask("Player"); } }

        protected void SaveOnPoint(bool OnPoint, string nameObject, string lastNameObject, Vector2 savingPosition)
        {
           if (OnPoint && nameObject != lastNameObject)
            {
                var saveCoordinate = new CoordinateDataAccessor(savingPosition);
                saveCoordinate.UpdateData((string)nameObject);
            }
        }
    }
}