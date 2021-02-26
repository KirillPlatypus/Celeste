using System;
using Game;
using Player.CheckAnymore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerObject
{
    public class CheckScene : MonoBehaviour
    {
        [SerializeField] private LoadScene load;
        [SerializeField] private string NextSceneName;

        [SerializeField] private bool OnCheckScene;

        [SerializeField] private LayerMask mask;

        private int i;
        
        private void Start() 
        {
            i = 0;
        } 
        void Update()
        {
            OnCheckScene = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);

            if (OnCheckScene && i == 0)
            {
                i = 1;
                StartCoroutine(load.Loading(NextSceneName));
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}
