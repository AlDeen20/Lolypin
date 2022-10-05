using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour {

	public GameObject exitPanel;


	// Use this for initialization
	void Start () {
		if (GameController.instance != null && MusicController.instance != null) {
			if (GameController.instance.isMusicOn) {
				MusicController.instance.PlayBackgroundSound ();
			} else {
				MusicController.instance.StopAllSound ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (exitPanel.activeInHierarchy) {
				exitPanel.SetActive (false);
			} else {
				exitPanel.SetActive (true);
			}
		}
	}

	public void PlayButton(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void YesButton(){
		Application.Quit ();
	}

	public void NoButton(){
		exitPanel.SetActive (false);
	}
		

}
