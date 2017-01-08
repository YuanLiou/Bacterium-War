using UnityEngine;
using System.Collections;

public class DestoryOverTime : MonoBehaviour {

	public float lifetime;
	public PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if ( lifetime < 0 ) {
			Destroy (gameObject);
			player.GetComponent<PlayerController> ().DecreaseBulletCounter ();
		} 
	}
}
