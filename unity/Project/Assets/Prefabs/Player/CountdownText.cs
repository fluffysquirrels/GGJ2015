using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour {

    public MusicController Music;
    private Text textComp;
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

    public void StartCountdown() {
        textComp.enabled = true;
        StartCoroutine (CountdownCoroutine ());
        Music.PlayRamp ();
    }

    public IEnumerator CountdownCoroutine() {
        for (int count = 10; count >= 1; count--) {
            textComp.text = count.ToString ();
            yield return new WaitForSeconds (1f);
        }

        textComp.text = "NOW!";
    }
}
