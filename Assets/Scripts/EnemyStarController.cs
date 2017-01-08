using UnityEngine;
using System.Collections;

public class EnemyStarController : MonoBehaviour {
	public float speed;
	private Rigidbody2D bdy;
	private PlayerController player;
	public GameObject impactEffect;
	public GameObject impactWall;
	private float rotationSpeed;
	public int attackPoint;    // 攻擊力
	// Use this for initialization
	void Start () {
		//Instantiate (impactEffect, transform.position, transform.rotation);    // 爆花
		bdy = GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<PlayerController> ();
		rotationSpeed = -360;    // 旋轉用
		// 向左，偵測玩家位置
		if ( player.transform.position.x < transform.position.x ) {
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
		if (other.tag == "Player") {
			other.GetComponent<AudioSource> ().Play ();    // 損血聲
			HealthManager.HurtPlayer (attackPoint);
			Instantiate (impactEffect, transform.position, transform.rotation);    // 爆花
			Destroy (gameObject);
		} else if ( other.tag == "Ground" || other.tag == "MovingPlatform" ) {
			Instantiate (impactWall, transform.position, transform.rotation);    // 爆花
			Destroy (gameObject);
		}

	}
}
