using UnityEngine;

public class PlayerElement : MonoBehaviour
{
    public PlayerAplication aplication { get { return FindObjectOfType<PlayerAplication>(); } }
}