using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    //Implementation of inventory is needed to build and test properly this coin text updater.
    private TextMeshProUGUI coinCounter;
    public static int coinNumber;

    public void Start()
    {
        coinCounter = GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        coinCounter.text = coinNumber.ToString("000");
    }
}
