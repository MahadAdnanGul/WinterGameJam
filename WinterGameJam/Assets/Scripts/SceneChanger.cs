using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
  
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MainScene()
    {
        if(PlayerPrefs.GetInt("Level",1)>5)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level",1));
    }

}
