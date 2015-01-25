using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
	public class KillTrigger : MonoBehaviour {
	
		void OnTriggerEnter(Collider c) {
			var playerBehaviour = c.GetComponent<PlayerMove> ();
			
			if (playerBehaviour == null) {
				// Only interested in colliding with player.
				return;
			}
			
			playerBehaviour.Kill ();
		}
	}
}
