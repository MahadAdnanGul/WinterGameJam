using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
   // private CapsuleCollider col;
    //private float dist_to_ground;
    private float rotate;
   // private bool jump;
    //private float forward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // col = GetComponent<CapsuleCollider>();
       // dist_to_ground = col.bounds.extents.y + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        rotate = Input.GetAxis("Horizontal");
       // forward = Input.GetAxis("Vertical"); // Not currently using this

        //Rocket Implementation
        if (Input.GetAxis("Jump") > 0 && fuelAmount > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, currentRocketPower, rb.velocity.z);
            fuelAmount -= FuelDepletionRate * Time.deltaTime;
            if(currentRocketPower <= maxRocketPower)
            {
                currentRocketPower += RocketPowerRate * Time.deltaTime;
                if(currentRocketPower > maxRocketPower)
                {
                    currentRocketPower = maxRocketPower;
                }
            }
           // Debug.Log(fuelAmount);
        }
        else
        {
            currentRocketPower = defaultRocketPower;
        }
        if(fuelAmount<0)
        {
            fuelAmount = 0;
            //Debug.Log(fuelAmount);
        }
        //Rocket Implementation End
        


        //JumpStart (Replaced by Rocket)

        /*jump = Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, -Vector3.up, dist_to_ground);
        if (jump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump_height, rb.velocity.z);
        }*/

        //JumpEnd (Replaced by Rocket)

    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotate * turn_rate, 0));
        rb.angularVelocity = Vector3.zero;
        //rb.MovePosition(transform.position + transform.forward * speed);
        rb.velocity = transform.forward * speed * 20 + new Vector3(0, rb.velocity.y, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
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
