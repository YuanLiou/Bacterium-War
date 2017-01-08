using UnityEngine;
using System.Collections;

public class HurtEnemyOnContact : MonoBehaviour {

	public int attackPoint;
	public float bounce;
	private Rigidbody2D bdy;
	public bool hasParent;
	// Use this for initialization
	void Start () {
		if (hasParent)
			bdy = transform.parent.GetComponent<Rigidbody2D> ();    // 找到 parent 的東西
		else
			bdy = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyHealthManager> ().GiveDamage (attackPoint);
			bdy.velocity = new Vector2 (bdy.velocity.x, bounce);
		} else if (transform.name == "Sword" && other.tag == "BossWeakPoint") {
			// Boss Fight!
			Debug.Log("碰撞");
			other.transform.parent.GetComponent<BossHealthManager> ().GiveDamage (attackPoint);
			//other.transform.parent.GetComponent<BossHealthManager> ().Flash (5f, 0.05f);    // 閃亮
			other.GetComponent<AudioSource> ().Play ();

			bdy.velocity = new Vector2 (bdy.velocity.x, bounce);
		}
	}
}
