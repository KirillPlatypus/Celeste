using UnityEngine;
using Player.Controller;
using DB;
using Player.CheckAnymore;
using System.Linq;

public class CheckPoint : ICheckAnymore
{
    private delegate void updateCoordinate(string name);
    private event updateCoordinate updated;


    [SerializeField] internal bool OnCheckpoint;
    [SerializeField] private CoordinateController saveCoordinate;


    private void Start()
    {
        updated += saveCoordinate.CoordinateUpdate;
    }

    public void Update()
    {
        if (OnCheckpoint && gameObject.name != ModuleDB.coordinateTable.Name)
        {

            saveCoordinate.CoordinateUpdate(gameObject.name);

        }
        OnCheckpoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
