using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {

	public void LoadLevel(string level)
	{
		Application.LoadLevel(level);
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
	
}
