using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	public GameObject platform;
	public float movingSpeed;
	public Transform currentPoint;
	public Transform[] point;
	public int pointSelection;

	// Use this for initialization
	void Start () {
		currentPoint = point[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
		platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, movingSpeed * Time.deltaTime);
		// 到底的處理，換方向
		if ( platform.transform.position == currentPoint.position ) {
			pointSelection++;
			if ( pointSelection == point.Length ) {
				pointSelection = 0;
			}
		}
		currentPoint = point[pointSelection];
	}
}
