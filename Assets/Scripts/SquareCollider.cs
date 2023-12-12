using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollision : MonoBehaviour
{
   public float sideLength; // Of square
    public Vector2 size;    // Of square
    private SpriteRenderer spriteRenderer;
    float speed = 5.0f;          // Simple float to hold object speed.

   void Start()
   {

   }

    void Update()
    {
        float dt = Time.deltaTime;          // Float that holds real time. 
        Vector3 direction = Vector3.zero;   // Every update when no controls are pressed, Brings all directional movement to zero.
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * dt);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * dt);  
        }
 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * dt); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * dt); 
        } 
    }
    //public bool CheckCollisionSquare(Vector2 point)
    //{
    //    Vector2 squareCenter = transform.position;
    //    Vector2 halfExtents = new Vector2(sideLength, sideLength) * 0.5f;

    //    float minX = squareCenter.x - halfExtents.x;
    //    float maxX = squareCenter.x + halfExtents.x;
    //    float minY = squareCenter.y - halfExtents.y;
    //    float maxY = squareCenter.y + halfExtents.y;

    //    return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
    //}
} 