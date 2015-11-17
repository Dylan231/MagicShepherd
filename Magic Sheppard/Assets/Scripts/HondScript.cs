using UnityEngine;
using System.Collections;

public class HondScript : MonoBehaviour {
    float speed = 15;
    float speed2 = 30;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
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
        InvokeRepeating("DraaienHondenRennen", 0, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke();
        StartCoroutine(TimerHond3());
    }

    IEnumerator TimerHond3()
    {
        InvokeRepeating("DraaiHondRen", 0, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke();
        StartCoroutine(TimerHond4());
    }

    IEnumerator TimerHond4()
    {
        InvokeRepeating("DraaienHondenRennen", 0, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke();
        gameObject.SetActive(false);
        ReturnBeginHond();
    }

    void RennenHond()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DraaienHondenRennen()
    {
        transform.Translate(Vector3.back * speed2 * Time.deltaTime);
    }

    void DraaiHondRen()
    {
        transform.Translate(Vector3.forward * speed2 * Time.deltaTime);
    }

    void ReturnBeginHond()
    {
        gameObject.SetActive(true);
        var H = GameObject.FindGameObjectWithTag("Herder");
        Vector3 beginplekhond = new Vector3(H.transform.position.x + 1, H.transform.position.y - 1, H.transform.position.z - 1);
        transform.position = beginplekhond;
        transform.parent = H.transform;
    }
 }

