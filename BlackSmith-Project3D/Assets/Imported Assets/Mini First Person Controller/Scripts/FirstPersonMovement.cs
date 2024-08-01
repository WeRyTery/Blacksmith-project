using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float MovementSpeed = 5;
    [SerializeField] Transform cam;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Inputs
        float horInput = Input.GetAxis("Horizontal") * MovementSpeed;
        float verInput = Input.GetAxis("Vertical") * MovementSpeed;

        //Camera direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        //Creating relative camera direction
        Vector3 forwardRelative = verInput * camForward;
        Vector3 rightRelative = horInput * camRight;

        Vector3 movementDirection = forwardRelative + rightRelative;

        //Movement
        rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);

        
        if (!Mathf.Approximately(rb.velocity.magnitude, 0)) //When speed == ~0, character wont turn away and face static position
        {
            transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);   
        }
    }
}
