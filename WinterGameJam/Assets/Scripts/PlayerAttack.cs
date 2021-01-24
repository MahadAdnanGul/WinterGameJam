using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private bool attack;
    private bool forward;
    private bool reverse;
    private float currentExtend;
    private float initialExtend;
    [SerializeField] private float force = 20;
    [SerializeField] private float extendRate = 10;
    [SerializeField] private float maxExtend = 10;
    [SerializeField] private Button attackButton;
    // Start is called before the first frame update
    void Start()
    {
        forward = true;
        reverse = false;
        initialExtend = transform.localPosition.z;
        currentExtend = 0;
        attack = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(attack)
        {
            if(currentExtend < maxExtend && forward)
            {
                currentExtend = transform.localPosition.z + extendRate * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentExtend);
            }
            else
            {
                forward = false;
                reverse = true;
            }
            if(currentExtend > initialExtend && reverse)
            {
                currentExtend = transform.localPosition.z - extendRate * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentExtend);
            }
            else if(reverse)
            {
                attack = false;
            }
            
           
        }
    }

    public void Attack()
    {
        attack = true;
        forward = true;
        reverse = false;
        attackButton.interactable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(rb!=null)
        {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }
        
    }
}
