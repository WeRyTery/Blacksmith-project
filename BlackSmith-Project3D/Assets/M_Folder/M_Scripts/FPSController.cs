using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSController : MonoBehaviour
{
    private int targetFPS = 60;

    void Start()
    {
        // Устанавливаем начальный FPS лимит
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    public void Set60FPS()
    {
        targetFPS = 60;
        ApplyFPSLimit();
    }

    public void Set120FPS()
    {
        targetFPS = 120;
        ApplyFPSLimit();
    }

    private void ApplyFPSLimit()
    {
        Application.targetFrameRate = targetFPS;
        Debug.Log("FPS limit set to: " + targetFPS);
    }
}

