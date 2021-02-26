using System;
using System.Collections;
using UnityEngine;

namespace Player.Controller
{
    public class DeathController : PlayerElement
    {
        [SerializeField] private DeathState deathStation;
        public IEnumerator Death()
        {
            switch (deathStation)
            {
                case DeathState.StartDeath:
                    
                        aplication.collider.enabled = false;

                        transform.Translate(new Vector3(0.07f, 0.05f, 0));

                        aplication._Body.bodyType = RigidbodyType2D.Static;
                    
                        yield return new WaitForSeconds(0.5f);
                        deathStation = DeathState.LoadPlayer;
                    
                    break;

                case DeathState.LoadPlayer:

                    yield return new WaitForSeconds(0.3f);

                    deathStation = DeathState.StartDeath;

                    aplication.collider.enabled = true;

                    aplication._Body.bodyType = RigidbodyType2D.Dynamic;

                    transform.position = new Vector2((float)ModuleDB.coordinateTable.CoordinateX, (float)ModuleDB.coordinateTable.CoordinateY);

                    aplication.playerModel.Death = false;

                    break;
            }
        }

        private enum DeathState
        {
            StartDeath,
            LoadPlayer,
        }
    }
}
