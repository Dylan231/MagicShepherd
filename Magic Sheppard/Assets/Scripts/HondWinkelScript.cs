﻿using UnityEngine;
using UnityEngine.UI;

public class HondWinkelScript : MonoBehaviour {
    public Text AantalHondText;
    public static int aantalhondgekocht = 0;

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
        if (GraanWinkelScript.aantalcoins > 0)
        {
            aantalhondgekocht = aantalhondgekocht + 1;
            SetAantalHond();
            GraanWinkelScript.aantalcoins = GraanWinkelScript.aantalcoins - 1;
        }
            
    }

    void SetAantalHond()
    {
        AantalHondText.text = "Hond gekocht: " + aantalhondgekocht;
    }

    void SetAantalCoins()
    {

    }
}