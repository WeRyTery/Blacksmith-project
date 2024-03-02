using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_ExpandMetalScript : MonoBehaviour
{

    [SerializeField] private float RadiusOfCheckingOfBlocks = 1.5f;
    [SerializeField] private float RadiusOfCheckingOfAir = 3f;
    [SerializeField] private float ExpandScale;
    [SerializeField] private GameObject[] ObjectsInRadius = new GameObject[8];

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.localScale = transform.localScale * ExpandScale;
            for (int i = 0; i < ObjectsInRadius.Length; i++)
            {
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, RadiusOfCheckingOfBlocks);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusOfCheckingOfAir);
    }

}
