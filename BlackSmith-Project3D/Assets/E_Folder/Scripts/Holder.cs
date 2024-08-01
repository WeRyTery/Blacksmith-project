using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private GameObject UIHolder;

    private bool IsHolderActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsHolderActive == false)
            {
                UIHolder.SetActive(true);
                IsHolderActive = true;
            }
            else
            {
                UIHolder.SetActive(false);
                IsHolderActive = false;
            }

        }

    }
}
