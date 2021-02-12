using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace PlayerObject.Coin
{


    public class Coin : MonoBehaviour   
    {
        [SerializeField] private ActiveCoins activeCoin;

        [SerializeField] private CoinCounter counter;
        [SerializeField] private LayerMask mask;
        [SerializeField] float size;
        [SerializeField] private ParticleSystem strawbarreParticle;
        [SerializeField] private Animator animationCoin;

        private bool isTaked; 
        private int i;

        private event EventHandler CoinTook;

        private void Start() 
        {        
            i = 0;
            CoinTook += counter.AddCoin;
            strawbarreParticle.Stop();
        }

        void Update()
        {
            if (i == 0)
            {
                isTaked = Physics2D.OverlapCircle(transform.position, size, mask);
            }
            if(isTaked)
            {
                i = 1;
                isTaked = false;

                CoinTook(this, new EventArgs());
                StartCoroutine(DestroyCoin());
            }
        }

        private IEnumerator DestroyCoin()
        {

            animationCoin.SetBool("IsItTake", true);

            yield return new WaitForSeconds(1 - animationCoin.GetCurrentAnimatorStateInfo((int)ModuleDB.sceneTable.buildIndex).length);

            strawbarreParticle.Play();

            yield return new WaitForSeconds(3f);
        
            if(strawbarreParticle.isStopped)
            {
                activeCoin.GetListForSpecificScene(SceneManager.GetActiveScene().name).Add(gameObject.name);

                Destroy(gameObject);
            }
        }
    
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, size);
        }
    }
}