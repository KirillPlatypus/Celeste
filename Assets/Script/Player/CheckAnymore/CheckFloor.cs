using UnityEngine;

public class CheckFloor : PlayerElement
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject FloorPoint;
    [SerializeField] private LayerMask mask;

    private void Update()
    {
        aplication.playerModel.OnFloor = Physics2D.OverlapCircle(FloorPoint.transform.position, radius, mask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(FloorPoint.transform.position, radius);
    }
}