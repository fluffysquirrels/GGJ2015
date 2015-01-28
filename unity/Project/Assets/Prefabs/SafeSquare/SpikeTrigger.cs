using UnityEngine;
using System.Collections;
using Ggj.Player;
using System;

namespace Ggj.Prefabs {
	public class SpikeTrigger : MonoBehaviour {
	
		public float AttackDelaySeconds;
        public bool HoldAttack;

        public delegate void StartAttackTimerDelegate (PlayerMove playerBehaviour);
        public event StartAttackTimerDelegate OnStartAttackTimer;
        public event Action OnEndAttackTimer;

		private bool TimerRunning = false;

        private PieChartCountdownMesh countdown;
        protected Animator animator;
		
        public static class AnimParams {
            public const string ShouldAttack = "ShouldAttack";
            public const string ShouldAttackAndHold = "ShouldAttackAndHold";
        }

        public static class StateTags {
            public const string Idle = "Idle";
            public const string Attack = "Attack";
        }

        public SpikeTrigger() {
            // Set default event handlers to avoid null reference exceptions
            // when they're invoked.
            OnStartAttackTimer += (pb) => {};
            OnEndAttackTimer += () => {};
        }

		void Start () {
			animator = GetComponentInChildren<Animator>();
            countdown = GetComponentInChildren<PieChartCountdownMesh> ();
            countdown.DeltaPerSecond = 1 / AttackDelaySeconds;
            countdown.enabled = false;
		}
		
		public void OnTriggerEnter(Collider other) {
			var playerBehaviour = other.GetComponent<PlayerMove> ();
			
			if (playerBehaviour == null) {
				return;
			}
            if (!TimerRunning && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
                StartAttackTimer (playerBehaviour);
            }
		}

        public void StartAttackTimer(PlayerMove playerBehaviour) {
            countdown.enabled = true;
            countdown.renderer.enabled = true;
            countdown.Value = 0;
            TimerRunning = true;
            Invoke ("AttackTimerDone", AttackDelaySeconds);
            OnStartAttackTimer.Invoke (playerBehaviour);
		}

        public void AttackTimerDone() {
            CancelInvoke ("AttackTimerDone");
            animator.SetTrigger(
                this.HoldAttack ? AnimParams.ShouldAttackAndHold
                                : AnimParams.ShouldAttack);
            OnEndAttackTimer.Invoke ();
            TimerRunning = false;
            countdown.enabled = false;
            countdown.renderer.enabled = false;
        }
	}
}
