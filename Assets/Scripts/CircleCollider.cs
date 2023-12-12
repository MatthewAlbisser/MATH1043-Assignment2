using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : MonoBehaviour
{
    public float radius;    // Of circle
    private SpriteRenderer spriteRenderer;
    float speed = 5.0f;          // Simple float to hold object speed.

    void Start()
    {

    }

    void Update()
    {
        float dt = Time.deltaTime;          // Float that holds real time. 
        Vector3 direction = Vector3.zero;   // Every update when no controls are pressed, Brings all directional movement to zero.
        if (Input.GetKey(KeyCode.T))
        {
            transform.Translate(Vector3.up * speed * dt);    
        }
        else if (Input.GetKey(KeyCode.G))
        {
            transform.Translate(Vector3.down * speed * dt);  
        }

        if (Input.GetKey(KeyCode.F))
        {
            transform.Translate(Vector3.left * speed * dt);  
        }
        else if (Input.GetKey(KeyCode.H))
        {
            transform.Translate(Vector3.right * speed * dt); 
        } 
    }


    //public bool CheckCollisionCircle(Vector2 point)
    //{
    //    Vector2 circleCenter = transform.position;

    //    float distance = Vector2.Distance(point, circleCenter);
    //    return distance <= radius;
    //}
}
