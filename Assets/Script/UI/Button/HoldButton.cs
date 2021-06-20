using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UI.Button.Jump;
using System.Collections.Generic;
using System.Linq;

namespace UI.Button
{
    public class HoldButton : MonoBehaviour
    {
	    [SerializeField] private static bool pointerDown;
        [SerializeField] static IButton[] button;
        
        private void Start() 
        {
            button = GetComponentsInChildren<IButton>();
        }

		public static bool GetButtonStatusDown(ButtonCode buttonCode) 
		{
			if(button[(int)buttonCode].buttonCode == buttonCode)
				return button[(int)buttonCode].pointerDown;
		
			return false;
		}
		public static bool GetButtonStatus(ButtonCode buttonCode) 
		{
			if(button[(int)buttonCode].buttonCode == buttonCode)
				return button[(int)buttonCode].pointer;
		
			return false;
		}
    }
    public enum ButtonCode
	{
		JumpButton,
		DashButton,
		InteractionButton
	}

}