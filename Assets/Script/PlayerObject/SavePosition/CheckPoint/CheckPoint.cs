using UnityEngine;
using Player.Controller;
using DB;
using Player.CheckAnymore;
using Game;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;
using PlayerObject;

public class CheckPoint : ICheckPoint, IPoint
{
    public bool OnPoint{get; set;}

    public void Update()
    {
        OnPoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);
        
        SaveOnPoint(OnPoint, gameObject.name, ModuleDB.coordinateTable.Name, new Vector2(playerAplication.transform.position.x , FindSavePointPlayer()));
    }

    public float FindSavePointPlayer()
    {
        if(transform.localScale.y > playerAplication.transform.localScale.y
        || transform.localScale.y < playerAplication.transform.localScale.y)
        {   
            return (transform.position.y - transform.localScale.y/2) + playerAplication.transform.localScale.y/2;
        }       
        else if(transform.localScale.y == playerAplication.transform.localScale.y)
        {    
            return transform.position.y;
        }
       return transform.position.y;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
