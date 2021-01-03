using UnityEngine;
using Traps;

public class Spike : IPlayerObjects, ITrap
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerAplication.playerModel.Death = true;


        }
    }
}
