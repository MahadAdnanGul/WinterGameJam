using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private Waypoint waypoint;

    [Header("Character Settings")]
    [SerializeField] private float turn_rate = 1;
    [SerializeField] private float speed = 1;
    [SerializeField] private float currentRocketPower = 1;
    [SerializeField] private float defaultRocketPower = 1;
    [SerializeField] private float maxRocketPower = 3;
    [SerializeField] private float RocketPowerRate = 4;
    [SerializeField] private float FuelDepletionRate = 30;
    [SerializeField] private float replenishAmount = 2;

    public float fuelCapacity = 100f;
    public float fuelAmount = 100f;

    private Rigidbody rb;
    private Quaternion old_rot;
    private Quaternion target_rot;
    private float frame_count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        old_rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waypoint)
            return;
        Vector3 direction = (waypoint.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(direction, transform.forward);
        Vector3 cross = Vector3.Cross(direction, transform.forward);

        float rot_modifier = cross.y; // 1 iff perpendicular
        float target_angle = transform.rotation.eulerAngles.y - rot_modifier * (angle);
        float new_y = Mathf.LerpAngle(transform.rotation.eulerAngles.y, target_angle, 0.001f*speed*turn_rate);

        //Quaternion target_rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rot_modifier*(angle - 1), 0);
        
        Debug.DrawRay(transform.position, waypoint.transform.position - transform.position);
        Quaternion new_rot = Quaternion.Euler(0, new_y, 0);
        rb.MoveRotation(new_rot);
        rb.velocity = (transform.forward * speed) + new Vector3(0, rb.velocity.y, 0);


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetInstanceID() == waypoint.gameObject.GetInstanceID())
        {
            Debug.Log("Reached Waypoint" + other.gameObject.name);
            waypoint = waypoint.next;
        }

        if(fuelAmount < fuelCapacity)
        {
            if (other.gameObject.CompareTag("FuelDrop"))
            {
                ReplenishFuel(replenishAmount);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Fuel"))
            {
                ReplenishFuel(replenishAmount * 15);
                Destroy(other.gameObject);
            }
        }
    }

    private void ReplenishFuel(float amount)
    {
        if(fuelAmount < fuelCapacity)
        {
            fuelAmount += amount;
        }
        if(fuelAmount > fuelCapacity)
        {
            fuelAmount = fuelCapacity;
        }
    }
}
