using System;
using System.Collections;
using UnityEngine;


namespace Player.Controller
{
    public class DashController : PlayerElement
    {
        [SerializeField] private KeyCode keyDash;

        [SerializeField] public DashState dashState;

        [SerializeField] private Vector2 savedVelocity;
        [SerializeField] private Vector2 savedDiraction;

        public void SetDash()
        {
            if (aplication.playerModel.OnFloor)
            {
                aplication.playerModel.DashingTime = true;
            }
        }


        public IEnumerator Dash(Vector2 diraction)
        {

            if (!aplication.playerModel.Dash)
            {
                dashState = DashState.ActionDash;

                    savedVelocity.x = aplication._Body.velocity.x;
                    savedDiraction = diraction;
                    aplication._Body.velocity = Vector2.zero;




                aplication.playerModel.Dash = true;
                aplication._Body.gravityScale = 0;

                if (savedDiraction != Vector2.zero)
                {

                    aplication._Body.velocity = savedDiraction.normalized *
                      new Vector2((float)aplication.playerModel.dashSpeed, (float)aplication.playerModel.dashSpeed);

                }
                else
                {

                    aplication._Body.velocity = Vector2.right * new Vector2((float)aplication.playerModel.dashSpeed, 0);

                }

                yield return new WaitForSeconds(0.3f);

                dashState = DashState.EndDashing;
            
            
                aplication._Body.velocity = savedVelocity;
    
                aplication.playerModel.Dash = false;
    
                aplication._Body.gravityScale = 1;
    
                aplication.playerModel.DashingTime = false;
    
                dashState = DashState.StartDashing;

            }
        }
    }

    public enum DashState
    {
        StartDashing,
        ActionDash,
        EndDashing,
    }
}