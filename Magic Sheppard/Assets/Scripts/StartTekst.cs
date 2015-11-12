using UnityEngine;
using System.Collections;

public class StartTekst : MonoBehaviour {
	
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	
	void OnMouseEnter() {
		transform.Translate (new Vector3(10, 0, 0));
		
	}
	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("Game"); 
		}
	}
	void OnMouseExit() {
		transform.Translate (new Vector3(-10, 0, 0));
	}
}