using UnityEngine;
using Controller;
using DB;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] internal bool OnCheckpoint;
    [SerializeField] private LayerMask mask;
    [SerializeField] private CoordinateController saveCoordinate;

   
    public void Update()
    {

        if (OnCheckpoint && gameObject.name != saveCoordinate.coordinateTable.Name)
        {
            //saveCoordinate.DeleteCoordinate();
            //saveCoordinate.SaveCoordinate(gameObject.name);
            saveCoordinate.UpdateCoordinate(gameObject.name);
        }
        OnCheckpoint = Physics2D.OverlapBox(transform.position, transform.localScale, 1f, mask);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}