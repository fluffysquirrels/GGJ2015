﻿using UnityEngine;
using System;

namespace Ggj.Prefabs {
    public class DeadlyCapsule : MonoBehaviour {

    	// Use this for initialization
    	void Start () {
    	
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        void OnTriggerEnter(Collider c) {
            var playerBehaviour = c.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            playerBehaviour.Kill ();
        }
    }
}