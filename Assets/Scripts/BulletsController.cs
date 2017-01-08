using UnityEngine;
using System.Collections;

public class BulletsController : MonoBehaviour {
	public float speed;
	private Rigidbody2D bdy;
	private PlayerController player;
	public GameObject enemyDestroyEffect;
	public GameObject impactEffect;
	public GameObject impactWall;
	private float rotationSpeed;
	public int attackPoint;    // 攻擊力
	// Use this for initialization
	void Start () {
		Instantiate (impactEffect, transform.position, transform.rotation);    // 爆花
		bdy = GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<PlayerController> ();
		rotationSpeed = -360;    // 旋轉用
		// 向左
		if ( player.transform.localScale.x < 0) {
			speed = -speed;
			rotationSpeed = -rotationSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		bdy.velocity = new Vector2 (speed, bdy.velocity.y);
		bdy.angularVelocity = rotationSpeed;
		//transform.RotateAround (Vector3.zero, Vector3.forward, 20 * Time.deltaTime);    // 旋轉
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			//Instantiate (enemyDestroyEffect, other.transform.position, other.transform.rotation);
			//Destroy (other.gameObject);
			other.GetComponent<EnemyHealthManager>().GiveDamage(attackPoint);    // 受攻擊
			Instantiate (impactEffect, transform.position, transform.rotation);    // 爆花
			Destroy (gameObject);
			player.DecreaseBulletCounter ();    // 減少子彈限制
		} else if ( other.tag == "Ground" || other.tag == "MovingPlatform" ) {
			Instantiate (impactWall, transform.position, transform.rotation);    // 爆花
			Destroy (gameObject);
			player.DecreaseBulletCounter ();    // 減少子彈限制
		} else if ( other.tag == "Boss" ) {
			Instantiate (impactEffect, transform.position, transform.rotation);    // 爆花
			int bounce = Random.Range(-8, 8);
			speed = -(speed + 10);    // 轉向
			bdy.velocity = new Vector2 (bdy.velocity.x, bdy.velocity.y + bounce);
			other.GetComponent<AudioSource> ().Play ();
		}

	}
}
