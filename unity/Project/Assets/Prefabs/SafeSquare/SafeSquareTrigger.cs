using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class SafeSquareTrigger : SpikeTrigger {

        private PlayerMove currentPlayer;

        public SafeSquareTrigger() {
            this.OnStartAttackTimer += (player) => {
                currentPlayer = player;
                player.EnterIdlePant();
                if (player.CountdownText != null) {
                    player.CountdownText.StartCountdown ();
                }
            };

            this.OnEndAttackTimer += () =>  {
                if (currentPlayer.CountdownText != null) {
                    currentPlayer.CountdownText.StopCountdown ();
                }
            };
        }



        public void OnTriggerExit(Collider other) {
            var playerBehaviour = other.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                return;
            }

            this.AttackTimerDone ();
        }
	}
}
