using System;
using System.Collections;
using UnityEngine;

public class JumpWallController : PlayerElement
{
	[SerializeField] private float distance;

	[SerializeField] private LayerMask mask;

	[SerializeField] [Range(0.1f, 1f)] private float time;


	public void WallJump( Vector2 power, float diraction)
    {

		aplication._Body.velocity = new Vector2(power.x * diraction, power.y);

	}
    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;

		Gizmos.DrawRay(transform.position, new Vector2(transform.localScale.x, 0) * distance);
	}
}