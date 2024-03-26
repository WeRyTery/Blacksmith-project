using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float Expand;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localScale = transform.localScale * Expand;
        }
    }
}
