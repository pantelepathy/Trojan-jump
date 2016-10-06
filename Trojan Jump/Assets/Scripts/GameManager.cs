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

	public GameObject playBotton;
	public GameObject exitButton;
	public GameObject gameOverGo;
	public GameObject scoreBoard;
	public GameObject scoreUITextGo;
	public GameObject highscoreUITextGo;
	GameObject playerTrojan;

	public enum GameManagerState {
		Opening,
		GamePlay,
		GameOver,
	}

	private GameManagerState GMState;

	// Use this for initialization
	void Start () {

		playerTrojan = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		scoreUITextGo = GameObject.FindGameObjectWithTag ("ScoreTextTag");
		highscoreUITextGo = GameObject.FindGameObjectWithTag ("HighScoreTextTag");
		GMState = GameManagerState.Opening;

		SetGameManagerState (GMState);
	}

	void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}
	/// <summary>
	///  ADD
	/// </summary>

	void UpdateGameManagerState () {
		switch (GMState) {

		case GameManagerState.Opening:
			playerTrojan.SetActive (false);
			gameOverGo.SetActive (false);
			scoreBoard.SetActive (false);
			break;

		case GameManagerState.GamePlay:
			playerTrojan.SetActive (true);
			playBotton.SetActive (false);
			exitButton.SetActive (false);
			break;

		case GameManagerState.GameOver:
			gameOverGo.SetActive (true);
			scoreBoard.SetActive (true);
			playerTrojan.SetActive (false);
			if (OnGUI2D.score * 15 > OnGUI2D.highScore) {
				highscoreUITextGo.GetComponent<GameScore> ().Score = OnGUI2D.score * 15; 
			} else {
				highscoreUITextGo.GetComponent<GameScore> ().Score = OnGUI2D.highScore;
			}
			scoreUITextGo.GetComponent<GameScore> ().Score = OnGUI2D.score*15;

			Invoke ("Restart", 3f);
			break;
		}
	}


	public void SetGameManagerState(GameManagerState state){
		GMState = state;
		UpdateGameManagerState ();
	}


	public void StartGamePlay(){
		GMState = GameManagerState.GamePlay;
		UpdateGameManagerState ();
	}
	public void ExitGame(){
		Application.Quit ();
	}
	public static void SetHeight(GameObject gameObject, float height)
	{
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, height,gameObject.transform.position.z);
	}

	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit = new RaycastHit();

		for (int i = 0; i < Input.touchCount; i++) {
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				if (Physics.Raycast(ray, out hit)) {
					hit.transform.gameObject.SendMessage("OnMouseDown");
					StartGamePlay ();
				}
			}
		}

		if (GMState == GameManagerState.GamePlay) {
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
					//set to game over
					GMState = GameManagerState.GameOver;
					SetHeight(gameOverGo,currentCameraHeight );
					SetGameManagerState (GMState);
				}
			}

			if (playerHeightY > OnGUI2D.score) {
				OnGUI2D.score = (int)playerHeightY;
			}
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
