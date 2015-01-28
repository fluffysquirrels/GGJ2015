using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class EndSquareTrigger : MonoBehaviour {
        void OnTriggerEnter(Collider c) {
            var playerBehaviour = c.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            Application.LoadLevel ("MainMenu");
        }
	}
}
