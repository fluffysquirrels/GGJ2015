using UnityEngine;
using System.Collections;
using Ggj.Player;

namespace Ggj.Prefabs {
    public class AcidBullet : MonoBehaviour {

        public float Speed;

    	void Update () {
            var newPos = transform.position;
            newPos += transform.forward * Time.deltaTime * Speed;
            transform.position = newPos;

            var isOutOfBounds = newPos.magnitude > 100;
            if (isOutOfBounds) {
                Object.Destroy (this.gameObject);
            }
    	}
    }
}