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
        SceneManager.LoadScene(1);
    }

}
