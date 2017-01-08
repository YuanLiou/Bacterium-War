using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public float startingTime;
	private float countingTime;
	private Text timerText;
	private PauseMenu pauseMenu;
	private LifeManager lifeManager;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		pauseMenu = FindObjectOfType<PauseMenu> ();
		lifeManager = FindObjectOfType<LifeManager> ();
		countingTime = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
		if ( pauseMenu.isPaused || lifeManager.gameOver )
			return;

		countingTime -= Time.deltaTime;
		if ( countingTime <= 0 ) {
			countingTime = 0;
			lifeManager.endTheGame ();    // 結束遊戲
		}
		timerText.text = Mathf.Round(countingTime).ToString ();    // Mathf.Round 四捨五入
	}

	public void ResetTime() {
		countingTime = startingTime;
	}
}
