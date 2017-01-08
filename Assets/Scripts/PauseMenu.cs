using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public string mainMenu;
	public bool isPaused;
	public GameObject pauseCanvas;

	void Update() {
		if (isPaused) {
			pauseCanvas.SetActive (true);    // 啟動暫停方塊
			Time.timeScale = 0f;    // 停止遊戲
		} else {
			pauseCanvas.SetActive (false);
			Time.timeScale = 1f;    // 遊戲啟動
		}
		// 暫停偵測
		if ( Input.GetButtonDown("Submit") ) {
			isPaused = !isPaused;
		}
	}

	public void Resume() {
		// 返回遊戲
		isPaused = false;
	}

	public void GoToMainMenu() {
		// 去 Main Menu
		Application.LoadLevel(mainMenu);
	}
}
