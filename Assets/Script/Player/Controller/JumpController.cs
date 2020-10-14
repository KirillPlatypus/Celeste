using UnityEngine;

public class JumpController : PlayerElement
{
    public float powerMin;
    public float powerMax;

    public void jumping()
    {
        if (Input.GetButtonDown("Jump") && aplication.playerModel.OnFloor)
        {
            aplication._Body.AddForce(Vector2.up * aplication.playerModel.standartJumpPower, ForceMode2D.Impulse);
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