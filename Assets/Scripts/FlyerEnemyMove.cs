using UnityEngine;
using System.Collections;

public class FlyerEnemyMove : MonoBehaviour {
	
	public PlayerController player;
	public float moveSpeed;
	public float playerRange;    // 瞄準用
	public LayerMask playerLayer;
	private bool playerInRange;
	public bool followOnLook;
	private bool facingAway;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
		if ( !followOnLook ) {
			// 朝著 Player 而去
			if ( playerInRange ) {
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
				return;
			}	
		}
		
		
		if (( player.transform.position.x < transform.position.x && player.transform.localScale.x < 0 ) ||
		    ( player.transform.position.x > transform.position.x && player.transform.localScale.x > 0 ) ) {
			// Player 在左邊，往左看|| Player 在右邊，往右看
			facingAway = true;
		} else {
			facingAway = false;
		}
		
		// 朝著 Player 而去
		if ( playerInRange && facingAway ) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			return;
		}	
		
	}
	
	// Debug use
	void OnDrawGizmosSelected() {
		Gizmos.DrawSphere(transform.position, playerRange);
	}
}
