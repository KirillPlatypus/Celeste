using UnityEngine;

public class WallController : PlayerElement
{
    [SerializeField] private KeyCode keyHanging;

    public bool DragOnWall()
    {
        if (aplication.playerModel.OnWall && Input.GetKey(keyHanging))
        {
            aplication._Body.gravityScale = 0;

            if (aplication.playerModel.OnLeftWall)
            {

                aplication.direction.diractionPlayer(-transform.localScale.x, transform.localScale);

            }
            return true;
        }
        else if(!aplication.playerModel.Dash)
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