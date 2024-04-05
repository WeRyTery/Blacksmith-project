using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeformScript : MonoBehaviour
{
    [SerializeField] private Transform[] BonesLeft, BonesRight, BonesBottom, BonesTop;
    [SerializeField] private float StrechForce;
    [SerializeField] private float DistanceToMoveCollider;
    [SerializeField] private string NameOfLeftCollier,NameOfRightCollier,NameOfTopCollier,NameOfBottomCollier;
    //variables for max distance which bones had already get to
    [SerializeField] private float TopDistanceOfLeftBones,TopDistanceOfRightBones,TopDistanceOfTopBones,TopDistanceOfBottomBones;
    [SerializeField] private float RadiusOfHummer;

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // reset Z position
            MousePosition.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Left");

                ActionOnHit(MousePosition, hit);
            }
        }
    }

    void ActionOnHit(Vector3 MousePosition, RaycastHit2D hit)
    {
        if (hit.collider.gameObject.name == NameOfLeftCollier)
        {
            for (int i = 0;i < BonesLeft.Length;i++)
            {
                if (Vector2.Distance(MousePosition, BonesLeft[i].transform.position) <= RadiusOfHummer)
                {
                    BonesLeft[i].position = new Vector2(BonesLeft[i].position.x - StrechForce, BonesLeft[i].position.y);
                }
            }
            hit.collider.gameObject.transform.position = new Vector2(hit.collider.gameObject.transform.position.x - DistanceToMoveCollider, hit.collider.gameObject.transform.position.y);
        }
        else if (hit.collider.gameObject.name == NameOfRightCollier)
        {
            for (int i = 0; i < BonesRight.Length; i++)
            {
                if (Vector2.Distance(MousePosition, BonesRight[i].transform.position) <= RadiusOfHummer)
                {
                    BonesRight[i].position = new Vector2(BonesRight[i].position.x + StrechForce, BonesRight[i].position.y);
                }
            }
            hit.collider.gameObject.transform.position = new Vector2(hit.collider.gameObject.transform.position.x + DistanceToMoveCollider, hit.collider.gameObject.transform.position.y);
        }
        else if (hit.collider.gameObject.name == NameOfTopCollier)
        {
            for (int i = 0; i < BonesTop.Length; i++)
            {
                if (Vector2.Distance(MousePosition, BonesTop[i].transform.position) <= RadiusOfHummer)
                {
                    BonesTop[i].position = new Vector2(BonesTop[i].position.x, BonesTop[i].position.y + StrechForce);
                }
            }
            hit.collider.gameObject.transform.position = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + DistanceToMoveCollider);
        }
        else if (hit.collider.gameObject.name == NameOfBottomCollier)
        {
            for (int i = 0; i < BonesBottom.Length; i++)
            {
                if (Vector2.Distance(MousePosition, BonesBottom[i].transform.position) <= RadiusOfHummer)
                {
                    BonesBottom[i].position = new Vector2(BonesBottom[i].position.x, BonesBottom[i].position.y - StrechForce);
                }
            }
            hit.collider.gameObject.transform.position = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y - DistanceToMoveCollider);
        }
        else
        {

        }
    }
}
