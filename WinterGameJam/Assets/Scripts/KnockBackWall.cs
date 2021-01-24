using UnityEngine;

public class KnockBackWall : MonoBehaviour
{
    [SerializeField] float knockForce = 500f;
    [SerializeField] float knockDuration = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if(controller != null)
        {
            // rb.AddRelativeForce(knockForce * -Vector3.forward);
            controller.Disable(knockForce, knockDuration);
        }
    }
}