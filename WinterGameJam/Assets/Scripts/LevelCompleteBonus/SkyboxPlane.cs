using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxPlane : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().OnDeath();
        }
    }
}
