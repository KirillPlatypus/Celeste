using System;
using UnityEngine;

namespace Player.Controller
{
    public class DeathController : PlayerElement
    {
        [SerializeField] private DeathState deathStation;
        private float time;
        public DeathController()
        {

        }
        public void Death()
        {
            time += Time.deltaTime;
            
            switch (deathStation)
            {
                case DeathState.StartDeath:
                    if (time < 0.5f)
                    {
                        aplication.collider.enabled = false;

                        transform.Translate(new Vector3(0.07f, 0.05f, 0));

                        aplication._Body.bodyType = RigidbodyType2D.Static;
                    }
                    else
                    {
                        time = 0;
                        deathStation = DeathState.AnimationState;
                    }
                    break;

                case DeathState.AnimationState:
                    if (time < 0.3f)
                    { 
                            
                    }
                    else
                    {
                        time = 0;
                        deathStation = DeathState.LoadPlayer;
                    }
                    break;

                case DeathState.LoadPlayer:

                    aplication.playerModel.Death = false;

                    transform.position = aplication.saveCoordinate.CoordinateRead();

                    aplication.collider.enabled = true;

                    aplication._Body.bodyType = RigidbodyType2D.Dynamic;
                    deathStation = DeathState.StartDeath;

                    break;

            }
        }

        private enum DeathState
        {
            StartDeath,
            AnimationState,
            LoadPlayer,
        }

    }
}
