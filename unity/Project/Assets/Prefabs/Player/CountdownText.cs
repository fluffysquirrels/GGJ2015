using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour {

    public MusicController Music;
    private Text textComp;
	
	// Use this for initialization
	void Awake () 
	{
		textComp = GetComponent<Text>();
		textComp.enabled = false;
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
        yield return new WaitForSeconds (1f);
        textComp.enabled = false;
    }

    public void PlayerDead ()
    {
        textComp.text = "You are dead\n" + "Press 'R' to be butchered again";
        textComp.enabled = true;
    }

}
