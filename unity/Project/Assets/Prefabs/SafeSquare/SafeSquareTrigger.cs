using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class SafeSquareTrigger : SpikeTrigger {

        private PlayerMove currentPlayer;

		public MusicController Music;

		// this safe squares reference, must be unique and set in editor
		public int SafeSquareReference = 0;

        public void Awake()
		{
			GameObject _music = GameObject.FindGameObjectWithTag("MusicController");
			Music = _music.GetComponent<MusicController>();
		}

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
			Music.StopMusic();
			Music.StartMusic();

            if (playerBehaviour == null) {
                return;
            }

            this.AttackTimerDone ();
        }
	}
}
