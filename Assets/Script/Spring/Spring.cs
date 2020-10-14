using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public bool onLocalSpring;
    [SerializeField] private PlayerAplication aplication;
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
            aplication.playerModel.onSpring = true;
            onLocalSpring = true;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            aplication.playerModel.onSpring = false;
            onLocalSpring = false;
        }
    }
}
