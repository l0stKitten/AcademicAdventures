using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DepthSortByY : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the order in layer based on the y-coordinate of the object
        int orderInLayer = -(int)(transform.position.y * 100);
        
        // Add a secondary sorting based on the instance ID to handle equal y-coordinates
        orderInLayer += GetInstanceID() % 100;

        spriteRenderer.sortingOrder = orderInLayer;
    }
}