using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public float moveSpeed;
	public bool moveToRight;

	private Rigidbody2D bdy;

	// 牆壁偵測
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask groundLayer;
	private bool touchedTheWall;

	// 邊緣偵測
	public Transform edgeCheck;
	private bool isOnGround;

	// Use this for initialization
	void Start () {
		bdy = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		touchedTheWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, groundLayer);
		isOnGround = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, groundLayer);
	}
	
	// Update is called once per frame
	void Update () {
		if ( touchedTheWall || !isOnGround )
			moveToRight = !moveToRight;    // 碰到牆壁反轉數值

		if ( moveToRight ) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);    // 翻轉用
			bdy.velocity = new Vector2 (moveSpeed, bdy.velocity.y);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);    // 翻轉用
			bdy.velocity = new Vector2 (-moveSpeed, bdy.velocity.y);
		}
	}
}
