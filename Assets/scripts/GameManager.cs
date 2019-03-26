using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public int score;
	int highscore;
	public Text scoreV;
	public Text hScoreV;

	// Use this for initialization
	void Start() {
		score = 0;
		Cursor.visible = false;
		highscore = PlayerPrefs.GetInt("RECORD", 0);
		hScoreV.text = highscore.ToString();
	}

	// Update is called once per frame
	void Update() {
		scoreV.text = score.ToString();
		if (score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("RECORD", highscore);
		}
	}


}