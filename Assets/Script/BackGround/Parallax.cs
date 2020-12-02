using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float parralaxPower;

    private float positionX;

    void Start()
    {
        positionX = transform.position.x;
    }


    void Update()
    {
        var distation = mainCamera.transform.position.x * parralaxPower;

        transform.position = new Vector3(positionX + distation, transform.position.y, transform.position.z);
    }
}
