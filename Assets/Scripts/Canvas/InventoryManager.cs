using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject inventoryPanel;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                inventoryPanel.SetActive(true);
                player.SetActive(false);
                Time.timeScale = 0f;

            }
            else
            {
                inventoryPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
        player.SetActive(true);
    }
}

