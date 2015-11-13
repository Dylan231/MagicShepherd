using UnityEngine;
using System.Collections;

public class HondScript : MonoBehaviour {
    float speed = 15;
    private bool rennen = false;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        var H = GameObject.FindGameObjectWithTag("Herder");
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.parent = null;
            rennen = true;
            
            if (rennen)
            {
                StartCoroutine(TimerHond());
            }
        }
    }

    IEnumerator TimerHond()
    {
        RennenHond();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        ReturnBeginHond();
        rennen = false;
    }

    void RennenHond()
    {
        transform.position = transform.forward * speed * Time.deltaTime;
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

