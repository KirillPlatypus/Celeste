using Cinemachine;
using System;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerView : PlayerElement
{
    [SerializeField] private float distance;

    private RaycastHit2D hit;

    private double powerWallJump;

    private void Start()
    {
        transform.position = aplication.saveCoordinate.CoordinateRead();
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        aplication.dash.SetDash();

        aplication.playerModel.horizontal = Input.GetAxis("Horizontal");
        aplication.playerModel.vertical = Input.GetAxis("Vertical");

        aplication.playerModel.horizontalRaw = Input.GetAxisRaw("Horizontal");
        aplication.playerModel.verticalRaw = Input.GetAxisRaw("Vertical");

        hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), distance);

        #region Movement

        Idle();

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

        #region Spring
        aplication.rebount.GetSpringPower();
        #endregion



        #region Diraction

        if (!aplication.wall.DragOnWall() && !aplication.playerModel.JumpHangWall && !aplication.playerModel.JumpWall)
        {

            aplication.direction.diractionPlayer(aplication.playerModel.horizontalRaw, transform.localScale);

        }

        #endregion Diraction




        if (aplication._Body.velocity.y <= -2.5f && !aplication.dash)
        {
            aplication._Body.velocity = new Vector2(aplication._Body.velocity.x, -2.0f);
        }
        if (aplication.playerModel.onSpring)
        {
            aplication.playerModel.Dash = false;

        }


    }


    private void Idle()
    {
        if (!aplication.wall.DragOnWall() && !aplication.playerModel.JumpWall && !aplication.playerModel.Dash)
        {
            aplication.movement.Idle(new MovementController());

            aplication.playerModel.Idle = aplication._Body.velocity.x != 0 ? true : false;

        }
    }


    private void Jump()
    {
        if (!aplication.wall.DragOnWall() && !aplication.playerModel.JumpWall && !aplication.playerModel.Dash)
        {
            aplication.jump.jumping();
            aplication.jump.SetGravity();

            aplication.playerModel.Jump = aplication._Body.velocity.y != 0 ? true : false;

        }

    }


    private void Dash()
    {

        if (aplication.playerModel.DashingTime)
        {
            //Debug.DrawRay(spawnRayDiraction.transform.position, _Body.velocity, Color.yellow, distance);

            aplication.dash.Dash(aplication.playerModel.horizontalRaw, aplication.playerModel.verticalRaw);
        }

    }


    private void IdleWall()
    {
        if (aplication.wall.DragOnWall() && !aplication.playerModel.Dash)
        {
            aplication.movement.Idle(new WallMovementController());
            aplication.playerModel.IdleWall = true;
        }
        else
        {
            aplication.playerModel.IdleWall = false;
        }
    }


    private void WallHangJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && aplication.wall.DragOnWall() && !aplication.playerModel.Dash)
        {

            aplication.playerModel.JumpHangWall = true;

            Invoke(nameof(StopWallHangJump), aplication.playerModel.timeWallHangJump);

        }
        if (aplication.playerModel.JumpHangWall)
        {

            aplication.jumpWall.WallJump(new Vector2((float)aplication.playerModel.powerWallHangJump * 1.8f, (float)Math.Abs(aplication.playerModel.powerWallHangJump)),
                                         aplication.playerModel.horizontalRaw);
        }

    }
    public void StopWallHangJump()
    {
        aplication.playerModel.JumpHangWall = false;
    }

    private void WallJuming()
    {
        if (!aplication.playerModel.OnFloor && !aplication.wall.DragOnWall() && aplication.playerModel.OnRightWall && !aplication.playerModel.Dash)
        {
            aplication.slide.PlayerSlide();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                aplication.direction.diractionPlayer(-transform.localScale.x, transform.localScale);

                powerWallJump = aplication.playerModel.SlideWall ? Math.Sqrt(aplication.playerModel.powerWallJump) : aplication.playerModel.powerWallJump;

                aplication.playerModel.JumpWall = true;

                Invoke(nameof(StopWallJump), aplication.playerModel.timeWallJump);

            }
        }
        else
        {
            aplication.playerModel.SlideWall = false;

            aplication._Body.drag = 0;
        }

        if (aplication.playerModel.JumpWall)
        {

            var diraction = aplication.playerModel.SlideWall ? -aplication.playerModel.horizontalRaw : -hit.normal.x;


            aplication.jumpWall.WallJump(new Vector2((float)powerWallJump, (float)Math.Sqrt(powerWallJump)), diraction);

        }
    }
    public void StopWallJump()
    {
        aplication.playerModel.JumpWall = false;
    }
}