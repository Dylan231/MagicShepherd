using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	float speed = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.y > 0) {	
			transform.Rotate (new Vector3 (0, speed * Time.deltaTime));
		}
		if (transform.rotation.y >= 90) {	
			transform.Rotate (new Vector3 (0, -speed * Time.deltaTime));
		}
	}
}
