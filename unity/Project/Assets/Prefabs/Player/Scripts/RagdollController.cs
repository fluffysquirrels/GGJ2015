using UnityEngine;
using System.Collections;

public class RagdollController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Process commands
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.LoadLevel("MainMenu");
		}
		if (Input.GetAxis ("Restart") > 0.01f) 
		{
			RestartLevel ();
		}
	}

	void RestartLevel () {
		Application.LoadLevel (Application.loadedLevel);
	}
}

