using System;
using Player.Controller.Move;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player.Controller;
using UI.Button;

namespace Player.View
{
    public class PlayerView : PlayerElement
    {
        public event Action<bool> OnDashingStateChange;

        public float distance;

        private RaycastHit2D hit;

        [SerializeField] private Joystick joystick;

        private void Update()
        {
            aplication.dash.SetDash();

            aplication.playerModel.horizontal = joystick.Horizontal;
            aplication.playerModel.vertical = joystick.Vertical;

            aplication.playerModel.horizontalRaw = joystick.Horizontal;
            aplication.playerModel.verticalRaw = joystick.Vertical;

            hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), distance);

            #region Movement

            Idle(new Vector2(aplication.playerModel.horizontal, aplication.playerModel.vertical));

            #endregion Movement


            #region Jump

            Jump();

            #endregion Jump


            #region Dash

            Dash();

            #endregion Dash


            #region Wall

            IdleWall();

            WallJuming();

            WallHangJump();

            #endregion Wall

            Death();

            #region Diraction

            if (!aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && !aplication.playerModel.JumpHangWall && !aplication.playerModel.JumpWall)
            {
                aplication.direction.diractionPlayer(aplication._Body.velocity.x, transform.localScale);

            }

            #endregion Diraction

        }


        private void Idle(Vector2 InputMove)
        {
            if (!aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && !aplication.playerModel.JumpWall && !aplication.playerModel.Dash)
            {
                aplication.movement.Idle(new MovementController(), InputMove);

                aplication.playerModel.Idle = aplication._Body.velocity.x != 0 ? true : false;

            }
        }


        public void Jump()
        {
            if (!aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && !aplication.playerModel.JumpWall && !aplication.playerModel.Dash)
            {
                aplication.jump.Jump(new Vector2(aplication._Body.velocity.x, aplication.playerModel.standartJumpPower), 0f, HoldButton.GetButtonStatusDown(ButtonCode.JumpButton));
                aplication.jump.SetGravity(HoldButton.GetButtonStatus(ButtonCode.JumpButton));

                aplication.playerModel.Jump = aplication._Body.velocity.y  != 0 && !aplication.playerModel.OnFloor ? true : false;

            }

        }


        private void Dash()
        {

            if (aplication.playerModel.DashingTime)
            {
                if(HoldButton.GetButtonStatus(ButtonCode.DashButton))
                    StartCoroutine(aplication.dash.Dash(new Vector2(aplication.playerModel.horizontalRaw, aplication.playerModel.verticalRaw)));
                
            }
            if (OnDashingStateChange != null)
                OnDashingStateChange(aplication.dash.dashState != DashState.StartDashing);
            

        }


        private void IdleWall()
        {
            if (aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && !aplication.playerModel.Dash)
            {
                aplication.movement.Idle(new WallMovementController(), new Vector2(aplication.playerModel.horizontal, aplication.playerModel.verticalRaw));
                aplication.playerModel.IdleWall = true;
            }
            else
            {
                aplication.playerModel.IdleWall = false;
            }
        }


        private double powerWallJump;

        private void WallHangJump()
        {
            if (HoldButton.GetButtonStatusDown(ButtonCode.JumpButton) && aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && !aplication.playerModel.Dash)
            {

                aplication.playerModel.JumpHangWall = true;

                Invoke(nameof(StopWallHangJump), aplication.playerModel.timeWallHangJump);

            }
            if (aplication.playerModel.JumpHangWall)
            {

                aplication.jumpWall.Jump(new Vector2((float)aplication.playerModel.powerWallHangJump * 1.8f, (float)Math.Abs(aplication.playerModel.powerWallHangJump)),
                                             aplication.playerModel.horizontalRaw, false);
            }

        }
        private void StopWallHangJump()
        {
            aplication.playerModel.JumpHangWall = false;
        }

        private void WallJuming()
        {
            if (!aplication.playerModel.OnFloor && !aplication.wall.DragOnWall(OnceButton.GetButtonStatus()) && aplication.playerModel.OnRightWall && !aplication.playerModel.Dash)
            {
                aplication.slide.PlayerSlide(aplication.playerModel.horizontal);
                if (HoldButton.GetButtonStatusDown(ButtonCode.JumpButton))
                {

                    powerWallJump = aplication.playerModel.SlideWall ? Math.Sqrt(aplication.playerModel.powerWallJump) : aplication.playerModel.powerWallJump;

                    aplication.playerModel.JumpWall = true;

                    Invoke(nameof(StopWallJump), aplication.playerModel.timeWallJump);

                    aplication.direction.diractionPlayer(-transform.localScale.x, transform.localScale);
                }
            }
            else
            {
                aplication.playerModel.SlideWall = false;
            }

            if (aplication.playerModel.JumpWall)
            {

                var diraction = aplication.playerModel.SlideWall ? -aplication.playerModel.horizontalRaw : -hit.normal.x;


                aplication.jumpWall.Jump(new Vector2((float)powerWallJump, (float)Math.Sqrt(powerWallJump)), diraction, false);

            }
        }
        private void StopWallJump()
        {
            aplication.playerModel.JumpWall = false;
        }

        public void Death()
        {
            if (aplication.playerModel.Death)
                StartCoroutine(aplication.death.Death());

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawRay(transform.position, new Vector2(distance, 0));
        }
    }
}