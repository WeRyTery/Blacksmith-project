using UnityEngine;
using System.Collections.Generic;

public class FinderOf : MonoBehaviour
{
    private void Start()
    {
        List<GameObject> objectsWithScript = new List<GameObject>();

        // �������� ��� ������� �� �����
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // ���������� �� ������� �������
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<E_CameraManagment>() != null)
            {
                Debug.Log(obj.name);
            }
        }
    }
}