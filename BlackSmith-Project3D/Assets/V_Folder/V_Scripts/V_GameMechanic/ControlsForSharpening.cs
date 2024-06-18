using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsForSharpening : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode UpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode DownKey = KeyCode.DownArrow;
    [SerializeField] private KeyCode ContactKey = KeyCode.Space;
    [SerializeField] private KeyCode FlipKey = KeyCode.E;
    [Space]
    [Header("Move on z axis")]
    [SerializeField] private float MaxPositionUp;
    [SerializeField] private float MaxPositionDown;
    [SerializeField] private float AddPositionUp;
    [SerializeField] private float AddPositionDown;
    [Space]
    [Header("Get closer to wheel")]
    [SerializeField] private float DistanceOfContact;
    [SerializeField] private float StartPosition;
    [Space]
    [Header("Flip Tool")]
    [SerializeField] private bool Turned;
    void Start()
    { 
        StartPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FlipKey))
        {
            transform.Rotate(new Vector3(180,90,0));

        }
        if (Input.GetKey(UpKey) && transform.position.z < MaxPositionUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z + AddPositionUp);
        }
        else if (Input.GetKey(DownKey) && transform.position.z > MaxPositionDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + AddPositionDown);
        }
        if(Input.GetKey(ContactKey))
        {
            Debug.Log("Start Space");
            transform.position = new Vector3(DistanceOfContact, transform.position.y, transform.position.z);
        }
        else if(Input.GetKeyUp(ContactKey))
        {
            transform.position = new Vector3(StartPosition, transform.position.y, transform.position.z);
            Debug.Log("Stop Space");
        }
        else
        {
            Debug.Log("No Space");
        }

    }
}
