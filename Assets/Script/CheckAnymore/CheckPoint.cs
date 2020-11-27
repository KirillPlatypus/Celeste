using UnityEngine;
using Controller;
using DB;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] internal bool OnCheckpoint;
    [SerializeField] private LayerMask mask;
    [SerializeField] private CoordinateController saveCoordinate;

    private delegate void updateCoordinate(string name);
    private event updateCoordinate updated;

    private void Awake()
    {
        updated += saveCoordinate.CoordinateUpdate;

    }
    public void Update()
    {

        if (OnCheckpoint && gameObject.name != ModuleDB.coordinateTable.Name)
        {
            updated(gameObject.name);
        }

        OnCheckpoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}