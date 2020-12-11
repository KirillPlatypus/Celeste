using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : IPlayerObjects
{
    [SerializeField] private Collider2D colliderPlatform;

    private delegate void playerCheck(bool massage);

    private event playerCheck objectHigher;

    private void Awake()
    {
        objectHigher += Platform_obectHigher;

    }
    private void Update()
    {
        if(objectHigher != null)
        {
            var _message = playerAplication.transform.position.y * 0.70f <= transform.position.y ? false : true;

            if (_message)
            {
                objectHigher(_message);
            }
            else
            {
                objectHigher(_message);

            }
        }
    }

    private void Platform_obectHigher(bool message)
    {
        colliderPlatform.enabled = message;
    }
}
