using UnityEngine;
using System.Collections;
using Ggj.Player;
using Ggj.Prefabs;

namespace Ggj.Scripts {
    public class KillPlayerTrigger : MonoBehaviour {

        public bool DeleteThisOnCollisionWithPlayer;

        void OnTriggerEnter(Collider c) {
            var playerBehaviour = c.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            playerBehaviour.Kill ();

            if (DeleteThisOnCollisionWithPlayer) {
                Object.Destroy (this.gameObject);
            }
        }
    }
}
