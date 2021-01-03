using System;
using System.Collections;
using UnityEngine;
namespace Player.Controller.Jump
{
    public class JumpWallController : PlayerElement, IJump
    {
        [SerializeField] private float distance;

        public void Jump(Vector2 power, float diraction)
        {

            aplication._Body.velocity = new Vector2(power.x * diraction, power.y);

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, new Vector2(transform.localScale.x, 0) * distance);
        }
    }
}