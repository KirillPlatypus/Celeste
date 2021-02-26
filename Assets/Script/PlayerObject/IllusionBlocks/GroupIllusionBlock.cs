using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace PlayerObject.IllusionBlocks
{
    public class GroupIllusionBlock : IPlayerObjects
    {
        public delegate IEnumerator disableIllusion(SpriteRenderer sprite);
        public event disableIllusion OnBlock;

        private SpriteRenderer[] sprite;

        [SerializeField] private Color color;
        private IllusionBlock[] illusion;

        private void Awake() 
        {
            illusion = GetComponentsInChildren<IllusionBlock>(); 
            sprite = GetComponentsInChildren<SpriteRenderer>();
        }

        private void FixedUpdate() 
        {
            if(!playerAplication.playerModel.Death)
                DisableIllusions();
        }

        private void DisableIllusions()
        {
            foreach (var ill in illusion)
            {
                if(ill == null)
                    Destroy(gameObject);

                foreach (var item in sprite)
                {
                    if(ill.OnIllusionBlock && OnBlock != null)
                    {
                        StartCoroutine(OnBlock(item));
                    }
                }
            }

        }
    }
}