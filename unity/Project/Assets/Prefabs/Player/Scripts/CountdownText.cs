﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour {

    public MusicController Music;
    public int CountDownTime = 10;
    private Text textComp;
	
	void Awake () 
	{
		textComp = GetComponent<Text>();
		textComp.enabled = false;
	}

    public void StartCountdown() {
        textComp.enabled = true;
        StartCoroutine ("CountdownCoroutine");
        Music.PlayRamp ();
    }

    public IEnumerator CountdownCoroutine() {
        for (int count = CountDownTime; count >= 1; count--) {
            textComp.text = count.ToString ();
            yield return new WaitForSeconds (1f);
        }

        textComp.text = "NOW!";
        yield return new WaitForSeconds (1f);
        textComp.enabled = false;
    }

    public void StopCountdown ()
    {
        StopCoroutine ("CountdownCoroutine");
        textComp.enabled = false;
    }

    public void PlayerDead ()
    {
        textComp.text = "You are dead\n" + "Press 'R' to be butchered again";
        textComp.enabled = true;
    }

}
