using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketFuelSlider : MonoBehaviour
{
    PlayerController player;
    Slider fuelSlider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        fuelSlider = gameObject.GetComponent<Slider>();
        fuelSlider.minValue = 0;
        fuelSlider.maxValue = player.fuelCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        fuelSlider.value = player.fuelAmount;
    }
}
