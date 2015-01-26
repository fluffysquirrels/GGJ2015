using UnityEngine;
using System.Collections;

namespace Ggj.Scripts {
    public class Projectile : MonoBehaviour {

        void OnTriggerEnter(Collider other) {
            if (other.GetComponent<StopsProjectiles> () != null) {
                Object.Destroy (this.gameObject);
            }
        }
    }
}