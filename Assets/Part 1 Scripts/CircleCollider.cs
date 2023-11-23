using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : MonoBehaviour
{
    public float radius;    // Of circle
    private SpriteRenderer spriteRenderer;
    float speed = 5.0f;          // Simple float to hold onject speed.

    void Start()
    {
        
    }

   void Update()
    {
        float dt = Time.deltaTime;      //updating float that holds real time. 
        Vector3 direction = Vector3.zero;   // Every update when no controls are pressed, Brings and holds all the directional force back to zero.
        if (Input.GetKey(KeyCode.T))
        {
            transform.Translate(Vector3.up * speed * dt);    // Push Up
        }
        else if (Input.GetKey(KeyCode.G))
        {
            transform.Translate(Vector3.down * speed * dt);  // Push down
        }

        if (Input.GetKey(KeyCode.F))
        {
            transform.Translate(Vector3.left * speed * dt);  // Push Left
        }
        else if (Input.GetKey(KeyCode.H))
        {
            transform.Translate(Vector3.right * speed * dt); // Push Right
        } 
    }

        // Checks for collision on circle size from center
    public bool CheckCollision(Vector2 point)
    {
        Vector2 circleCenter = transform.position;

        float distance = Vector2.Distance(point, circleCenter);
        return distance <= radius;
    }
}
