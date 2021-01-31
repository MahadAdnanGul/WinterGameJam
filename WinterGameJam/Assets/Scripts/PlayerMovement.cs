using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float turn_rate = 1;
    [SerializeField] public float speed = 1;
    [SerializeField] private float accel = 1;
    [SerializeField] private float currentRocketPower = 1;
    [SerializeField] private float defaultRocketPower = 1;
    [SerializeField] private float maxRocketPower = 3;
    [SerializeField] private float RocketPowerRate = 4;
    [SerializeField] private float FuelDepletionRate = 30;
    [SerializeField] private Button attackButton;

    

    public float fuelCapacity = 100f;
    public float fuelAmount = 100f;

    private Rigidbody rb;
    private float rotate;
    private bool disabled = false;
    private float disableTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
      
    }

    public void Move(Quaternion quat)
    {
        rb.MoveRotation(quat);
        rb.angularVelocity = Vector3.zero;

        if (disabled)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer < 0)
            {
                rb.velocity = Vector3.zero;
                disabled = false;
            }
        }
        else
        {
            float new_speed = (new Vector3(rb.velocity.x, 0, rb.velocity.z)).magnitude + accel / 10;
            if (new_speed > speed)
            {
                new_speed = speed;
            }
            rb.velocity = transform.forward * new_speed + new Vector3(0, rb.velocity.y, 0);

        }
    }

    public void Rocket()
    {
        //Rocket Implementation
        if (fuelAmount > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, currentRocketPower, rb.velocity.z);
            //rb.AddForce(new Vector3(0, currentRocketPower, 0), ForceMode.Acceleration);
            fuelAmount -= FuelDepletionRate * Time.deltaTime;
            if (currentRocketPower <= maxRocketPower)
            {
                currentRocketPower += RocketPowerRate * Time.deltaTime;
                if (currentRocketPower > maxRocketPower)
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
        if (fuelAmount < 0)
        {
            fuelAmount = 0;
            //Debug.Log(fuelAmount);
        }
    }

    public void ForceRocket()
    {
        if (fuelAmount > 0)
        {
            //rb.velocity = new Vector3(rb.velocity.x, currentRocketPower, rb.velocity.z);
            
            rb.AddForce(new Vector3(0, currentRocketPower, 0), ForceMode.Acceleration);
            if(rb.velocity.y>maxRocketPower/4)
            {
                rb.velocity = new Vector3(rb.velocity.x, maxRocketPower/4, rb.velocity.z);
            }
            fuelAmount -= FuelDepletionRate * Time.deltaTime;
            if (currentRocketPower <= maxRocketPower)
            {
                currentRocketPower += RocketPowerRate * Time.deltaTime;
                if (currentRocketPower > maxRocketPower)
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
        if (fuelAmount < 0)
        {
            fuelAmount = 0;
            //Debug.Log(fuelAmount);
        }
    }

    public void Disable(float force, float duration)
    {
        if (gameObject.tag == "Player")
        {
            FindObjectOfType<SoundManager>().PlayPush();
            disabled = true;
            disableTimer = duration;
            rb.AddRelativeForce(force * -Vector3.forward, ForceMode.Acceleration);
        }
    }

}
