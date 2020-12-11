using System;
using UnityEngine;


namespace Controller
{
    public class DashController : PlayerElement
    {
        [SerializeField] private KeyCode keyDash;

        [SerializeField] private DashState dashState;

        [SerializeField] private Vector2 savedVelocity;
        [SerializeField] private Vector2 savedDiraction;

        [SerializeField] private float time = 0;
        [SerializeField] private float endDashTime;


        public void SetDash()
        {
            if (aplication.playerModel.OnFloor && time == 0)
            {
                aplication.playerModel.DashingTime = true;
            }
        }


        public void Dash(float horizontalRaw, float verticalRaw)
        {
            switch (dashState)
            {
                case DashState.StartDashing:

                    if (Input.GetKeyDown(keyDash) || (time < endDashTime && time != 0))
                    {
                        if (time <= 0.0001f)
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
                        time += Time.fixedDeltaTime * 0.1f;
                    }
                    else if (time >= endDashTime)
                    {
                        dashState = DashState.EndDashing;
                    }
                    break;

                case DashState.EndDashing:
                    time = 0;

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