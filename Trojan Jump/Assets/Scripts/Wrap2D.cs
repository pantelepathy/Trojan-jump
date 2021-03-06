﻿using UnityEngine;
using System.Collections;

public class Wrap2D : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () 
	{
		// if player's position exceeds the left bound
		if (transform.position.x <= -3.07f) 
		{
			transform.position = new Vector3 (3.07f, transform.position.y, transform.position.z);
		}
		// if player's position exceeds the right bound
		else if (transform.position.x >= 3.07f) 
		{
			transform.position = new Vector3 (-3.07f, transform.position.y, transform.position.z);
		}
	}
}
