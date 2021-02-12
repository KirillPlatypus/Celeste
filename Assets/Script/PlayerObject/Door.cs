using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System.Threading.Tasks;

public class Door : MonoBehaviour
{
    [SerializeField] private LoadScene scene;
    [SerializeField] private KeyCode button;
    [SerializeField] private string sceneName;
    [SerializeField] Vector2 size;
    [SerializeField] private LayerMask mask;
    private bool AboutDoor;


    void Update()
    {
        AboutDoor = Physics2D.OverlapBox(transform.position, size, 1f, mask);

        if(Input.GetKeyDown(button) && AboutDoor)
        {
            StartCoroutine(scene.Loading(sceneName));
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, size);
    }

}
