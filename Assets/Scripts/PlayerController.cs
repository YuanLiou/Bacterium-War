using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;
	private float moveVelocity;
	// 地板偵測
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool isOnGrounded;
	private bool doubleJumped;     // 二段跳
	// Animation
	private Animator anim;
	private Rigidbody2D bdy;
	// Bullets
	public Transform firePosition;
	public GameObject bullets;
	public int bulletLimit;
	private int bulletCounter;
	// Knock back
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	private bool attacking;
	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		bdy = GetComponent<Rigidbody2D> ();
		bulletCounter = 0;
		attacking = false;
		gravityStore = bdy.gravityScale;    // 先將原來角色的重力設定存下來
	}

	// For RigidBody Physics Effect Use
	void FixedUpdate () {
		isOnGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
	}
	
	// Update is called once per frame
	void Update () {
		if (isOnGrounded)
			doubleJumped = false;
		anim.SetBool ("Grounded", isOnGrounded);
		//moveVelocity = 0f;    // 每畫格煞車
		// 移動=============================
		moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");
		// ================================
		if ( knockbackCount <= 0 ) {
			bdy.velocity = new Vector2(moveVelocity, bdy.velocity.y );    // 移動	
		} else {
			// 從右邊撞過來
			if (knockFromRight)
				bdy.velocity = new Vector2 (-knockback, knockback);
			else
				bdy.velocity = new Vector2 (knockback, knockback);
			knockbackCount -= Time.deltaTime;
		}
		// 跳躍
		if ( Input.GetButtonDown("Jump") && isOnGrounded ) {
			bdy.velocity = new Vector2(bdy.velocity.x , jumpHeight);
		}
		// 二段跳
		if ( Input.GetButtonDown("Jump") && !isOnGrounded && !doubleJumped ) {
			// 跳躍
			bdy.velocity = new Vector2(bdy.velocity.x , jumpHeight);
			doubleJumped = true;
		}

		anim.SetFloat ("Speed", Mathf.Abs(bdy.velocity.x) );
		// 控制 Script 轉向
		if (bdy.velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (bdy.velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);

		// 發射子彈
		if ( Input.GetButtonDown("Bullet") && bulletCounter < bulletLimit ) {
			bulletCounter++;
			Instantiate (bullets, firePosition.position, firePosition.rotation);
		}

		// 劍
		if (anim.GetBool ("Sword"))
			anim.SetBool ("Sword", false);    // 避免動畫一直持續
		if ( Input.GetButtonDown("Sword") && !attacking ) {
			anim.SetBool ("Sword", true);
			attacking = true;
		}

		// 爬梯子
		if (onLadder) {
			bdy.gravityScale = 0f;
			climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");
			bdy.velocity = new Vector2 (bdy.velocity.x, climbVelocity);
		} else {
			bdy.gravityScale = gravityStore;    // 還原重力
		}
	}

	public void DecreaseBulletCounter() {
		bulletCounter--;
	}

	public void StopSwordAnim() {
		attacking = false;
	}
	// 隨著移動平台移動
	void OnCollisionEnter2D(Collision2D other) {
		if ( other.transform.tag == "MovingPlatform" ) {
			Debug.Log("Collision 觸發");
			transform.parent = other.transform;    // 將 Player 作為移動平台的 Child
		}
	}
	// 離開移動平台時，不隨著移動
	void OnCollisionExit2D(Collision2D other) {
		if ( other.transform.tag == "MovingPlatform" ) {
			Debug.Log("Collision 觸發");
			transform.parent = null;    // 將 Player 作為移動平台的 Child
		}
	}

}
