using UnityEngine;
using System.Collections;

public class JumpPlat2D : MonoBehaviour {

	public float jumpHeight = 500;
	float velY;

	// Update is called once per frame
	void Update () {
		velY = GetComponent<Rigidbody2D>().velocity.y;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "JumpPlat" && velY <= 0) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpHeight ));
		}
	}
}
