using UnityEngine;

public class CollectibleFuel : MonoBehaviour
{
    [SerializeField] float refillAmount = 2f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if(controller != null)
        {
            controller.fuelAmount += controller.fuelCapacity > controller.fuelAmount ? refillAmount : 0f;
        }
        Destroy(gameObject);
    }
}