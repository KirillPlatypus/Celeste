using UnityEngine;

public class Movement : PlayerElement
{
    internal Vector2 InputMove;

    public void Idle(IMovement movement)
    {
        InputMove = new Vector2(aplication.playerModel.horizontal, aplication.playerModel.vertical);

        movement.SetMovement(InputMove);
    }
}