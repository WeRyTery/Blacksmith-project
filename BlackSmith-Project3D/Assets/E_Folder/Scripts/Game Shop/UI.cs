using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject Canvas;

    public void CloseCanvas()
    {
        Canvas.SetActive(false);
    }
}
