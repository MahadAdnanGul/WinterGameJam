using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private float cheat_probability = 0;

    [Header("Character Settings")]
    private Rigidbody rb;
    private Quaternion old_rot;
    private Quaternion target_rot;
    private float frame_count;
    private bool disabled = false;
    private float disableTimer = 0f;
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        old_rot = transform.rotation;
        movement = GetComponent<PlayerMovement>();
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
        float x = rot_modifier > 0 ? 1 : -1;
        float target_angle = transform.rotation.eulerAngles.y - x * (angle);
        float new_y = Mathf.LerpAngle(transform.rotation.eulerAngles.y, target_angle, movement.speed * movement.turn_rate * Time.deltaTime * 0.1f);

        //Quaternion target_rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rot_modifier*(angle - 1), 0);

        Debug.DrawRay(transform.position, waypoint.transform.position - transform.position);
        Quaternion new_rot = Quaternion.Euler(0, new_y, 0);
        if (transform.position.y < waypoint.transform.position.y - 0.2f)
        {
            movement.Rocket();
        }
        movement.Move(new_rot);
        
        





    }
    private void OnTriggerEnter(Collider other)
    {

        if (waypoint != null && other.gameObject.GetInstanceID() == waypoint.gameObject.GetInstanceID())
        {
            if(waypoint.shortcut != null && Random.Range(0.0f, 1.0f) < cheat_probability)
            {
                waypoint = waypoint.shortcut;
            }
            else
            {
                waypoint = waypoint.next;
                if (!waypoint)
                {
                    rb.drag = 3;
                }
            }
            
        }

    }
}
