using UnityEngine;

namespace Player.Controller.Jump
{
    public interface IJump
    {
        void Jump(Vector2 power, float diraction, bool buttonDown);
    }
}
