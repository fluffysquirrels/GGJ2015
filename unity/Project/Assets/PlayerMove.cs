using UnityEngine;
using System.Collections;
using System;

namespace Ggj {
	public class PlayerMove : MonoBehaviour {

		void Start () {
			animator = GetComponent<Animator> ();
		}

		Animator animator;
		
		void Update () {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			bool isIdle = animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle");
			if (!isIdle) {
				// Don't move when not idle.
				return;
			}

			bool noMove =
				Math.Abs (moveVertical) < 0.01f &&
				Math.Abs (moveHorizontal) < 0.01f;

			if (noMove) {
				return;
			}
			
			transform.rotation = CalculateNewRotation ();

			animator.SetTrigger ("Hop forward");
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
	}
}