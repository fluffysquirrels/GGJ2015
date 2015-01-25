using UnityEngine;
using System.Collections;

namespace Ggj.Prefabs {
    public class AcidGun : MonoBehaviour {

        public float FireIntervalSeconds;
        public GameObject BulletPrefab;

    	void Start () {
            this.InvokeRepeating ("OnFireTimer", FireIntervalSeconds, FireIntervalSeconds);
    	}
    	
        public void OnFireTimer() {
            Instantiate (BulletPrefab, transform.position, transform.rotation);
        }

    	void Update () {
    	
    	}
    }
}