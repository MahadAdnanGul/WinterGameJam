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
        Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level",1));
    }

}
