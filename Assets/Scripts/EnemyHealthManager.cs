using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
	public int enemyHealth;    // 血量
	public GameObject enemyDeathEffect;
	public int enemyPoint;    // 分數
	private EnemySpawner enemySpawner;

	// Use this for initialization
	void Start () {
		enemySpawner = FindObjectOfType<EnemySpawner> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( enemyHealth <= 0) {
			// 怪物死亡
			Instantiate(enemyDeathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoint (enemyPoint);    // 加分
			Destroy(gameObject);
			EnemySpawner.AddBeatCount ();    // 增加殺敵數
			enemySpawner.DecreaseEnemyCount();    // 減少總數
		}
	}

	public void GiveDamage(int damage) {
		enemyHealth -= damage;
		GetComponent<AudioSource> ().Play ();    // 播放聲音
	}
}
