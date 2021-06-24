using UnityEngine;
using UI.Button;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] static Button[] button;
        Door door;
        
        private void Awake() 
        {
            button = GameObject.FindObjectsOfType<Button>();
            door = GameObject.FindObjectOfType<Door>();
        }

        private void Update() 
        {
            if(door != null)
                DisableButton("Interaction", !door.OnPoint);
            else
                DisableButton("", true);
        }
        private void DisableButton(string name, bool disable)
        {
            if(name == "")
                return;
            for (int i = 0; i < button.Length; i++)
            {
                if(button[i].name == name)
                    if(disable)
                        button[i].interactable = false;
                    else 
                        button[i].interactable = true;   
                   
            }

        }
    }
}