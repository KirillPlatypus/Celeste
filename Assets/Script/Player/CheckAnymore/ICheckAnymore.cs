using System;
using UnityEngine;

namespace Player.CheckAnymore
{
    public abstract class ICheckAnymore : PlayerElement
    {
        public float radius;
        public LayerMask mask;
        public abstract void OnDrawGizmos();
    }
}
