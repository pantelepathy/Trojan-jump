using UnityEngine;
using System.Collections;

public class LeftRight2D : MonoBehaviour {

	float platformSpeed = 2f;
	bool endPoint;
	
	// Update is called once per frame
	void Update () 
	{
		if (endPoint) {
			transform.position += Vector3.right * platformSpeed * Time.deltaTime;
		} else {
			transform.position -= Vector3.right * platformSpeed * Time.deltaTime;
		}

		if (transform.position.x >= 2.33) {
			endPoint = false ;
		}
		if (transform.position.x <= -2.33) {
			endPoint = true;
		}
	}
}
