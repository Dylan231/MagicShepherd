using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraanWinkelScript : MonoBehaviour {
    public Text AantalGraanText;
    public int aantalgraangekocht = 0;

	// Use this for initialization
	void Start () {
        SetAantalGraan();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("mouse 0"))
        {
            aantalgraangekocht = aantalgraangekocht + 1;
            SetAantalGraan();
        }
	}

    void SetAantalGraan()
    {
        AantalGraanText.text = "Graan gekocht: " + aantalgraangekocht;
    }
}
