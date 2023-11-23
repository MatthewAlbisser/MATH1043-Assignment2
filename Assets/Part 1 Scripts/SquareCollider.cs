using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollision : MonoBehaviour
{
   public float sideLength; //of square
    public Vector2 size; // Size of the square
    private SpriteRenderer spriteRenderer;
    float speed = 5.0f;          // Simple float to hold object speed.

   void Start()
   {

   }

    void Update()
   {
      float dt = Time.deltaTime;      //updating float that holds real time. 
      Vector3 direction = Vector3.zero;   // Every update when no controls are pressed, Brings and holds all the directional force back to zero.
      if (Input.GetKey(KeyCode.W))
      {
         transform.Translate(Vector3.up * speed * dt);    // Push Up
      }
         else if (Input.GetKey(KeyCode.S))
      {
         transform.Translate(Vector3.down * speed * dt);  // Push down
      }
 
         if (Input.GetKey(KeyCode.A))
      {
         transform.Translate(Vector3.left * speed * dt);  // Push Left
      }
        else if (Input.GetKey(KeyCode.D))
      {
         transform.Translate(Vector3.right * speed * dt); // Push Right
      } 
    }
    // Checks for collision on square size from center
    public bool CheckCollision(Vector2 point)
   {
        Vector2 squareCenter = transform.position;
        Vector2 halfExtents = new Vector2(sideLength, sideLength) * 0.5f;

        float minX = squareCenter.x - halfExtents.x;
        float maxX = squareCenter.x + halfExtents.x;
        float minY = squareCenter.y - halfExtents.y;
        float maxY = squareCenter.y + halfExtents.y;

        return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
   }
} 