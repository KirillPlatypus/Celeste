using UnityEngine.PlayerLoop;

namespace Player.Controller
{
    public class AnimationController : PlayerElement
    {
        IAnimation[] concreteAnimation = { new MoveAnimation(), new JumpAnimation(), new DashAnimation(), new DeathAnimation()};

        private void Update()
        {
            foreach (IAnimation Anim in concreteAnimation) {
                Anim.CauseChange("");
            }
        }
    }


    interface IAnimation
    {
        void CauseChange(string name); 
    }

    class MoveAnimation : PlayerElement, IAnimation
    {
        public void CauseChange(string name)
        {
            if (aplication.playerModel.OnFloor)
            {
                aplication.animationPlayer.SetBool("IsMove", aplication.playerModel.Idle);
            }
            else
            {
                aplication.animationPlayer.SetBool("IsMove", false);
            }

        }
    }
    class JumpAnimation : PlayerElement, IAnimation
    {
        public void CauseChange(string name)
        {
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
        }
    }
    class DashAnimation : PlayerElement, IAnimation
    {
        public void CauseChange(string name)
        {
            aplication.animationPlayer.SetBool("IsDash", aplication.playerModel.Dash);
        }
    }

    class DeathAnimation : PlayerElement, IAnimation
    {
        public void CauseChange(string name)
        {

        }
    }
}