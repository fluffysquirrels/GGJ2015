using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
	public class SpikeTrigger : MonoBehaviour {
	
		public float AttackDelaySeconds;

		private bool TimerRunning = false;

        private PieChartCountdownMesh countdown;
        private Animator anim;
		
        private static class AnimParams {
            public const string ShouldAttack = "ShouldAttack";
        }

        private static class StateTags {
            public const string Idle = "Idle";
            public const string Attack = "Attack";
        }

		void Start () {
			anim = GetComponentInChildren<Animator>();
            countdown = GetComponentInChildren<PieChartCountdownMesh> ();
            countdown.DeltaPerSecond = 1 / AttackDelaySeconds;
            countdown.enabled = false;
		}
		
		void OnTriggerEnter(Collider c) {
			var playerBehaviour = c.GetComponent<PlayerMove> ();
			
			if (playerBehaviour == null) {
				// Only interested in colliding with player.
				return;
			}
            if (!TimerRunning && anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
                StartAttackTimer ();
            }
		}
		
		void StartAttackTimer() {
            countdown.enabled = true;
            countdown.renderer.enabled = true;
            countdown.Value = 0;
            TimerRunning = true;
            this.Invoke ("AttackTimerDone", this.AttackDelaySeconds);
		}

        void AttackTimerDone() {
            anim.SetTrigger(AnimParams.ShouldAttack);
            StopAttackTimer ();
        }

        void StopAttackTimer() {
            TimerRunning = false;
            countdown.enabled = false;
            countdown.renderer.enabled = false;
        }
	}
}
