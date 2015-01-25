using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
	public class MainTrigger : MonoBehaviour {
	
		private Animator anim;
		private float TimerEnd = 10f;
		private float TimerCurrent = 0f;
		private bool StartTimer = false;
		
		// Use this for initialization
		void Start () 
		{
			anim = GetComponentInChildren<Animator>();
		}
		
		// Update is called once per frame
		void Update () 
		{
			if ( StartTimer )
			{
				TimerCurrent += Time.deltaTime;
			}
		
			if ( TimerCurrent >= TimerEnd )
			{
				anim.SetTrigger("ShouldAttack");
				TimerCurrent = 0f;
				StartTimer = false;
			}
		}
		
		void OnTriggerEnter(Collider c) {
			var playerBehaviour = c.GetComponent<PlayerMove> ();
			
			if (playerBehaviour == null) {
				// Only interested in colliding with player.
				return;
			}
			LetsStartTimer();
		}
		
		void LetsStartTimer()
		{
			StartTimer = true;
		}
	}
}
