using UnityEngine;
using System;

namespace Ggj {
    public class DeadlyCapsule : MonoBehaviour {

    	// Use this for initialization
    	void Start () {
    	
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        void OnTriggerEnter(Collider c) {
            //var c = collision.collider;

            var playerBehaviour = c.GetComponent<Player.PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            playerBehaviour.Kill ();

            Debug.Log (String.Format (
                "Deadly capsule collision! " +
                "collider-type={0} " +
                "collider-name={1} " +
                "collider-tag={2} " +
                "num-PlayerMove-components={3}",
                c.GetType (),
                c.name,
                c.tag,
                c.GetComponents<Player.PlayerMove> ().Length));
        }
    }
}