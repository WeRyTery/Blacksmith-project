using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliderOfSord : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the PolygonCollider2D component
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Update collider shape initially
        UpdateCollider();
    }

    void Update()
    {
        // Check if the sprite has changed
        if (spriteRenderer.sprite != null && spriteRenderer.sprite.texture != null)
        {
            // Update collider shape
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        // Clear existing collider points
        polygonCollider.pathCount = 0;

        // Get the sprite's vertices in local space
        Vector2[] spriteVertices = spriteRenderer.sprite.vertices;

        // Convert vertices to world space
        for (int i = 0; i < spriteVertices.Length; i++)
        {
            spriteVertices[i] = transform.TransformPoint(spriteVertices[i]);
        }

        // Update the collider's points to match the sprite's vertices
        polygonCollider.SetPath(0, spriteVertices);
    }
}
