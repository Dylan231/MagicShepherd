using UnityEngine;
using System.Collections;

public class HeksScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        foreach (GameObject go in gos)
        {
            float xcoordinaat = go.transform.position.x;
            float zcoordinaat = go.transform.position.z;
        }
	}
}
