using UnityEngine;

namespace Player.Controller.Move
{
    public class WallMovementController : Movement, IMovement
    {
        public void SetMovement(Vector2 move)
        {
            aplication._Body.velocity = new Vector2(1, move.y * aplication.playerModel.speedY);
        }
    }
}