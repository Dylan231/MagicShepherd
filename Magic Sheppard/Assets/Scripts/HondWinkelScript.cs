using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HondWinkelScript : MonoBehaviour {
    public Text AantalHondText;
    public int aantalhondgekocht = 0;

    // Use this for initialization
    void Start()
    {
        SetAantalHond();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        aantalhondgekocht = aantalhondgekocht + 1;
        SetAantalHond();
    }

    void SetAantalHond()
    {
        AantalHondText.text = "Hond gekocht: " + aantalhondgekocht;
    }
}