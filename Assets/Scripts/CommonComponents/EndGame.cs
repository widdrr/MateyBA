using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public HealthManager player;
    public void Start()
    {
     	HealthManager player = GameObject.FindWithTag("Player").GetComponent<HealthManager>();   
    }
    public void FixedUpdate()
    {
		if(player.maxHealth >= 12)
	{
	    	SceneManager.LoadScene("MainMenu");
	}
    }
}
