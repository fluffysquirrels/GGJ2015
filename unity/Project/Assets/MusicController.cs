using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public AudioSource Ramp;
    public AudioSource Loop;

    public AudioSource EffectsSource;

    private bool Stopped = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Loop.isPlaying && !Ramp.isPlaying && !Stopped) {
            Loop.Play ();
        }
	}

    public void PlayRamp() {
        Ramp.Play ();
        Loop.Stop ();
    }

    public void StopMusic() {
        Stopped = true;
        Ramp.Stop ();
        Loop.Stop ();
    }

    public void StartMusic() {
        Stopped = false;
    }
}
