using UnityEngine.PlayerLoop;

namespace Controller
{
    public class AnimationController : PlayerElement
    {
        private void Update()
        {

            if (aplication.playerModel.OnFloor)
            {
                aplication.animationPlayer.SetBool("IsMove", aplication.playerModel.Idle);
            }
            else
            {
                aplication.animationPlayer.SetBool("IsMove", false);
            }

            if (!aplication.playerModel.OnFloor || aplication.playerModel.Jump)
            {
                if (aplication._Body.velocity.y > 0)
                {
                    aplication.animationPlayer.SetBool("IsJumpUp", true);
                    aplication.animationPlayer.SetBool("IsJumpDown", false);

                }
                else if (aplication._Body.velocity.y < 0)
                {
                    aplication.animationPlayer.SetBool("IsJumpUp", false);
                    aplication.animationPlayer.SetBool("IsJumpDown", true);
                }
            }
            else
            {

                aplication.animationPlayer.SetBool("IsJumpUp", false);
                aplication.animationPlayer.SetBool("IsJumpDown", false);

            }

            aplication.animationPlayer.SetBool("IsDash", aplication.playerModel.Dash);
        }
    }
}