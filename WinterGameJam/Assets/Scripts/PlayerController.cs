using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float turn_rate = 1;
    [SerializeField] private float speed = 1;
    [SerializeField] private float jump_height = 3;

    private Rigidbody rb;
    private CapsuleCollider col;
    private float dist_to_ground;
    private float rotate;
    private bool jump;
    private float forward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        dist_to_ground = col.bounds.extents.y + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        rotate = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical"); // Not currently using this
        jump = Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, -Vector3.up, dist_to_ground);
        if (jump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump_height, rb.velocity.z);
        }
        rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotate * turn_rate, 0));
        //rb.angularVelocity = Vector3.zero;
        //rb.MovePosition(transform.position + transform.forward * speed);
        rb.velocity = (transform.forward * speed) + new Vector3(0, rb.velocity.y, 0);
    }

}
