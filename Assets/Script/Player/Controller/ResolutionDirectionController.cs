using UnityEngine;

namespace Player.Controller
{
    public class ResolutionDirectionController : PlayerElement
    {
        private bool resolutionDirection = false;

        public bool diractionPlayer(float horizontal, Vector2 _transform)
        {
            if ((horizontal < 0 && !resolutionDirection) || (horizontal > 0 && resolutionDirection))
            {
                resolutionDirection = !resolutionDirection;

                var scale = _transform;
                scale.x *= -1;
                transform.localScale = scale;
            }
            return resolutionDirection;
        }
    }
}