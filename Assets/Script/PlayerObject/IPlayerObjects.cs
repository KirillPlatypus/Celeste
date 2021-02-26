using UnityEngine;
using Player;

public abstract class IPlayerObjects : MonoBehaviour
{
    protected PlayerAplication playerAplication { get { return FindObjectOfType<PlayerAplication>(); } }
}
