using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Controller
{
    public sealed class IllusionPlayer : PlayerElement
    {
        [SerializeField] private SpriteRenderer Renderer;
        [SerializeField] private Transform Player;

        private const float timeEnd = 2f;
        private float time = timeEnd;

        void Start()
        {
            transform.localScale = aplication.transform.localScale;
            Renderer.sprite = aplication.renderer.sprite;
        }


        void Update()
        {

            time -= Time.deltaTime;
            if (!aplication.playerModel.Dash && time <= 0)
            {
                Destroy(gameObject);
                time = timeEnd;
            }
        }
    }
}
