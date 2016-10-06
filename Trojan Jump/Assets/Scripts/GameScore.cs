using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

	// Use this for initialization
	Text scoreTextUI;
	int score;


	public int Score
	{
		get{ 
			return this.score;
		}
		set{ 
			this.score = value;
			UpdateScoreTextUI ();
		}
	}



	void Start () {
		scoreTextUI = GetComponent<Text> ();
	}

	void UpdateScoreTextUI () {
		string scoreStr = string.Format ("{0:000000}",score);
		scoreTextUI.text = scoreStr;
	}
}
