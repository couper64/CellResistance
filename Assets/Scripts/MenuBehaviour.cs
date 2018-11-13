using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) {
			GetComponent<AudioSource>().Play();
			UnityEngine.SceneManagement.SceneManager.LoadScene("scene_game_play");
		}
	}
}
