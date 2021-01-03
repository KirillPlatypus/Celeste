using UnityEngine;

namespace Player.Controller.Jump
{
    public class JumpController : PlayerElement, IJump
    {
        public float powerMin;
        public float powerMax;

        public void Jump(Vector2 power, float diraction)
        {
            if (Input.GetButtonDown("Jump") && aplication.playerModel.OnFloor)
            {
                aplication._Body.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            }
        }

        public void SetGravity()
        {
            if (aplication._Body.velocity.y < 0.0f)
            {
                aplication._Body.velocity += Vector2.up * Physics2D.gravity.y * (powerMin - 1) * Time.fixedDeltaTime;
            }
            else if (aplication._Body.velocity.y > 0.0f && !Input.GetButton("Jump"))
            {
                aplication._Body.velocity += Vector2.up * Physics2D.gravity.y * (powerMax - 1) * Time.fixedDeltaTime;
            }
        }
    }
}