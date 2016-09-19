using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	Transform player;
	float playerHeightY;

	public Transform regular;
	public Transform jump;
	public Transform LeftRight;
	public Transform UpDown;

	private int platNumber;

	private float platCheck;
	private float spawnPlatformsTo = 1.5f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

//		PlatformSpawner (1.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerHeightY = player.position.y;

		if (playerHeightY > platCheck) {
			platformManager ();
		}

		float currentCameraHeight = transform.position.y;

		float newHeightOfCamera = Mathf.Lerp (currentCameraHeight, playerHeightY, Time.deltaTime * 10);

		if (playerHeightY > currentCameraHeight) {
			transform.position = new Vector3 (transform.position.x, newHeightOfCamera, transform.position.z);
		} else {
			if (playerHeightY < (currentCameraHeight - 4.4f)) {

				OnGUI2D.OG2D.CheckHighScore ();
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if (playerHeightY > OnGUI2D.score) {
			OnGUI2D.score = (int)playerHeightY;
		}
	}

	void platformManager() {
		platCheck = player.position.y + 15;
		PlatformSpawner (platCheck + 15);
	}

	void PlatformSpawner(float floatValue) {

		float y = spawnPlatformsTo;

		while (y <= floatValue) {
			float x = Random.Range (-2.33f, 2.33f);
			int currentScore = OnGUI2D.score;
			platNumber = 0;
			if ( currentScore < 5) {
				platNumber = 4;
			} else if (currentScore < 30) {
				platNumber = Random.Range (1,20);
			} else {
				platNumber = Random.Range (1,7);
			}

			Vector2 posXY = new Vector2 (x,y);
			if (platNumber >= 4) {
				Instantiate (regular, posXY, Quaternion.identity);
				y += Random.Range (.5f, 0.7f);
			} else {
				if (platNumber == 3) {
					Instantiate (jump, posXY, Quaternion.identity);
				}
				if (platNumber == 2) {
					Instantiate (LeftRight, posXY, Quaternion.identity);
				}
				if (platNumber == 1) {
					Instantiate (UpDown, posXY, Quaternion.identity);
				}
				y += Random.Range (.5f, 1.75f);
			}

			Debug.Log ("Spawned Platform");
		}
		spawnPlatformsTo = floatValue;
	}
}
