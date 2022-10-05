using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour {

	public Text scoreText;

	private int score;

	// Use this for initialization
	void Start () {
		InitializeVariables ();
		if (GameController.instance != null && MusicController.instance != null) {
			if(GameController.instance.isMusicOn){
				MusicController.instance.StopAllSound ();
				MusicController.instance.PlaySuccessSound ();
			}
		} 
	}

	void InitializeVariables(){
		score = GameController.instance.currentScore;
		scoreText.text = score.ToString ();
	}

	public void NextButton(){
		SceneManager.LoadScene ("Gameplay");
	}

	public void ExitButton(){
		SceneManager.LoadScene ("Main Menu");
	}
		

}
