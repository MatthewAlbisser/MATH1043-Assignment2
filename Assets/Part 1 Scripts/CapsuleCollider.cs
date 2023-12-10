using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleCollision : MonoBehaviour
{
    public float radius;    // Of capsule
    public float height;    // Of capsule
    private SpriteRenderer spriteRenderer;
    float speed = 5.0f;     // Simple float to hold object speed.

    void Start()
    {

    }

    void Update()
    {
        float dt = Time.deltaTime;          // Float that holds real time. 
        Vector3 direction = Vector3.zero;   // Every update when no controls are pressed, Brings all directional movement to zero.
        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.up * speed * dt);    
        }
        else if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(Vector3.down * speed * dt);  
        }

        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(Vector3.left * speed * dt);  
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.right * speed * dt);
        } 
    }
    //public bool CheckCollisionCapsule(Vector2 point)
    //{
    //    Vector2 capsuleCenter = transform.position;
    //    float minY = capsuleCenter.y - height * 0.5f;
    //    float maxY = capsuleCenter.y + height * 0.5f;

    //    Vector2 pointOnLine = new Vector2(point.x, Mathf.Clamp(point.y, minY, maxY));
    //    float distance = Vector2.Distance(point, pointOnLine);

    //    return distance <= radius;
    //}
}