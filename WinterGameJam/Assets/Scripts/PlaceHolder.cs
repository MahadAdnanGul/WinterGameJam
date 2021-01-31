using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlaceHolder : MonoBehaviour
{
    TextMeshProUGUI pos;
    string defInit = "th";
    string firstInit = "st";
    string secInit = "nd";
    string thirdInit = "rd";
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void SetPlace(int place)
    {
        if(place==1)
        {
            pos.text = place.ToString() + firstInit;
        }
        else if(place==2)
        {
            pos.text = place.ToString() + secInit;
        }
        else if(place==3)
        {
            pos.text = place.ToString() + thirdInit;
        }
        else
        {
            pos.text = place.ToString() + defInit;
        }
    }
}
