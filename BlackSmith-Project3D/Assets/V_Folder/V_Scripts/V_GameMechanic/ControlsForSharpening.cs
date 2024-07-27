using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsForSharpening : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode LeftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode RightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode ContactKey = KeyCode.Space;
    [SerializeField] private KeyCode FlipKey = KeyCode.E;
    [Space]
    [Header("Move on z axis")]
    [SerializeField] private float MaxPositionLeft; //-9.65 //-3.65 //130.591 //130.093
    [SerializeField] private float MaxPositionRight;
    [SerializeField] private float AddPositionLeft = 0.02f;
    [SerializeField] private float AddPositionRight = 0.02f;
    [Space]
    [Header("Get closer to wheel")]
    [SerializeField] private float DistanceOfContact;
    [SerializeField] private float StartPosition;
    [Space]
    [Header("Flip Tool")]
    [SerializeField] private bool Turned;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, StartPosition); // idk why it doesn't work with simple version of transform.position.x = StartPosition...
    }

    void Update()
    {
        if (Input.GetKeyDown(FlipKey))
        {
            transform.Rotate(new Vector3(180, 0, 180));

        }

        if (Input.GetKey(LeftKey) && transform.position.x > MaxPositionLeft)
        {
            transform.position = new Vector3(transform.position.x - AddPositionLeft, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(RightKey) && transform.position.x < MaxPositionRight)
        {
            transform.position = new Vector3(transform.position.x + AddPositionRight, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(ContactKey))
        {
            Debug.Log("Start Space");
            transform.position = new Vector3(transform.position.x, transform.position.y, DistanceOfContact);
        }
        else if (Input.GetKeyUp(ContactKey))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, StartPosition);
            Debug.Log("Stop Space");
        }
        else
        {
            Debug.Log("No Space");
        }

    }
}
