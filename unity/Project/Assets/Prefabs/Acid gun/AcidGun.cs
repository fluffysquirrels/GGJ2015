using UnityEngine;
using System.Collections;

namespace Ggj.Prefabs {
    public class AcidGun : MonoBehaviour {

        public float FireIntervalSeconds;
        public float InitialFireDelaySeconds;
        public GameObject BulletPrefab;

    	void Start () {
            this.InvokeRepeating ("OnFireTimer", InitialFireDelaySeconds, FireIntervalSeconds);
    	}
    	
        public void OnFireTimer() {
            Instantiate (BulletPrefab, transform.position, transform.rotation);
        }

    	void Update () {
    	
    	}
    }
}