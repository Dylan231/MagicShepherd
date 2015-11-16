using UnityEngine;
using System.Collections;

public class HondScript : MonoBehaviour {
    float speed = 15;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        var H = GameObject.FindGameObjectWithTag("Herder");
        if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.parent = null;
            StartCoroutine(TimerHond1());
            
        }
    }

    IEnumerator TimerHond1()
    {
        InvokeRepeating("RennenHond", 0, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke();
        StartCoroutine(TimerHond2());
    }

    IEnumerator TimerHond2()
    {
        //transform.Rotate(new Vector3(0, 180, 0));
        InvokeRepeating("DraaienHondenRennen", 0, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke();
        gameObject.SetActive(false);
        ReturnBeginHond();
    }

    void RennenHond()
    {
        //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DraaienHondenRennen()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void ReturnBeginHond()
    {
        gameObject.SetActive(true);
        var H = GameObject.FindGameObjectWithTag("Herder");
        Vector3 beginplekhond = new Vector3(H.transform.position.x + 1, H.transform.position.y - 1, H.transform.position.z - 1);
        transform.Rotate(new Vector3(0, -180, 0));
        transform.position = beginplekhond;
        transform.parent = H.transform;
    }
 }

