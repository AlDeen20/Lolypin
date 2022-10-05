using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public GameObject pin;
	public int maxPin;

	[HideInInspector]
	public int totalPin;

	[HideInInspector]
	public int limitPin;

	private Text countText;

	void Awake(){
		totalPin = maxPin;
	}

	// Use this for initialization
	void Start () {
		InitializeSpawnVariables ();
	}
	
	// Update is called once per frame
	void Update () {	
		
	}

	void InitializeSpawnVariables(){
		limitPin = 4;

		Vector3[] pinPosition = new[] {
			new Vector3(0, -2.0f, 0),
			new Vector3(0, -2.8f, 0),
			new Vector3(0, -3.6f, 0),
			new Vector3(0, -4.4f, 0)
		};
			
		for (int i = 0; i < limitPin; i++) {
			if(maxPin != 0){
				GameObject newPin = Instantiate (pin, pinPosition [i], Quaternion.identity) as GameObject;
				if (i == 0)
					newPin.transform.GetComponent<Pin> ().isPrepareToShoot = true;

				countText = newPin.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ();
				countText.text = maxPin.ToString ();
				maxPin--;
			}
		} 



	}

	public void IsPrepareToFire(){
		int index = 0;
		Vector3[] pinPosition = new[] {
			new Vector3 (0, -2.0f, 0),
			new Vector3 (0, -2.8f, 0),
			new Vector3 (0, -3.6f, 0)
		};
		GameObject[] nextPin = GameObject.FindGameObjectsWithTag("Pin");
		foreach(GameObject newPin in nextPin){
			if(!newPin.transform.GetComponent<Pin>().hasMove){
				newPin.transform.position = Vector3.Lerp (newPin.transform.position, pinPosition [index], Time.time);
				if(index == 0){
					newPin.transform.GetComponent<Pin> ().isPrepareToShoot = true;
				}
				index++;

			}

		}
			
		if(maxPin != 0){
			GameObject newPin = Instantiate (pin, new Vector3(0, -4.4f, 0), Quaternion.identity) as GameObject;
			countText = newPin.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.GetComponent<Text>();
			countText.text = maxPin.ToString ();

			maxPin--;
		}


	}



}
