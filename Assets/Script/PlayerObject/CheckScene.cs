using System;
using Game;
using Player.CheckAnymore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerObject
{
    public class CheckScene : ICheckAnymore
    {
        [SerializeField] private LoadScene load;

        [SerializeField] private bool OnCheckScene;

        void Update()
        {
            OnCheckScene = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);

            if (OnCheckScene )
            {
                load.LoadNextScene();
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}
