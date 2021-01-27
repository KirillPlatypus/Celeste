using UnityEngine;
using Player.Controller;
using DB;
using Player.CheckAnymore;
using Game;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;


public class CheckPoint : ICheckAnymore
{
    [SerializeField] Scene LastScene;
    [SerializeField] Transform player;

    [SerializeField] internal bool OnCheckpoint;

    private IDataAccessor saveCoordinate;
    private SceneDataAccessor saveScene;    


    private void Start() 
    {
        saveCoordinate = new CoordinateDataAccessor(player); 
        saveScene = new SceneDataAccessor();
    }

    public void Update()
    {
        
        if (OnCheckpoint && gameObject.name != ModuleDB.coordinateTable.Name)
        {

            saveCoordinate.UpdateData((string)gameObject.name);

        }
        
        OnCheckpoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
