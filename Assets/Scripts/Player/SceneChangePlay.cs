using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangePlay : MonoBehaviour
{
    public void Start()
    {
        SetRatio(4, 3);   
    }
    void SetRatio(float w, float h)
    {
        if ((((float)Screen.width) / ((float)Screen.height)) > w / h)
        {
            Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
        }
    }
    public void LoadScene(string sceneName)
    {
	    SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
