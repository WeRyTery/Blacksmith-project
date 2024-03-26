using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void TransformToScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        
    }
}
