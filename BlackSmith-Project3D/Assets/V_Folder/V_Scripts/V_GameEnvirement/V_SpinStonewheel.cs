using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_SpinStonewheel : MonoBehaviour
{
    [SerializeField] private float RotationPrecent;
    void Update()
    {
        transform.Rotate(0 ,RotationPrecent ,0);
    }
}
