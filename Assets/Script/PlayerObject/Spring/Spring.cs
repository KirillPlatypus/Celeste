using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : IPlayerObjects
{
    public bool onLocalSpring;
    public Vector2 PowerRebount;
    
    [SerializeField] private new Animator animation;

    private void Awake()
    {

        PowerRebount *= transform.up;

    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up);

        animation.SetBool("OnSpring", onLocalSpring);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerAplication.playerModel.onSpring = true;
            onLocalSpring = true;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerAplication.playerModel.onSpring = false;
            onLocalSpring = false;
        }
    }
}
