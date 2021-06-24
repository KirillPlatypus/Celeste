using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System.Threading.Tasks;
using PlayerObject;
using UI.Button;

public class Door : ICheckPoint, IPoint
{
    [SerializeField] private LoadScene scene;
    [SerializeField] private string sceneName;
    [SerializeField] Vector2 size;

    public bool OnPoint{get; set;}

    void Update()
    {
        OnPoint = Physics2D.OverlapBox(transform.position, size, 1f, mask);

        if(HoldButton.GetButtonStatus(ButtonCode.InteractionButton) && OnPoint)
        {
            SaveOnPoint(OnPoint, gameObject.name, ModuleDB.coordinateTable.Name, transform.position);
            scene.Loading(sceneName);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, size);
    }
}
