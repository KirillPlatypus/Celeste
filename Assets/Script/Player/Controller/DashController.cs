using System;
using System.Collections;
using UnityEngine;


namespace Player.Controller
{
    public class DashController : PlayerElement
    {
        [SerializeField] private KeyCode keyDash;

        [SerializeField] private DashState dashState;

        [SerializeField] private Vector2 savedVelocity;
        [SerializeField] private Vector2 savedDiraction;

        public void SetDash()
        {
            if (aplication.playerModel.OnFloor)
            {
                aplication.playerModel.DashingTime = true;
            }
        }


        public IEnumerator Dash(float horizontalRaw, float verticalRaw)
        {
            switch (dashState)
            {
                case DashState.StartDashing:

                    if (Input.GetKeyDown(KeyCode.Z) && !aplication.playerModel.Dash)
                    {
                        if (!aplication.playerModel.Dash)
                        {
                            savedVelocity.x = aplication._Body.velocity.x;
                            savedDiraction = new Vector2(horizontalRaw, verticalRaw);
                            aplication._Body.velocity = Vector2.zero;
                        }



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
                    }
                    break;

                case DashState.EndDashing:

                    aplication._Body.velocity = savedVelocity;

                    dashState = DashState.StartDashing;

                    aplication.playerModel.Dash = false;

                    aplication._Body.gravityScale = 1;

                    aplication.playerModel.DashingTime = false;
                    break;

            }
        }
    }

    public enum DashState
    {
        StartDashing,
        EndDashing,
    }
}