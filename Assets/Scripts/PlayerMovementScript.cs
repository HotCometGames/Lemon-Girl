using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public float speed = 6;
    public float maxVelocity = 8f;


    public bool canJump = true;
    public float jumpStrength = 15f;
    public float maxJumpSpeed = 18f;

    public bool dead = false;

    void Start()
    {
        Physics.gravity = new Vector3(0, -25f, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.velocity += moveDir * speed;
                Vector3 rbNoUp = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                if (rbNoUp.magnitude > maxVelocity)
                {
                    rb.velocity = rbNoUp.normalized * maxVelocity + new Vector3(0, rb.velocity.y, 0);
                }
            }


            float jumpAdd = 0f;
            if(canJump && Input.GetButton("Jump"))
            {
                jumpAdd = jumpStrength;
                transform.position += new Vector3(0, 0.1f, 0);
                canJump = false;
            }
            rb.velocity += new Vector3(0, jumpAdd, 0);
            if (rb.velocity.y > maxJumpSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, maxJumpSpeed, rb.velocity.z);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        int targetLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer != targetLayer)
        {
            canJump = true;
        }
        
    }

    void OnCollisionStay(Collision collision)
    {
        int targetLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer != targetLayer)
        {
            canJump = true;
        }
    }
}
