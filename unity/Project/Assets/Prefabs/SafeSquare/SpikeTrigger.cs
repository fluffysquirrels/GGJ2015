using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
	public class SpikeTrigger : MonoBehaviour {
	
		public float AttackDelaySeconds;

		private float TimerCurrent = 0f;
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
		
		void Update () {
			if ( TimerRunning )
			{
				TimerCurrent += Time.deltaTime;
			}
		
			if ( TimerCurrent >= AttackDelaySeconds )
			{
                anim.SetTrigger(AnimParams.ShouldAttack);

				TimerCurrent = 0f;
				TimerRunning = false;
                countdown.enabled = false;
                countdown.renderer.enabled = false;
			}
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
		}

        void StopAttackTimer() {
            TimerCurrent = 0f;
            TimerRunning = false;
            countdown.enabled = false;
            countdown.renderer.enabled = false;
        }
	}
}
