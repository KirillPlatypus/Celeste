using Controller;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public sealed class EffectsPlayerController : PlayerElement
    {
        #region Dash
        [SerializeField] private ParticleSystem particleDash;

        #endregion Dash

        #region SlideWall
        [SerializeField] private ParticleSystem slideWall;
        #endregion

        [SerializeField] private float time;
        private void Update()
        {
            time = Time.deltaTime;
            DashEffect();
            SlideEffect();
        }
        public void DashEffect()
        {
            if (aplication.playerModel.Dash)
            {
                if (time >= 0.02f)
                {
                    Instantiate(aplication.illusion, transform.position, Quaternion.identity);
                    time = 0;
                }
                particleDash.Play();
            }
            else
            {                
                particleDash.Stop();
            }

        }

        public void SlideEffect()
        {
            if (aplication.playerModel.SlideWall)
            {
                slideWall.Play();

            }
            else
            {

                slideWall.Stop();
            }
        }
    }
}