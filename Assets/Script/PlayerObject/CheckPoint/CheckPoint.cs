using UnityEngine;
using Player.Controller;
using DB;
using Player.CheckAnymore;
using Game;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;


public class CheckPoint : IPlayerObjects
{
    [SerializeField] Scene LastScene;

    [SerializeField] internal bool OnCheckpoint;

    [SerializeField] private LayerMask mask;

    private IDataAccessor saveCoordinate;


    private void Start() 
    {
        saveCoordinate = new CoordinateDataAccessor(playerAplication.transform); 
    }

    public void Update()
    {
        
        if (OnCheckpoint && gameObject.name != ModuleDB.coordinateTable.Name)
        {

            saveCoordinate.UpdateData((string)gameObject.name);

        }
        
        OnCheckpoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
