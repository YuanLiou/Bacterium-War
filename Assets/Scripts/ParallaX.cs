using UnityEngine;
using System.Collections;

public class ParallaX : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxXScale;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCameraPostion;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		previousCameraPostion = cam.position;
		parallaxXScale = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxXScale [i] = backgrounds [i].position.z * -1;
		}

	}
	
	// 在 Update 結束後 Call LateUpdate()
	void LateUpdate () {
		for (int i = 0; i<backgrounds.Length; i++) {
			float pararellX = (previousCameraPostion.x - cam.position.x) * parallaxXScale[i];
			float bgTargerPositionX = backgrounds [i].position.x + pararellX;
			Vector3 backgroundTargetPos = new Vector3 (bgTargerPositionX, backgrounds [i].position.y, backgrounds [i].position.z);
			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);    // 背景圖層移動
		}
		previousCameraPostion = cam.position;
	}
}
