﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	float speed = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			transform.Rotate (new Vector3 (0, speed * Time.deltaTime * Input.GetAxis ("Mouse X")));
	}
}
