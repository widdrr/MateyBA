using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject inventoryPanel;
    public PlayerController player;
    public GameObject point;

    private PlayerState state;

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                point.transform.position = player.gameObject.transform.position;
                point.SetActive(true);
                pausePanel.SetActive(true);
                state = player.currentState;
                player.currentState = PlayerState.attacking;
                Time.timeScale = 0f;
                InventoryOpen();
            }
            else
            {
                point.SetActive(false);
                pausePanel.SetActive(false);
                player.currentState = state;
                Time.timeScale = 1f;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    //Close the pause menu
    public void Resume()
    {	
        point.SetActive(false);
        isPaused = false;
        pausePanel.SetActive(false);
        player.currentState = state;
        Time.timeScale = 1f;
    }

    //Switch from inventory panel to options panel
    public void OptionsOpen()
    {
        inventoryPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    
    //Switch from options panel to inventory panel
    public void InventoryOpen()
    {
        optionsPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }
}

