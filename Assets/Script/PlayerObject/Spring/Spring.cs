﻿using UnityEngine;

namespace PlayerObject.Spring
{
    public class Spring : IPlayerObjects, ISpring
    {
        public bool OnLocalSpring { get; set; }

        [SerializeField] private bool onSpring;
        [SerializeField] [Range(1f, 20f)] float power;
        
        public Vector2 RebountPower { get; set; }

        [SerializeField] private new Animator animation;

        private void Awake()
        {

            RebountPower = transform.up * power;

        }
        void Update()
        {
             

            Debug.DrawRay(transform.position, transform.up);

            if (OnLocalSpring)
            {
                Rebount();
            }

            animation.SetBool("OnSpring", OnLocalSpring);


        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                playerAplication.playerModel.onSpring = true;
                onSpring = true;
                OnLocalSpring = true;

            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                playerAplication.playerModel.onSpring = false;
                OnLocalSpring = false;
                onSpring = false;
            }
        }

        public void Rebount()
        {
            playerAplication._Body.velocity = RebountPower;
        }
    }
}