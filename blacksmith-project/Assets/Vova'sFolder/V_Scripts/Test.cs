using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform[] BonesOfSword;
    [Space]
    [Header("Radius")]
    [SerializeField] private float FirstCircleRadius, SecondCircleRadius, ThirdCircleRadius;
    [Space]
    [Header("Power")]
    [SerializeField] private float FirstCirclePower, SecondCirclePower, ThirdCirclePower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 MousePositionOnScreen = Input.mousePosition;

            Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(MousePositionOnScreen);
            Debug.Log(MouseWorldPosition);
            for (int i = 0; i < BonesOfSword.Length; i++)
            {
                if (Vector2.Distance(MouseWorldPosition, BonesOfSword[i].position) <= FirstCircleRadius)
                {
                    Vector2 DirectionOfStretch = BonesOfSword[i].position - MouseWorldPosition;
                    BonesOfSword[i].position = new Vector2(BonesOfSword[i].position.x + (DirectionOfStretch.x * FirstCirclePower), BonesOfSword[i].position.y + (DirectionOfStretch.y * FirstCirclePower));
                    Debug.Log(BonesOfSword[i].position);
                    Debug.Log("Stretch a lot");
                }
                //else if (Vector2.Distance(MouseWorldPosition, BonesOfSword[i].position) <= SecondCircleRadius)
                //{
                //    Vector2 DirectionOfStretch = BonesOfSword[i].position - MouseWorldPosition;
                //    BonesOfSword[i].position = new Vector2(BonesOfSword[i].position.x + (DirectionOfStretch.x * SecondCirclePower), BonesOfSword[i].position.y + (DirectionOfStretch.y * SecondCirclePower));
                //    Debug.Log(BonesOfSword[i].position);
                //    Debug.Log("Stretch a small amount");
                //}
                //else if (Vector2.Distance(MouseWorldPosition, BonesOfSword[i].position) <= ThirdCircleRadius)
                //{
                //    Vector2 DirectionOfStretch = BonesOfSword[i].position - MouseWorldPosition;
                //    BonesOfSword[i].position = new Vector2(BonesOfSword[i].position.x + (DirectionOfStretch.x * ThirdCirclePower), BonesOfSword[i].position.y + (DirectionOfStretch.y * ThirdCirclePower));
                //    Debug.Log(BonesOfSword[i].position);
                //    Debug.Log("Stretch a lit");
                //}
                else
                {
                    Debug.Log("Nothing Changed");
                }
            }
        }
    }
}
