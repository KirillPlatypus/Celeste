using System;
using UnityEngine;
using System.Collections;

namespace PlayerObject.IllusionBlocks
{
    public class IllusionBlock : MonoBehaviour, IIllusion
    {

        public bool OnIllusionBlock {get; set;}

        [SerializeField] private Vector2 size;
        [SerializeField] private LayerMask mask;
        [SerializeField] Color color;

        GroupIllusionBlock group;

        static float a = 1;

        private void Awake() 
        {
            group = GetComponentInParent<GroupIllusionBlock>();
        }

        private void OnEnable() 
        {
            group.OnBlock += DisableIllusion;
        }
        private void OnDisable() 
        {
            group.OnBlock -= DisableIllusion;
        }

        private void Update() 
        {    
            OnIllusionBlock = Physics2D.OverlapBox(transform.position, size, 1f, mask);
        }

        public IEnumerator DisableIllusion(SpriteRenderer sprite)
        {

            for (; a >= 0; a -= 0.001f)
            {          
                color.a = a;  
                sprite.color = color;

                yield return new WaitForSeconds(0.001f);
            
            }
            if(a <= 0.01f)
                 Destroy(gameObject);
            
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}