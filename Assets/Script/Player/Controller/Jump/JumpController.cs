using UnityEngine;

namespace Player.Controller.Jump
{
    public class JumpController : PlayerElement, IJump
    {
        public float powerMin;
        public float powerMax;

        public void Jump(Vector2 power, float diraction, bool buttonDown)
        {
            if (buttonDown && aplication.playerModel.OnFloor)
            {
                aplication._Body.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            }
        }

        public void SetGravity(bool button)
        {
            if (aplication._Body.velocity.y < 0.0f)
            {
                aplication._Body.velocity += Vector2.up * Physics2D.gravity.y * (powerMin - 1) * Time.deltaTime;
            }
            else if (aplication._Body.velocity.y > 0.0f && !button)
            {
                aplication._Body.velocity += Vector2.up * Physics2D.gravity.y * (powerMax - 1) * Time.deltaTime;
            }
        }
    }
}