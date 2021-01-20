using Cinemachine;
using UnityEngine;
using Player.Controller;
using Player.Controller.Move;
using Player.Controller.Jump;
using Player.Model;
using Player.View;
namespace Player
{
    public sealed class PlayerAplication : MonoBehaviour
    {
        public new SpriteRenderer renderer;
        public new Transform transform;
        public new Collider2D collider;

        public PlayerView playerView;
        public PlayerModel playerModel;
        public ResolutionDirectionController direction;

        public Movement movement;

        public JumpController jump;

        public WallController wall;
        public JumpWallController jumpWall;

        public DashController dash;

        public EffectsPlayerController effects;

        public SlideController slide;

        public new AnimationController animation;

        public CinemachineVirtualCamera cinemachine;
        internal CinemachineBasicMultiChannelPerlin cameraShake;

        public IllusionPlayer illusion;

        public DeathController death;

        public CameraPlayer playerCamera;
        public Rigidbody2D _Body;
        public Animator animationPlayer;
    }
}