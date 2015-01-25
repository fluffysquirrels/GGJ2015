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
            newPos += transform.forward * Time.deltaTime * Speed;
            transform.position = newPos;

            var isOutOfBounds = newPos.magnitude > 100;
            if (isOutOfBounds) {
                Object.Destroy (this.gameObject);
            }
    	}

        void OnTriggerEnter(Collider c) {
            var playerBehaviour = c.GetComponent<PlayerMove> ();

            if (playerBehaviour == null) {
                // Only interested in colliding with player.
                return;
            }

            playerBehaviour.Kill ();
            Object.Destroy (this.gameObject);
        }
    }
}