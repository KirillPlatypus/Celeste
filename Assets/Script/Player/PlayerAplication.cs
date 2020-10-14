using Cinemachine;
using UnityEngine;
using Controller;

public sealed class PlayerAplication : MonoBehaviour
{
    public new SpriteRenderer renderer;
    public new Transform transform;

    public PlayerView playerView;
    public PlayerModel playerModel;
    public ResolutionDirectionController direction;

    public Movement movement;

    public JumpController jump;

    public RebountOffSpring rebount;

    public WallController wall;
    public JumpWallController jumpWall;

    public DashController dash;

    public EffectsPlayerController effects;

    public SlideController slide;

    public new AnimationController animation;

    public CinemachineVirtualCamera cinemachine;
    internal CinemachineBasicMultiChannelPerlin cameraShake;

    public IllusionPlayer illusion;

    public CoordinateController saveCoordinate;

    public CameraPlayer playerCamera;
    public Rigidbody2D _Body;
    public Animator animationPlayer;
}