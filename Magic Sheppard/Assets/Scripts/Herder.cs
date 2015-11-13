using UnityEngine;
using System.Collections;

public class Herder : MonoBehaviour {
	float speed = 15;
	float rotatespeed = 10;
    float rotatecamera = 8;
    float positiecameray = 2;
    float positiecameraz = -0.02f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var C = GameObject.FindGameObjectWithTag("MainCamera");
        if (Input.GetAxis("Mouse X")>0)
		{
            transform.Rotate(new Vector3(0, rotatespeed * Input.GetAxis("Mouse X"), 0));
		}
        if (Input.GetAxis("Mouse X")<0)
		{
			transform.Rotate(new Vector3(0, rotatespeed * Input.GetAxis("Mouse X"),0));
		}
        if (Input.GetAxis("Mouse Y")>0)
        {
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y"),0,0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y"), positiecameraz * Input.GetAxis("Mouse Y")));
        }
        if (Input.GetAxis("Mouse Y")<0)
        {
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y"), 0, 0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y"), positiecameraz * Input.GetAxis("Mouse Y")));
        }

		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
	}
}
