using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : MonoBehaviour
{
    public float radius;         // Part1: Of circle
    float speed = 5.0f;          // Part1: Simple float to hold object speed.

    private Vector3 objectSize;                     // Part2: Vector for Initial object size.
    public Vector3 velocity = Vector3.zero;         // Part2: Vector for Initial velocity.
    private Vector3 acceleration = Vector3.zero;    // Part2: Vector for Initial acceleration.
    private Vector3 force = Vector3.zero;           // Part2: Vector for Initial force.

    void Start()
    {
        objectSize = transform.localScale;          // Part2: Vector for this objects size saved as variable on game start.
    }

    void Update()
    {
        float dt = Time.deltaTime;              // Part1: Float that holds real time.
    //---GRAVITY FORCE---//
        float mass = 15000f;        // Part2: Declared float for mass.
        float gravity = 9.8f;    // Part2: Declared float for gravity.

    Vector3 gravityMass = mass * Vector3.down * gravity;    // Part2: Calculates this objects downward force and calls it gravityMass.
        force += gravityMass;                                   // Part2: Apllies gravityMass to objects force vector.

        acceleration = force / mass;                            // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * dt;                          // Part2: Calculates velocity using acceleration and real time.
        transform.position += velocity * dt;                    // Part2: Updates object position based on velocity and real time.

        force = Vector3.zero;                                   // Part2: Reverts back force vector every frame; doesnt stack velocity.
    //---GROUND COLLISION---//                                 // Part2: Resets every frame, keeps it honest. 
        Ground ground = FindObjectOfType<Ground>();             // Part2: Declares a variable for the Ground game object.

        bool hasCollided = ground.GroundCollision(transform.position, objectSize.y);    // Part2: Declares a bool for the activation of the GroundCollision method in the Ground script.
        if (hasCollided)
        {
            velocity = Vector3.zero;                                                // Part2: Velcocity for this object is reset.
            acceleration = Vector3.zero;                                            // Part2: Acceleration for this object is reset.
            float radiusY = objectSize.y / 2;                                       // Part2: Float to hold the Y axis points of the falling object.
            float distanceToGround = Mathf.Abs(transform.position.y - radiusY);     // Part2: Float to hold the remaining Y axis distance from the ground.
            Vector3 upOffset = Vector3.up * distanceToGround;                       // Part2: Declares vector thats pushing upwards based on the total distance from the ground. 

            transform.position += upOffset;                                         // Part2: Objects position and upOffset are added, replacing the objects position.
        }
        Vector3 direction = Vector3.zero;               // Part1: No button pressed = all directional movement to zero.
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
}
