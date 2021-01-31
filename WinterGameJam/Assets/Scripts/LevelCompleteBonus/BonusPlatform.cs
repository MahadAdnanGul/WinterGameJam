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
            if(multiplier==1)
            {
                if(controller.place==1)
                {
                    controller.fixedPosition = true;
                }
                else
                {
                    controller.OnDeath();
                }
            }
            if(controller.multiplier<=multiplier)
            {
                controller.multiplier = multiplier;
                controller.lastTouchedBonus = gameObject;
                controller.isComplete = true;
            }
            if(multiplier==10)
            {
                controller.OnDeath();
            }
            
            
        }

    }
}
