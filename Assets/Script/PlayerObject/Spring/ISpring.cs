using UnityEngine;

namespace PlayerObject.Spring
{
    public interface ISpring
    {
        Vector2 RebountPower { get; set; }

        bool OnLocalSpring { get; set; }
        void Rebount();
        void Update();
    }
}