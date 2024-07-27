using UnityEngine;
using System.Collections.Generic;

public class FinderOf : MonoBehaviour
{
    private void Start()
    {
        List<GameObject> objectsWithScript = new List<GameObject>();

        // Получаем все объекты на сцене
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // Проходимся по каждому объекту
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<E_CameraManagment>() != null)
            {
                Debug.Log(obj.name);
            }
        }
    }
}