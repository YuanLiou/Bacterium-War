using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject currentSavePoint;
	private PlayerController player;

	public GameObject deathPaticle;
	public GameObject respawnPaticle;
	public int pointPenaltyOnDeath;
	public float respawnDelay;

	public HealthManager healthManager;
	private LifeManager lifeManager;

	//private float userGravity;    // 儲存重力狀態

	private CameraController myCamera;
	private EnemySpawner enemySpawner;

	public GameObject bgm;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		myCamera = FindObjectOfType<CameraController> ();
		healthManager = FindObjectOfType<HealthManager> ();
		lifeManager = FindObjectOfType<LifeManager> ();
		enemySpawner = GetComponent<EnemySpawner> ();
		enemySpawner.StartEnemySpawn ();    // 開始生怪
		bgm.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 給使用者重生
	public void RespawnPlayer() {
			StartCoroutine ("RespawnPlayerCo");
	}

	// 重生 CoRutune 為了使用 WaitForSec
	public IEnumerator RespawnPlayerCo() {
		player.enabled = false;    // 先讓玩家消失
		Instantiate (deathPaticle, player.transform.position, player.transform.rotation);    // 死亡粒子特效
		myCamera.isFollowing = false;
		//player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;    // 死亡時將加速度移除
		player.GetComponent<Renderer> ().enabled = false;    // 先讓玩家消失
		//userGravity = player.GetComponent<Rigidbody2D>().gravityScale;
		//player.GetComponent<Rigidbody2D>().gravityScale = 0f;	// 清除重力
		//ScoreManager.AddPoint(-pointPenaltyOnDeath);    // 死亡處罰
		yield return new WaitForSeconds (respawnDelay);    // 延遲
		if (!lifeManager.gameOver) {
			Debug.Log ("Player Respawm: 玩家重生");
			healthManager.FullHealth();    // 補滿血
			player.transform.position = currentSavePoint.transform.position;    // 移動位置
			player.enabled = true;    // 啟動玩家
			player.GetComponent<Renderer> ().enabled = true;    // 啟動玩家
			myCamera.isFollowing = true;
			healthManager.isDead = false;
			//player.GetComponent<Rigidbody2D>().gravityScale = userGravity;	// 賦予重力
			//player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;    // 將加速度移除
			Instantiate(respawnPaticle, currentSavePoint.transform.position, currentSavePoint.transform.rotation);    // 重生粒子特效
		} else {
			// GameOver
			enemySpawner.StopEnemySpawn();    // 停止生怪
			bgm.GetComponent<AudioSource>().Stop();
			Time.timeScale = 0f;    // 停止時間
		}
	}
}
