using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	public Text textComp;
	private float TimerEnd = 8f;
	private float Timer = 0f;

	// Use this for initialization
	void Awake () 
	{
		textComp = GetComponent<Text>();
		textComp.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void StartTimer() {
	
	}
}
