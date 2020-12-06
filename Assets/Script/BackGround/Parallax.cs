using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float parralaxPower, colliderLength;
    [SerializeField] private Rigidbody2D Player;

    private float positionX;

    void Start()
    {
        colliderLength = GetComponent<SpriteRenderer>().bounds.size.x;
        positionX = transform.position.x;
    }


    void Update()
    {
        var temp = mainCamera.transform.position.x * (1 - parralaxPower);
       
        var distation = mainCamera.transform.position.x * parralaxPower;

        transform.position = new Vector3(positionX + distation, transform.position.y, transform.position.z);

        if (temp > positionX + colliderLength)
        {
            positionX += colliderLength;

        }
        else if (temp < positionX - colliderLength)
        {
            positionX -= colliderLength;
        }
    }

}
