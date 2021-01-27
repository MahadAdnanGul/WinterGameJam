using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{

    [SerializeField] private GameObject LevelCompleteArea;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LevelCompleteArea.SetActive(true);
        }
    }

}
    
