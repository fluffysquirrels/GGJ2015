using UnityEngine;
using System;

namespace Ggj.Prefabs {
	public class PlayerMove : MonoBehaviour {

        public Material PlayerMaterial;
        public Material DeadPlayerMaterial;

        public AudioClip PlayerDies;

        public CountdownText CountdownText;

        public MusicController Music;

        private Vector3? initialPosition;

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

        bool NoMoveCommand {
            get {
                float moveHorizontal = Input.GetAxis ("Horizontal");
                float moveVertical = Input.GetAxis ("Vertical");

                return
                    Math.Abs (moveVertical) < 0.01f &&
                    Math.Abs (moveHorizontal) < 0.01f;
            }
        }

		void Update () {
            if (!initialPosition.HasValue) {
                initialPosition = transform.position;
            }

            // Process commands
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.LoadLevel("MainMenu");
            } else if (Input.GetAxis ("Restart") > 0.01f) {
                RestartLevel ();
            } else if (IsIdle && !NoMoveCommand) {
                transform.rotation = CalculateNewRotation ();
                animator.SetTrigger (AnimatorParams.ShouldHop);
            }

            // Update state
            bool duckCommand = Input.GetAxis ("Duck") > 0.01f;
            animator.SetBool (AnimatorParams.IsDucking, duckCommand);

            UpdatePlayerMaterial ();
            
            // fudge position to a grid point.
            if ( IsIdle ) {
            	transform.position = ClampPosition(transform.position);
            	//transform.rotation = ClampRotation(transform.rotation);
            }
		}

        void RestartLevel () {
            Application.LoadLevel (Application.loadedLevel);
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
            if (animator.GetBool (AnimatorParams.IsDead)) {
                // Don't die twice (we're not Bond, James Bond).
                return;
            }
            animator.SetBool (AnimatorParams.IsDead, true);
            UpdatePlayerMaterial ();
            Music.StopMusic ();
            Music.EffectsSource.PlayOneShot (PlayerDies);
            CountdownText.PlayerDead ();
        }

        private void UpdatePlayerMaterial() {
            var playerRenderer = GetComponentInChildren<Renderer> ();
            playerRenderer.material =
                animator.GetBool (AnimatorParams.IsDead)
                ? DeadPlayerMaterial
                : PlayerMaterial;
        }
        
        private Vector3 ClampPosition(Vector3 pos) {
        	pos.x = Mathf.Round(pos.x);
        	pos.y = 0f;
        	pos.z = Mathf.Round(pos.z);
        	
        	return pos;
        }
        
        private Vector3 ClampRotation(Vector3 rot) {
        	return rot;
        }
        
        public void EnterIdlePant()
        {
        	animator.SetTrigger (AnimatorParams.ShouldIdlePant);
            animator.ResetTrigger (AnimatorParams.ShouldHop);
        }
	}
}