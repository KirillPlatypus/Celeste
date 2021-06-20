using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Button.Dash
{
    public class DashButton : MonoBehaviour, IButton, IPointerDownHandler, IPointerUpHandler
    {
        public ButtonCode buttonCode { get => ButtonCode.DashButton; }

        public bool pointerDown{ get; set;}

        public bool pointer{ get; set;}

        [SerializeField] bool pointerDownSF;

        private void Awake() {
            pointerDownSF = pointerDown;
        }

        public void OnPointerDown(PointerEventData eventData)
	    {
            pointer = true;
            pointerDown = true;

	    }

	    public void OnPointerUp(PointerEventData eventData)
    	{
            pointer = false;
            pointerDown = false;

	    }

        private void Update() 
        {
            if (pointerDown )
	    		pointerDown = true;

        }


    }
}