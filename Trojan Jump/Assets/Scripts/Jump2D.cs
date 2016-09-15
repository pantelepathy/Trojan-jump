using UnityEngine;
using System.Collections;

public class Jump2D : MonoBehaviour {

	public bool grounded;			// true or false based on where we are grounded
	public float jumpHeight = 500f; // player jump height

	public Transform groundcheck;	// object to check if grounded

	public float groundRadius = .2f;
	public LayerMask ground;		// decide which layers count as ground

	
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundcheck.position, groundRadius, ground);

		float velY = GetComponent<Rigidbody2D>().velocity.y;

		if (grounded && velY <= 0) 
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpHeight));
		}
	}
}
