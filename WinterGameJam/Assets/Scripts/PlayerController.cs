using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Button attackButton;

    public float fuelCapacity = 100f;
    public float fuelAmount = 100f;

    private Rigidbody rb;
    private float rotate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate = Input.GetAxis("Horizontal");
        // forward = Input.GetAxis("Vertical"); // Not currently using this
        rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotate * turn_rate, 0));
        rb.velocity = (transform.forward * speed) + new Vector3(0, rb.velocity.y, 0);

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
            else if (other.gameObject.CompareTag("Attack"))
            {
                if(attackButton.interactable==false)
                {
                    attackButton.interactable = true;
                    Destroy(other.gameObject);
                }
              
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
