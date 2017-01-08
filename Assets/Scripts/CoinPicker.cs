using UnityEngine;
using System.Collections;

public class CoinPicker : MonoBehaviour {

	public int point;

	public AudioSource coinSound;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<PlayerController> () == null)
			return;

		ScoreManager.AddPoint (point);
		coinSound.Play ();
		Destroy (gameObject);
	}
}
