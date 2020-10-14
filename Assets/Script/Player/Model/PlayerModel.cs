using UnityEngine;

public class PlayerModel : PlayerElement
{

    [SerializeField] internal float horizontal ;
    [SerializeField] internal float vertical;
    [SerializeField] internal float horizontalRaw;
    [SerializeField] internal float verticalRaw;

    [SerializeField] [Range(0f, 10f)] internal float timeWallHangJump;
    [SerializeField] [Range(0f, 10f)] internal float timeWallJump;

    [Range(1, 10)] public float speedX;
    [Range(0.1f, 5)] public float speedY;
    [Range(1, 30)] public double dashSpeed;

    [Range(1, 10)] public float standartJumpPower;
    [Range(1, 30)] public double powerWallJump;
    [Range(1, 30)] public double powerWallHangJump;
    [Range(1, 100)] public double powerWallLerpJump;
    
    [Range(0f, 1f)] public float drag;

    public bool Idle;
    public bool IdleWall;
    public bool Jump;
    public bool JumpWall;
    public bool JumpHangWall;
    public bool SlideWall;

    public bool DashingTime = true;
    public bool Dash;

    public bool OnWall;
    public bool OnRightWall;
    public bool OnLeftWall;
    public bool OnFloor;
    public bool onSpring;

}