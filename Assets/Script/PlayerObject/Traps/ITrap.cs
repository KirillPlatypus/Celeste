using System;
using UnityEngine;

namespace Traps
{
    public interface ITrap
    {
        void OnCollisionEnter2D(Collision2D collision);
    }
}
