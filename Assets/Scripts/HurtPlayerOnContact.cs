using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {
	public int attackPoint;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ( other.name == "Player" ) {
			other.GetComponent<AudioSource> ().Play ();
			HealthManager.HurtPlayer (attackPoint);

			var player = other.GetComponent<PlayerController> ();
			// 往後彈
			if (HealthManager.playerHealth > 0 )
				player.knockbackCount = player.knockbackLength;

			if (other.transform.position.x < transform.position.x)
				player.knockFromRight = true;
			else 
				player.knockFromRight = false;
		}
	}
}
