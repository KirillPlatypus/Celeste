using UnityEngine;

namespace Player.Controller.Move
{
    public class WallMovementController : Movement, IMovement
    {
        public void SetMovement(Vector2 move)
        {
            aplication._Body.velocity = new Vector2(aplication._Body.velocity.x, move.y * aplication.playerModel.speedY);
        }
    }
}