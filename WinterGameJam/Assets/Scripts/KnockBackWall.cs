using UnityEngine;

public class KnockBackWall : MonoBehaviour
{
    [SerializeField] float knockForce = 500f;
    [SerializeField] float knockDuration = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if(player != null)
        {
            // rb.AddRelativeForce(knockForce * -Vector3.forward);
            player.Disable(knockForce, knockDuration);
        }
    }
}