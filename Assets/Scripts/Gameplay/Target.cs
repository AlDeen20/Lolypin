using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RotateMovement ();
	}

	void RotateMovement(){
		transform.Rotate (0, 0, speed * Time.deltaTime);
	}
}
