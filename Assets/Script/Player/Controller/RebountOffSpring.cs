using UnityEngine;
using System.Collections.Generic;

namespace Controller
{
    public sealed class RebountOffSpring : PlayerElement
    {
        private Spring spring;
        [SerializeField] private List<Spring> SpringObject;

        private Dictionary<string, Spring> springs = new Dictionary<string, Spring>();
        private void Awake()
        {
            for (int numberSpring = 0; numberSpring < SpringObject.Count; numberSpring++) 
            {
                springs.Add($"Spring ({numberSpring})", SpringObject[numberSpring]);
            }
        }

        public void GetSpringPower()
        {

            for (int springI = 0; springI <= springs.Count; springI++)
            {
                if (springs.ContainsKey($"Spring ({springI})"))
                {

                    if (springs[$"Spring ({springI})"].onLocalSpring)
                    {
                        aplication._Body.velocity = springs[$"Spring ({springI})"].PowerRebount;
                    }
                } 
            }
        }
    }
}