using UnityEngine;

namespace Player.CheckAnymore
{
    public class CheckFloor : ICheckAnymore
    {
        [SerializeField] private GameObject FloorPoint;

        private void Update()
        {
            aplication.playerModel.OnFloor = Physics2D.OverlapCircle(FloorPoint.transform.position, radius, mask);
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(FloorPoint.transform.position, radius);
        }
    }
}