using UnityEngine;
using System.Collections;
using System;

namespace Ggj.Player {
	public class PlayerMove : MonoBehaviour {

        public Material PlayerMaterial;
        public Material DeadPlayerMaterial;

        public AudioClip PlayerDies;


		static class StateTags {
			public const string Idle = "Idle";
            public const string Ducking = "Ducking";
		}

		static class AnimatorParams {
	        public const string ShouldHop = "ShouldHop";
            public const string ShouldIdlePant = "ShouldIdlePant";
            public const string IsDucking = "IsDucking";
            public const string IsDead = "IsDead";
		}

        Animator animator;

		void Start () {
			animator = GetComponent<Animator> ();
		}

        bool IsIdle {
            get {
                return animator.GetCurrentAnimatorStateInfo (0).IsTag (StateTags.Idle);
            }
        }

        bool IsDucking {
            get {
                return animator.GetCurrentAnimatorStateInfo (0).IsTag (StateTags.Ducking);
            }
        }

		void Update () {
            float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

            bool noMoveControl =
				Math.Abs (moveVertical) < 0.01f &&
				Math.Abs (moveHorizontal) < 0.01f;

            bool duckControl = Input.GetAxis ("Duck") > 0.01f;

            animator.SetBool (AnimatorParams.IsDucking, duckControl);

            if (IsIdle && !noMoveControl) {
                transform.rotation = CalculateNewRotation ();
                animator.SetTrigger (AnimatorParams.ShouldHop);
            } else if (IsIdle && duckControl) {
                animator.SetBool (AnimatorParams.IsDucking, true);
            } else if (IsDucking && !duckControl) {
                animator.SetBool (AnimatorParams.IsDucking, false);
            }
		}

		Quaternion CalculateNewRotation() {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			double rawMoveDirectionRadians = Math.Atan2 (moveVertical, moveHorizontal);
			double rawMoveDirection = (360 / (2 * Math.PI)) * rawMoveDirectionRadians;
			double moveDirectionQuantized = 90 * Math.Floor (rawMoveDirection / 90);
			double moveDirection = 90 - moveDirectionQuantized;

			var rotation = transform.rotation;
			var angles = rotation.eulerAngles;
			angles.y = (float) moveDirection;
			rotation.eulerAngles = angles;
			return rotation;
		}

        public void Kill() {
            Debug.Log ("Player killed!");
            var playerRenderer = GetComponentInChildren<Renderer> ();
            playerRenderer.material = DeadPlayerMaterial;
            animator.SetBool (AnimatorParams.IsDead, true);
            audio.PlayOneShot (PlayerDies);
        }
	}
}