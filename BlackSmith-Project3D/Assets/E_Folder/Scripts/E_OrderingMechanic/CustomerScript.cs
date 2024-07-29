using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    [Header("Customers")]
    [SerializeField] GameObject[] CustomerPrefabs;
    [SerializeField] int ReputationAmountToSubractIfDeclinedOrder = 10;
    [Space]

    [Header("Positions and timing")]
    [SerializeField] GameObject CustomerSpawnPoint;
    [SerializeField] GameObject[] CustomerWaitingPointsInQueue;

    [SerializeField] float TimeToReachWaitingPoint = 3f;
    [Space]

    [Header("UI")]
    [SerializeField] GameObject AcceptOrDeclineCanvas;
    [SerializeField] GameObject PopoutMessageCanvas;

    private static List<GameObject> Customers = new List<GameObject>();
    private int customerQueue = 0;

    public static bool customerArrived;
    E_OrderingLogic orderingLogic;

    void Start()
    {
        orderingLogic = gameObject.GetComponent<E_OrderingLogic>();

        E_EventBus.CustomerArrival += CustomerArrival;
        E_EventBus.PlayerInteractedWithCustomer += AcceptOrDeclineCustomerOrder;
    }

    private void CustomerArrival()
    {
        if (customerQueue < CustomerWaitingPointsInQueue.Length && orderingLogic.CanWeStartNewOrder())
        {
            if (orderingLogic.currentNumOfSimultaneousOrders + customerQueue < orderingLogic.MaxSimultaneousOrders)
            {
                GameObject newCustomer = Instantiate(CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)], CustomerSpawnPoint.transform.position, Quaternion.identity);
                newCustomer.transform.DOMove(CustomerWaitingPointsInQueue[customerQueue].transform.position, TimeToReachWaitingPoint);

                Customers.Add(newCustomer);


                customerArrived = true;
                customerQueue++;
            }
        }
        else
        {
            Debug.LogWarning("No more waiting points available for new customers.");
        }

        //E_EventBus.CustomerArrival += CustomerArrival;
    }

    private void CustomerDeparture()
    {
        if (Customers.Count > 0)
        {
            GameObject customerToDepart = Customers[0];
            Customers.RemoveAt(0);
            customerQueue--;

            customerToDepart.transform.DOMove(CustomerSpawnPoint.transform.position, TimeToReachWaitingPoint).OnComplete(() => Destroy(customerToDepart));

            if (customerQueue == 0)
            {
                customerArrived = false;
                AcceptOrDeclineCanvas.SetActive(false);
            }
            else
            {
                orderingLogic.NewDialogue(); // Changing position in queue of other customers when first one leaves
                for (int i = 0; i < Customers.Count; i++)
                {
                    Customers[i].transform.DOMove(CustomerWaitingPointsInQueue[i].transform.position, TimeToReachWaitingPoint);
                }
            }
        }
        else
        {
            Debug.Log("No customers left");
        }
    }

    public void AcceptOrDeclineCustomerOrder()
    {
        AcceptOrDeclineCanvas.SetActive(true);
    }

    public void AcceptOrder()
    {
        if (orderingLogic.CanWeStartNewOrder())
        {
            orderingLogic.NewCustomerOrder();
            CustomerDeparture();
        }
        else
        {
            DeclineOrder();
            Sequence mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() => AcceptOrDeclineCanvas.SetActive(false));
            mySequence.AppendCallback(() => PopoutMessageCanvas.SetActive(true));
            mySequence.AppendInterval(3f);
            mySequence.AppendCallback(() => PopoutMessageCanvas.SetActive(false));

            if (customerQueue > 0)
            {
                mySequence.AppendCallback(() => AcceptOrDeclineCanvas.SetActive(true));
            }
        }
    }

    public void DeclineOrder()
    {
        Reputation.SubtractReputation(ReputationAmountToSubractIfDeclinedOrder);
        CustomerDeparture();
    }
}