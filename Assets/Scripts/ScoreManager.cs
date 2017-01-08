using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score;    // 分數
	Text score_ui;

	void Start() {
		score_ui = GetComponent<Text> ();
		score = 0;
	}

	void Update() {
		if (score < 0)
			score = 0;

		string scoreText = string.Format ("{0:000000}", score);
		score_ui.text = scoreText;
	}

	// 分數處理
	public static void AddPoint(int point) {
		score += point;
	}

	public static void ResetPoint() {
		score = 0;
	}

}
