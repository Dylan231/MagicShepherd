using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraanScript : MonoBehaviour {
    public int aantalgraangekocht = 2;
    public Text AantalGraanText;

    private bool graantje = true;


    // Use this for initialization
    void Start () {
        SetAantalGraanText();
	}
	
	// Update is called once per frame
	void Update () {
		var H = GameObject.FindGameObjectWithTag ("Herder");
		if (aantalgraangekocht > 0 && Input.GetKeyDown(KeyCode.Alpha1) && graantje == true)
		{
            transform.parent = null;
            Vector3 GraanPlek = new Vector3(H.transform.position.x, H.transform.position.y, H.transform.position.z);
			transform.position = GraanPlek;
            graantje = false;
			StartCoroutine(TimerGraan());
		}
}

	IEnumerator TimerGraan()
	{
        //Hier komt de aantrekking van het graan voor de schapen
        aantalgraangekocht = aantalgraangekocht - 1;
        SetAantalGraanText();
        yield return new WaitForSeconds (10);
		gameObject.SetActive (false);
        if (aantalgraangekocht > 0)
        {
            ReturnBeginGraan();
        }
	}

    void ReturnBeginGraan()
    {
        gameObject.SetActive(true);
        var H = GameObject.FindGameObjectWithTag("Herder");
        Vector3 beginplekgraan = new Vector3(H.transform.position.x, H.transform.position.y, H.transform.position.z);
        transform.position = beginplekgraan;
        transform.parent = H.transform;
        graantje = true;
    }

    void SetAantalGraanText()
    {
        AantalGraanText.text = "Graan: " + aantalgraangekocht;
    }

}
