using UnityEngine;
using System.Collections;

public class OnGUI2D : MonoBehaviour {

	public static OnGUI2D OG2D;

	public static int score;
	int highScore;

	// Use this for initialization
	void Start () {
		OG2D = this;
		score = 0;
		highScore = PlayerPrefs.GetInt("HighScore1", 0) ;
	}

	void OnGUI() {
		GUI.Label (new Rect(10,10,100,20),"Score: " + (score * 15));
		GUI.Label (new Rect(10,30,100,20),"High Score: " + highScore);
	}

	public void CheckHighScore() {
		if ((score * 15) > highScore) {
			Debug.Log ("Saving Score");

			PlayerPrefs.SetInt ("HighScore1", (score * 15)) ;
		}
	}
}
