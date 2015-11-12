using UnityEngine;
using System.Collections;

public class Herder : MonoBehaviour {
	float speed = 15;
	float rotatespeed = 10;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse X")>0)
		{
            transform.Rotate(new Vector3(0, rotatespeed * Input.GetAxis("Mouse X"), 0));
		}
        if (Input.GetAxis("Mouse X")<0)
		{
			transform.Rotate(new Vector3(0,rotatespeed * Input.GetAxis("Mouse X"),0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
	}
}
