using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class SafeSquareTrigger : SpikeTrigger {

        public SafeSquareTrigger() {
            this.OnStartAttackTimer += (player) => {
                player.EnterIdlePant();
                if (player.CountdownText != null) {
                    player.CountdownText.StartCountdown ();
                }
            };
        }
	}
}
