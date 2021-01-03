﻿using UnityEngine;

namespace Player.CheckAnymore
{

    internal class CheckWall : ICheckAnymore
    {
        [SerializeField] private GameObject leftWallPoint;
        [SerializeField] private GameObject rightWallPoint;
        [SerializeField] private Vector2 size;

        private void Update()
        {
            aplication.playerModel.OnWall = Physics2D.OverlapBox(leftWallPoint.transform.position, size, radius, mask)
                                         || Physics2D.OverlapBox(rightWallPoint.transform.position, size, radius, mask);
            aplication.playerModel.OnLeftWall = Physics2D.OverlapBox(leftWallPoint.transform.position, size, radius, mask);
            aplication.playerModel.OnRightWall = Physics2D.OverlapBox(rightWallPoint.transform.position, size, radius, mask);
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(leftWallPoint.transform.position, size);
            Gizmos.DrawWireCube(rightWallPoint.transform.position, size);
        }
    }
}