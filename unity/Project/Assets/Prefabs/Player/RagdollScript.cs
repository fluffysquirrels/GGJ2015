using UnityEngine;
using System.Collections;

//A very simple script to show how to turn a Mecanim character into a ragdoll
//when the user presses space, assuming that the Unity Ragdoll Wizard has
//been used to add the ragdoll RigidBody components

namespace Ggj.Player {
	public class RagdollScript : MonoBehaviour {

		private float timerEnd = 5f;
		private float timer = 0f;
		public bool ragdoll = false;
		
		//Helper to set the isKinematic property of all RigidBodies in the children of the 
		//game object that this script is attached to
		void SetKinematic(bool newValue)
		{
			//Get an array of components that are of type Rigidbody
			Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
			
			//For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
			foreach (Rigidbody rb in bodies)
			{
				rb.isKinematic=newValue;
			}
			
			//Don't want to do this on root game object
			Rigidbody self = GetComponent<Rigidbody>();
			self.isKinematic = true;
		}

		void Start () {
			//Set all RigidBodies to kinematic so that they can be controlled with Mecanim
			//and there will be no glitches when transitioning to a ragdoll
			SetKinematic(true);
		}
		
		void Update () 
		{
			SetKinematic( !ragdoll );
			GetComponent<Animator>().enabled = !ragdoll;
		}
	}
}