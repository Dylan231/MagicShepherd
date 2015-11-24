using UnityEngine;
using System.Collections;

public class HeksScript : MonoBehaviour {
    bool keuze = false;
    private int lengte;
    private int chosenone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (keuze == false)
        {
            KeuzeMaken();
        }

        if (keuze == true)
        {
            InvokeRepeating("SchaapZoeken", 0, 0.2f);
        }
        
	}

    void KeuzeMaken()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        lengte = gos.Length;

        System.Random r = new System.Random();

        chosenone = r.Next(lengte);

        keuze = true;
    }

    void SchaapZoeken()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        
        GameObject o1 = gos[chosenone];

        if (o1.activeSelf == false)
        {
            keuze = false;
            CancelInvoke();
        }

        float xdesired = o1.transform.position.x;
        float zdesired = o1.transform.position.z;

        float xeigen = gameObject.transform.position.x;
        float zeigen = gameObject.transform.position.z;

        float xrichting = xdesired - xeigen;
        float zrichting = zdesired - zeigen;

        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesired - xeigen) < 0.1 && Mathf.Abs(zdesired - zeigen) < 0.1)
        {
            //doodmaken schaap
            DoodmakenSchaap();
            keuze = false;
            CancelInvoke();
        }
    }

    void DoodmakenSchaap()
    {
        System.Random r1 = new System.Random();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        GameObject o1 = gos[chosenone];

        int r2 = r1.Next(10);

        if (r2 == 0)
        {
            //doodschieten effect
            o1.SetActive(false);
        }
    }
}

