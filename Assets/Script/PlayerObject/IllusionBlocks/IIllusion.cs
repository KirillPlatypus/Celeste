using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace PlayerObject.IllusionBlocks
{
    public interface IIllusion
    {
        bool OnIllusionBlock {get; set;}
        IEnumerator DisableIllusion(SpriteRenderer sprite);
    }
}