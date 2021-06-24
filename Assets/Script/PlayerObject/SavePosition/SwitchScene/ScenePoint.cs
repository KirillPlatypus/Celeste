using UnityEngine;
using Game;
using System.Threading.Tasks;
using PlayerObject;

namespace PlayerObject.SavePosition.ScenePoint
{
    public class ScenePoint : IScenePoint, IPoint
    {
        [SerializeField] private LoadScene scene;
        [SerializeField] private string sceneName;
        [SerializeField] Vector2 size;

        public bool OnPoint{get; set;}

        void Update()
        {
            OnPoint = Physics2D.OverlapBox(transform.position, size, 1f, mask);

            if(OnPoint)
            {
                Switch(scene, sceneName);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(transform.position, size);
        }

    }
}