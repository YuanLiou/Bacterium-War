using UnityEngine;
using System.Collections;

public class ShootPlayerAtRange : MonoBehaviour {

	public float playerRange;
	public GameObject enemyBullet;
	private PlayerController player;
	public Transform launchPoint;
	public float waitForShoot;
	private float shootCounter;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		shootCounter = waitForShoot;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (new Vector3 (transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3 (transform.position.x + playerRange, transform.position.y, transform.position.z));
		shootCounter -= Time.deltaTime;

		if ( transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange && shootCounter < 0 ) {
			Instantiate (enemyBullet, launchPoint.position, launchPoint.rotation);    // 發射子彈
			shootCounter = waitForShoot;
		}

		if ( transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange && shootCounter < 0 ) {
			Instantiate (enemyBullet, launchPoint.position, launchPoint.rotation);    // 發射子彈
			shootCounter = waitForShoot;
		}
	}
}
