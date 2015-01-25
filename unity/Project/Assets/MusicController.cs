using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public AudioSource Ramp;
    public AudioSource Loop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Loop.isPlaying && !Ramp.isPlaying) {
            Loop.Play ();
        }
	}

    public void PlayRamp() {
        Ramp.Play ();
        Loop.Stop ();
    }
}
