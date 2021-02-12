using UnityEngine;

namespace Player.Controller
{
    public class SlideController : PlayerElement
    {
        public void PlayerSlide()
        {
            if ((aplication.playerModel.OnRightWall && aplication.playerModel.horizontalRaw != 0))
            {
                aplication._Body.velocity = Vector2.zero;
                aplication._Body.gravityScale = 0.1f;

                aplication.playerModel.SlideWall = true;

            }
            else
            {
                aplication.playerModel.SlideWall = false;

                aplication._Body.gravityScale = 1;
            }
        }
    }
}