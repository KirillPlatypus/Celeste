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
                aplication._Body.drag = aplication.playerModel.drag;

                aplication.playerModel.SlideWall = true;

            }
            else
            {
                aplication.playerModel.SlideWall = false;

                aplication._Body.drag = 0;
            }
        }
    }
}