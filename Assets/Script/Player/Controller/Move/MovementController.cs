using UnityEngine;

namespace Player.Controller.Move
{
    public class MovementController : Movement, IMovement
    {
        public void SetMovement(Vector2 move)
        {
            aplication._Body.velocity = new Vector2(move.x * aplication.playerModel.speedX, aplication._Body.velocity.y);
        }
    }
}