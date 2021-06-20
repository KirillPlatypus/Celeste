using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace UI.Button.Jump
{
    public class JumpButton : MonoBehaviour, IButton, IPointerDownHandler, IPointerUpHandler
    {
        public ButtonCode buttonCode { get => ButtonCode.JumpButton; }

        public bool pointerDown{ get; set;}

        public bool pointer{ get; set;}

        [SerializeField] bool pointerDownSF;
        [SerializeField] bool pointerSF;

        public void OnPointerDown(PointerEventData eventData)
	    {
            pointer = true;
	    	pointerDown = true;

            StartCoroutine(PointerDisable(pointerDown, 0.3f));
            StartCoroutine(PointerDisable(pointer, 0.1f));
	    }

	    public void OnPointerUp(PointerEventData eventData)
    	{
            pointer = false;

	    	pointerDown = false;
	    }

        private IEnumerator PointerDisable(bool point, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            point = false;
        }
        private void Update() 
        {
            pointerDownSF = pointerDown;
            pointerSF = pointer;
        }

    }
}