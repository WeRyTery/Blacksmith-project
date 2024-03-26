using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_OrderBookUI : MonoBehaviour
{
    [SerializeField] Canvas ordersBookCanvas;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.transform.CompareTag("Book"))
            {
                ordersBookCanvas.gameObject.SetActive(true);
            }
        }
    }

    public void CloseOrdersBook()
    {
        ordersBookCanvas.gameObject.SetActive(false);
    }
}
