using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class AcidBullet : MonoBehaviour {

        public float Speed;

    	void Start () {
    	
    	}
    	
    	void Update () {
            var newPos = transform.position;
            newPos.z += Time.deltaTime * Speed;
            transform.position = newPos;

            // TODO: Delete when out of bounds.
    	}

        void OnTriggerEnter(Collider c) {
            Debug.Log ("AcidBullet.OnTriggerEnter");
            var playerBehaviour = c.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            playerBehaviour.Kill ();
        }
    }
}