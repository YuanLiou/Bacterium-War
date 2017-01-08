using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
	public static int playerHealth;    // 血量
	public int maxPlayerHealth;    // 最大血量
	//Text life_ui;
	public Slider healthBar;
	private LevelManager levelManager;
	public bool isDead;
	private LifeManager lifeSystem;    // 命

	// Use this for initialization
	void Start () {
		//life_ui = GetComponent<Text> ();
		healthBar = GetComponent<Slider>();
		playerHealth = maxPlayerHealth;
		levelManager = FindObjectOfType<LevelManager> ();
		isDead = false;
		lifeSystem = FindObjectOfType<LifeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0 && !isDead) {
			levelManager.RespawnPlayer ();
			isDead = true;
			playerHealth = 0;
			lifeSystem.TakeLife ();    // 減一條命
		}

		//life_ui.text = playerHealth.ToString();
		healthBar.value = playerHealth;
	}

	public static void HurtPlayer(int damage) {
		if (playerHealth - damage < 0)
			playerHealth = 0;
		else
			playerHealth -= damage;
	}

	// 補滿血
	public void FullHealth() {
		playerHealth = maxPlayerHealth;
	}
}
