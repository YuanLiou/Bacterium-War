using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

	public int startLife;    // 接關次數
	private int lifeCounter;
	private Text lifeUIText;
	public Text scoreUIText;
	public GameObject gameOverScreen;    // GameOver 畫面
	public PlayerController player;
	public string mainMenu;    // 主選單場景
	public float waitAfterPlayerDead;
	public bool gameOver;

	// Use this for initialization
	void Start () {
		lifeUIText = GetComponent<Text> ();
		scoreUIText.GetComponent<Text> ();
		lifeCounter = startLife;
		player = FindObjectOfType<PlayerController> ();
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ( lifeCounter <= 0 ) {
			lifeCounter = 0;
			scoreUIText.text = string.Format ("{0:000000}", ScoreManager.score);
			endTheGame ();
		}
		lifeUIText.text = "x  " + lifeCounter;
		if ( gameOverScreen.activeSelf ) {
			waitAfterPlayerDead -= Time.deltaTime;    // 等待一點時間
		}
		if ( waitAfterPlayerDead < 0 ) {
			Application.LoadLevel (mainMenu);    // 回到主選單
		}
	}

	// 加命 
	public void addLife() {
		lifeCounter++;
	}

	// 減命
	public void TakeLife() {
		lifeCounter--;
	}

	public void endTheGame() {
		gameOverScreen.SetActive (true);
		player.gameObject.SetActive (false);    // 關閉 Player
		gameOver = true;
	}


}
