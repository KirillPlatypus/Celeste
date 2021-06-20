using UnityEngine;
namespace Player.Controller.Move
{
    public class Movement : PlayerElement
    {
        public void Idle(IMovement movement, Vector2 InputMove)
        {
            movement.SetMovement(InputMove);
        }
    }
}