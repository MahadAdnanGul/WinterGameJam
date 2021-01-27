using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int multiplier = 2;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.multiplier = multiplier;
        }

    }
}
