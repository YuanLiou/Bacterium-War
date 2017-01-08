using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	//public string levelSelect;

	public void StartNewGame() {
		Application.LoadLevel (startLevel);
	}

	public void QuitGame() {
		Debug.Log ("離開遊戲");
		Application.Quit ();
	}
}
