using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    //Implementation of inventory is needed to build and test properly this coin text updater.
    public TMP_Text coinHUD;

    public void Update()
    {
        coinHUD.text = "00" + 5;
    }
}
