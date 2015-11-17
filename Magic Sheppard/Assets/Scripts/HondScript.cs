using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HondScript : MonoBehaviour {
    public int aantalhondengekocht = HondWinkelScript.aantalhondgekocht;
    public Text AantalHondenGekocht;
    bool aan = false;
    float xbegin;
    float zbegin;
    bool n1 = false;
    bool n2 = false;
    bool n3 = false;
    bool n4 = false;

    // Use this for initialization
    void Start () {
        SetAantalHondenText();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2) && aantalhondengekocht > 0 && aan == false)
        {
            transform.parent = null;
            aan = true;
            n1 = true;
            aantalhondengekocht = aantalhondengekocht - 1;
            SetAantalHondenText();
        }
        if (n1 == true)
        {
            xbegin = gameObject.transform.position.x;
            zbegin = gameObject.transform.position.z;
            InvokeRepeating("PlekjeZoeken", 0, 0.2f);
        }
        if (n2 == true)
        {
            xbegin = gameObject.transform.position.x;
            zbegin = gameObject.transform.position.z;
            InvokeRepeating("PlekjeZoeken2", 0, 0.2f);
        }
        if (n3 == true)
        {
            xbegin = gameObject.transform.position.x;
            zbegin = gameObject.transform.position.z;
            InvokeRepeating("PlekjeZoeken3", 0, 0.2f);
        }
        if (n4 == true)
        {
            xbegin = gameObject.transform.position.x;
            zbegin = gameObject.transform.position.z;
            InvokeRepeating("TerugnaarHerder", 0, 0.2f);
        }
    }

    void PlekjeZoeken()
    {
        float xdesired = xbegin + 0.1f;
        float zdesired = zbegin + 0.5f;

        float xrichting = xdesired - xbegin;
        float zrichting = zdesired - zbegin;

        float xnu = gameObject.transform.position.x;
        float znu = gameObject.transform.position.z;

        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
        {
            n1 = false;
            CancelInvoke();
            n2 = true;
        }
    }

    void PlekjeZoeken2()
    {
        float xdesired = xbegin - 0.1f;
        float zdesired = zbegin - 0.5f;

        float xrichting = xdesired - xbegin;
        float zrichting = zdesired - zbegin;

        float xnu = gameObject.transform.position.x;
        float znu = gameObject.transform.position.z;

        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
        {
            n2 = false;
            CancelInvoke();
            n3 = true;
        }
    }
    
    void PlekjeZoeken3()
    {
        float xdesired = xbegin + 0.1f;
        float zdesired = zbegin + 0.5f;

        float xrichting = xdesired - xbegin;
        float zrichting = zdesired - zbegin;

        float xnu = gameObject.transform.position.x;
        float znu = gameObject.transform.position.z;

        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
        {
            n3 = false;
            CancelInvoke();
            n4 = true;
        }
    }

    void TerugnaarHerder()
    {
        var H = GameObject.FindGameObjectWithTag("Herder");

        float xdesired = H.transform.position.x + 1;
        float zdesired = H.transform.position.z - 1;

        float xnu = gameObject.transform.position.x;
        float znu = gameObject.transform.position.z;

        float xrichting = xdesired - xnu;
        float zrichting = zdesired - znu;
        
        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
        {
            n4 = false;
            CancelInvoke();
            ReturnBeginHond();
        }
    }

    void ReturnBeginHond()
    {
        var H = GameObject.FindGameObjectWithTag("Herder");
        transform.parent = H.transform;
        aan = false;
    }

    void SetAantalHondenText()
    {
        AantalHondenGekocht.text = "Aantal honden: " + aantalhondengekocht;
    }
}

