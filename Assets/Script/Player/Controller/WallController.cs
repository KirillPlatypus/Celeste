using UnityEngine;

namespace Player.Controller
{
    public class WallController : PlayerElement
    {
        public bool DragOnWall(bool button )
        {
            if (aplication.playerModel.OnWall && button)
            {
                aplication._Body.gravityScale = 0;

                if (aplication.playerModel.OnLeftWall)
                {

                    aplication.direction.diractionPlayer(-transform.localScale.x, transform.localScale);

                }
                return true;
            }
            else if (!aplication.playerModel.Dash)
            {
                aplication._Body.gravityScale = 1;
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}