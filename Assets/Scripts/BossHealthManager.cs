using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour {
	public int enemyHealth;    // 血量
	public GameObject enemyDeathEffect;
	public int enemyPoint;    // 分數
	public Text scoreUIText;
	public GameObject clearScreen;    // GameOver 畫面
	//public PlayerController player;
	public string mainMenu;    // 主選單場景
	public float waitAfterPlayerClear;
	private LifeManager lifemanager;
	private bool bossDead;

	// Use this for initialization
	void Start () {
		scoreUIText.GetComponent<Text> ();
		//player = FindObjectOfType<PlayerController> ();
		lifemanager = FindObjectOfType<LifeManager> ();
		bossDead = false;
	}

	// Update is called once per frame
	void Update () {
		if ( enemyHealth <= 0 && !bossDead ) {
			// Boss 死亡
			bossDead = true;
			Instantiate(enemyDeathEffect, transform.position, transform.rotation);
			GetComponent<SpriteRenderer> ().enabled = false;    // 消失
			GetComponent<HurtPlayerOnContact>().enabled = false;    // 減血裝置取消
			GetComponent<ShootPlayerAtRange>().enabled = false;    // 射人取消
			GetComponent<BoxCollider2D>().isTrigger = false;
			ScoreManager.AddPoint (enemyPoint);    // 加分
			scoreUIText.text = string.Format ("{0:000000}", ScoreManager.score);
			clearTheGame ();
		}
		if ( clearScreen.activeSelf ) {
			waitAfterPlayerClear -= Time.deltaTime;    // 等待一點時間
		}
		if ( waitAfterPlayerClear < 0 ) {
			Destroy(gameObject);
			Application.LoadLevel (mainMenu);    // 回到主選單
		}
	}

	public void GiveDamage(int damage) {
		enemyHealth -= damage;
		GetComponent<AudioSource> ().Play ();    // 播放聲音
	}

	void clearTheGame() {
		clearScreen.SetActive (true);
		//player.gameObject.SetActive (false);    // 關閉 Player
		lifemanager.gameOver = true;    // 結束遊戲
		Time.timeScale = 0f;    // 停止遊戲
	}

}
