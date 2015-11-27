using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Herder : MonoBehaviour {
	float speed = 15;
	float rotatespeed = 10;
    float rotatecamera = 8;
    float positiecameray = 4;
    float positiecameraz = 1;
    public Text Ziekschaap;
    // Use this for initialization
    void Start () {
        Ziekschaap.text = "";
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
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y")*Time.deltaTime,0,0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y") * Time.deltaTime, positiecameraz * Input.GetAxis("Mouse Y") * Time.deltaTime));
        }
        if (Input.GetAxis("Mouse Y")<0)
        {
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y")* Time.deltaTime, 0, 0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y") * Time.deltaTime, positiecameraz * Input.GetAxis("Mouse Y") * Time.deltaTime));
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

        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;

        if (leng == 0)
        {
            Ziekschaap.text = "";
        }

        if (leng > 0)
        {
            Ziekschaap.text = "ER IS EEN ZIEK SCHAAP!";
            StartCoroutine(Timerziekschaap());
            for (int m = 0; m < leng; m++)
            {
                GameObject ziekschaap = ziek[m];
                float xposs = ziekschaap.transform.position.x;
                float zposs = ziekschaap.transform.position.z;
                float xposh = transform.position.x;
                float zposh = transform.position.z;
                float xvers = Mathf.Abs(xposs - xposh);
                float zvers = Mathf.Abs(zposs - zposh);
                if (xvers <10f && zvers<10f && Input.GetKey(KeyCode.Alpha3))
                {
                    ziekschaap.tag = "Schaap";
                    ziekschaap.GetComponent<Renderer>().material.color = Color.grey;
                }
            }
        }
    }

    IEnumerator Timerziekschaap()
    {
        yield return new WaitForSeconds(20);
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            ziek[0].SetActive(false);
        }
    }
}
