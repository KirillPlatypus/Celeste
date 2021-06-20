using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField] CinemachineConfiner cinemachine;

    void Update()
    {
        var objectBackground = GameObject.FindGameObjectWithTag("BackGround");
        var cameraBorder = objectBackground.GetComponent<PolygonCollider2D>();

        cinemachine.m_BoundingShape2D = cameraBorder;
    }
}
