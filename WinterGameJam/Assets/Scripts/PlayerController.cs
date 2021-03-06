﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject finishLine;
    private float distanceFromFinish;

    public bool fixedPosition;

     public GameObject lose;
    public GameObject win;

    PlaceHolder placeHolder;

    GameObject[] bots;
    private float[] botsDistance;

    [SerializeField] private float turn_rate = 1;
    //[SerializeField] private float speed = 1;
    //[SerializeField] private float accel = 1;
    //[SerializeField] private float currentRocketPower = 1;
    //[SerializeField] private float defaultRocketPower = 1;
    //[SerializeField] private float maxRocketPower = 3;
    //[SerializeField] private float RocketPowerRate = 4;
    //[SerializeField] private float FuelDepletionRate = 30;
    [SerializeField] private Button attackButton;

    [SerializeField] private Joystick joystick;

    public bool isComplete = false;
    public bool done = false;
    public int multiplier = 1;
    public float Score = 1000;
    public GameObject lastTouchedBonus;

    //public float fuelCapacity = 100f;
    //public float fuelAmount = 100f;
    float buttonReset = 0.2f;
    int buttonCount = 0;

    public int place;

   
    private PlayerMovement movement;
    private float rotate;
    //private bool disabled = false;
    //private float disableTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        fixedPosition = false;
        isComplete = false;
        bots = GameObject.FindGameObjectsWithTag("Bot");
        finishLine = GameObject.FindGameObjectWithTag("Win");
        botsDistance = new float[bots.Length];
        placeHolder = GameObject.FindGameObjectWithTag("Place").GetComponent<PlaceHolder>();
        
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        place = 6;

       if (Input.GetMouseButtonDown(0))
        {
            buttonReset = 0.3f;
            buttonCount += 1;
        }
        else if (buttonReset > 0 && buttonCount == 2 && Input.GetMouseButton(0))
        {
            movement.Rocket();
        }

        else if(buttonReset>0)
        {
            buttonReset -= 1 * Time.deltaTime;
        }
        else
        {
            buttonCount = 0;
        }
        rotate = joystick.Horizontal;
        Quaternion quat = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotate * turn_rate, 0);
        if(!done)
        {
            movement.Move(quat);
        }

        if(!fixedPosition)
        {
            distanceFromFinish = Vector3.Distance(transform.position, finishLine.GetComponent<Transform>().position);

            for (int i = 0; i < bots.Length; i++)
            {
                botsDistance[i] = Vector3.Distance(finishLine.transform.position, bots[i].transform.position);
            }
            for (int i = 0; i < bots.Length; i++)
            {
                if (distanceFromFinish < botsDistance[i])
                {
                    place--;
                }
            }
            placeHolder.SetPlace(place);
        }
        

        

        //rotate = Input.GetAxis("Horizontal");


        ////Rocket Implementation
        //if (Input.GetAxis("Jump") > 0 && fuelAmount > 0)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, currentRocketPower, rb.velocity.z);
        //    fuelAmount -= FuelDepletionRate * Time.deltaTime;
        //    if(currentRocketPower <= maxRocketPower)
        //    {
        //        currentRocketPower += RocketPowerRate * Time.deltaTime;
        //        if(currentRocketPower > maxRocketPower)
        //        {
        //            currentRocketPower = maxRocketPower;
        //        }
        //    }
        //   // Debug.Log(fuelAmount);
        //}
        //else
        //{
        //    currentRocketPower = defaultRocketPower;
        //}
        //if(fuelAmount<0)
        //{
        //    fuelAmount = 0;
        //    //Debug.Log(fuelAmount);
        //}
        ////Rocket Implementation End



        //rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotate * turn_rate, 0));
        //rb.angularVelocity = Vector3.zero;

        //if (disabled)
        //{
        //    disableTimer -= Time.deltaTime;
        //    if (disableTimer < 0)
        //    {
        //        rb.velocity = Vector3.zero;
        //        disabled = false;
        //    }
        //}
        //else
        //{
        //    float new_speed = (new Vector3(rb.velocity.x, 0, rb.velocity.z)).magnitude + accel/10;
        //    if (new_speed > speed)
        //    {
        //        new_speed = speed;
        //    }
        //    rb.velocity = transform.forward * new_speed + new Vector3(0, rb.velocity.y, 0);

        //}


    }

    public void OnDeath()
    {
        if(isComplete)
        {
            gameObject.transform.position = lastTouchedBonus.transform.position;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f, gameObject.transform.position.z);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            done = true;
            if(fixedPosition)
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level",0)+1);
                win.SetActive(true);
            }
            else
            {
                lose.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 0;
            lose.SetActive(true);
        }
        
            
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Attack"))
        {
            if (attackButton.interactable == false)
            {
                attackButton.interactable = true;
                Destroy(other.gameObject);
            }

        }

        
    }

    

}
