using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour {

	public float speed;
	public Rigidbody2D myRigidBody;
	public RuntimeAnimatorController cameraZoom;
	public bool isPrepareToShoot = false;
	public bool hasMove = false;
	public AudioClip hit;

	private AudioSource audioSource;
	private Animator animator;
	private bool isSticked = false;
	private bool isFire = false;
	private bool hasTouched = false;

	void Awake(){
		audioSource = GetComponent<AudioSource> ();
		animator = GameObject.FindGameObjectWithTag ("MainCamera").transform.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.platform == RuntimePlatform.WindowsEditor){
			KeyBoardButton ();
		}else if(Application.platform == RuntimePlatform.Android){
			TouchButton ();
		}
			
		MoveUpward ();
	}


	void KeyBoardButton(){
		if(Input.GetKeyDown(KeyCode.Space)){
			if(isPrepareToShoot){
				isFire = true;
				hasMove = true;
			}
		}
	}

	void TouchButton(){
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch (0);

			if(touch.phase == TouchPhase.Began){
				if(isPrepareToShoot){
					isFire = true;
					hasMove = true;
				}
			}
		}
	}

	void MoveUpward(){
		if (!isSticked && isPrepareToShoot && isFire) {
			myRigidBody.MovePosition (myRigidBody.position + Vector2.up * speed * Time.deltaTime);
			transform.GetChild (0).gameObject.SetActive (true);
		} 
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag ("Target")) {
			isSticked = true;
			if(isSticked && !hasTouched){
				audioSource.PlayOneShot (hit);
				GameplayController.instance.score++;
				if(GameplayController.instance.score == GameObject.FindGameObjectWithTag("Spawner").transform.GetComponent<Spawner>().totalPin){
					GameObject.Find ("Background").transform.GetComponent<Image> ().color = Color.green;
					GameObject.FindGameObjectWithTag ("Target").transform.GetComponent<Target> ().enabled = false;
					Vector3 temp = transform.GetChild (0).transform.localScale;
					temp = new Vector3 (temp.x, temp.y * temp.y, temp.z);
					GameObject[] pin = GameObject.FindGameObjectsWithTag ("Pin");
					foreach(GameObject newPin in pin){
						newPin.transform.GetChild (0).transform.localScale = temp;
						newPin.transform.GetChild (1).transform.GetChild (0).transform.gameObject.SetActive (false);
						newPin.transform.GetComponent<SpriteRenderer> ().enabled = false;
					}
					StartCoroutine (NextLevel ());
					isPrepareToShoot = false;
					isFire = false;
				}
			}

			GameObject.FindGameObjectWithTag ("Spawner").transform.GetComponent<Spawner> ().IsPrepareToFire ();
			transform.SetParent (collider.transform);

		} else if(collider.CompareTag("Pin")) {
			if (!isSticked) {
				hasTouched = true;
				GameObject.FindGameObjectWithTag ("Target").transform.GetComponent<Target> ().enabled = false;
				GameObject.Find ("Background").transform.GetComponent<Image> ().color = Color.red;
				StartCoroutine (RestartLevel ());
				animator.runtimeAnimatorController = cameraZoom;
				isPrepareToShoot = false;
				isFire = false;
			}
		}
	}

	IEnumerator NextLevel(){
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("Success");
	}

	IEnumerator RestartLevel(){
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("Failed");
	}


}

