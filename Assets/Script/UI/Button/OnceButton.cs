using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Player.Model;

namespace UI.Button
{
    public class OnceButton : MonoBehaviour, IPointerDownHandler
    {
	    public static bool pointerDown;
		public static bool GetButtonStatus() => pointerDown;

		PlayerModel player {get =>  FindObjectOfType<PlayerModel>();}

		private void Update()
		{
			if(player.OnFloor)
				pointerDown = false;
		}
	    public void OnPointerDown(PointerEventData eventData)
	    {
            if(pointerDown)
	    	    pointerDown = false;

            else 
                pointerDown = true;    
	    }
    } 
}