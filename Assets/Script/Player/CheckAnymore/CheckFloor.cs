using UnityEngine;

namespace Player.CheckAnymore
{
    public class CheckFloor : PlayerElement        
    {
        [SerializeField] private GameObject FloorPoint;

        [SerializeField] private float radius;
        [SerializeField] private LayerMask mask; 

        private void Update()
        {
            aplication.playerModel.OnFloor = Physics2D.OverlapCircle(FloorPoint.transform.position, radius, mask);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(FloorPoint.transform.position, radius);
        }
    }
}