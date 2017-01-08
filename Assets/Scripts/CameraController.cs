using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public PlayerController player;
	public bool isFollowing;    // 攝影機跟隨

	public float xOffset;
	public float yOffset;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () {
		// z 為了讓旋轉固定故用原來攝影機自己的
		if (isFollowing)
			transform.position = new Vector3 (player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
	}
}
