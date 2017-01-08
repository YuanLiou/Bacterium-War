using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] spawnPoint;
	public Transform[] flyerPoint;
	public GameObject[] enemy;

	public float maxSpawnRateInSec;
	//private float originalRate;
	private static int beatCount;
	public int maxSpawnCount;
	private int nowSpawnCount;
	public int totalSpawn;    // 生怪總數
	private int spawnCounter;    // UI 使用
	private int spawnTotalCount;    // 生怪總數計算
	public GameObject boss;
	public GameObject bgm;
	public GameObject bossFight;
	private bool isFighting;

	public Slider ipSlider;
	public Text ipText;

	// Use this for initialization
	void Start () {
		beatCount = 0;
		nowSpawnCount = 3;
		//originalRate = maxSpawnRateInSec;
		//ipSlider = GetComponent<Slider>();
		//ipText = GetComponent<Text> ();
		spawnCounter = totalSpawn;
		spawnTotalCount = 0;
		isFighting = false;
	}
	
	// Update is called once per frame
	void Update () {
		ipSlider.GetComponent<Slider>().value = spawnCounter;    // 感染條
		float ipPercent = (float) spawnCounter / totalSpawn;
		//Debug.Log ("Log:" + ipPercent);
		//string ipPercentString = ipPercent.ToString("P1");
		ipText.GetComponent<Text>().text = ipPercent.ToString("P0");
		if ( beatCount == totalSpawn+2 && !isFighting ) {
			bgm.GetComponent<AudioSource> ().Pause ();
			bossFight.GetComponent<AudioSource> ().Play ();
			Invoke ("startBossFight", 4f);
			isFighting = true;
		}
	}

	// 生產怪物
	void SpawnEnemy() {
		// 到達最大值
		if ( spawnTotalCount >= totalSpawn ) {
			Debug.Log ("停止生怪！");
			StopEnemySpawn ();    // 停止生怪
			return;
		}
		if ( nowSpawnCount < maxSpawnCount ) {
			int randomNum = Random.Range(0, 100);
			int enemyIndex;
			if ( randomNum < 25 ) {
				enemyIndex = 1;
			} else {
				enemyIndex = 0;
			}
			GameObject new_enemy = (GameObject) Instantiate(enemy[enemyIndex]);
			float x, y;
			if ( enemyIndex == 0 ) {
				int enemyPoint = Random.Range (0, spawnPoint.Length);    // 幾個生怪點
				x = spawnPoint [enemyPoint].transform.position.x;
				y = spawnPoint [enemyPoint].transform.position.y;	
			} else {
				int enemyPoint2 = Random.Range (0, flyerPoint.Length);    // 幾個生怪點
				x = flyerPoint [enemyPoint2].position.x;
				y = flyerPoint [enemyPoint2].position.y;	
			}
			
			new_enemy.transform.position = new Vector2 (x, y);
			nowSpawnCount++;
			spawnTotalCount++;
		}
		NextSpawnSheduler ();
	}

	// 排程
	void NextSpawnSheduler() {
		float spawnInNSec;
		if ( maxSpawnRateInSec > 1f ) {
			// 隨機生怪秒數
			spawnInNSec = Random.Range(1f, maxSpawnRateInSec);
		} else {
			spawnInNSec = 1f;
		}
		Invoke ("SpawnEnemy", spawnInNSec);    // n秒執行SpawnEnemy
	}

	// 難度強化器
	void IncreseSpawnRate() {
		if (maxSpawnRateInSec > 1f)
			maxSpawnRateInSec--;

		if (maxSpawnRateInSec == 1f)
			CancelInvoke ("IncreseSpawnRate");    // 剩一秒就算了
	}

	// Public method of 生成
	public void StartEnemySpawn() {
		//maxSpawnRateInSec = originalRate;
		Invoke ("SpawnEnemy", maxSpawnRateInSec);
		InvokeRepeating ("IncreseSpawnRate", 0f, 30f);
	}

	// 停止生怪
	public void StopEnemySpawn() {
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreseSpawnRate");
	}

	public static void AddBeatCount() {
		beatCount++;
	}

	public void DecreaseEnemyCount() {
		nowSpawnCount--;
		if ( spawnCounter != 0 )
			spawnCounter--;
	}

	void startBossFight() {
		boss.SetActive(true);
	}
}
