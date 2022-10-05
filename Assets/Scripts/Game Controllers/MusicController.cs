using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	public static MusicController instance;

	public AudioClip background;
	public AudioClip gameplay;
	public AudioClip success;
	public AudioClip failed;

	private AudioSource audioSource;

	void Awake(){
		CreateInstance ();
		audioSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	void CreateInstance(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void PlayBackgroundSound(){
		if(background){
			audioSource.clip = background;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	public void StopAllSound(){
		if(audioSource.isPlaying){
			audioSource.Stop ();
		}
	}

	public void PlayGameplaySound(){
		if(gameplay){
			audioSource.clip = gameplay;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	public void PlaySuccessSound(){
		if(success){
			audioSource.PlayOneShot (success);
		}
	}

	public void PlayFailedSound(){
		if(failed){
			audioSource.PlayOneShot (failed);
		}
	}

}
