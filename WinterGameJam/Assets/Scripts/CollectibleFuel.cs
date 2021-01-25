using UnityEngine;

public class CollectibleFuel : MonoBehaviour
{
    [SerializeField] float refillAmount = 2f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if(controller != null)
        {
            if(controller.fuelAmount<controller.fuelCapacity)
            {
                controller.fuelAmount += refillAmount;
                if(controller.fuelAmount>controller.fuelCapacity)
                {
                    controller.fuelAmount = controller.fuelCapacity;
                }
                Destroy(gameObject);
            }
        }
        
    }
}