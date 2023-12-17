using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CapsuleCollision : MonoBehaviour
{
    private Vector3 objectSize;                     // Part2: Vector for Initial object size.
    private Vector3 velocity = Vector3.zero;        // Part2: Vector for Initial velocity.
    private Vector3 acceleration = Vector3.zero;    // Part2: Vector for Initial acceleration.
    private Vector3 force = Vector3.zero;           // Part2: Vector for Initial force.

    private float jumpHeight = 10.0f;   // Part3:
    private float jumpDuration = 0.5f;  // Part3:
    private float capsuleRadius = 0.5f; // Part3:
    private float capsuleHeight = 2.0f; // Part3:

    private bool isJumping = false;
    private float jumpStartTime;
    private Vector3 initialPosition;


    void Start()
    {
        objectSize = transform.localScale;          // Part2: Vector for this objects size saved as variable on game start.
        initialPosition = transform.position;
    }

    void Update()
    {
        float dt = Time.deltaTime;                  // Part1: Float that holds real time.


    //---GRAVITY FORCE---//


        float mass = 30f;       // Part2: Declared float for mass.
        float gravity = 9.8f;   // Part2: Declared float for gravity.

        Vector3 gravityMass = mass * Vector3.down * gravity;    // Part2: Calculates this objects downward force and calls it gravityMass.
        force += gravityMass;                                   // Part2: Apllies gravityMass to objects force vector.

        acceleration = force / mass;                            // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * dt;                          // Part2: Calculates velocity using acceleration and real time.
        transform.position += velocity * dt;                    // Part2: Updates object position based on velocity and real time.

        force = Vector3.zero;                                   // Part2: Reverts back force vector every frame; doesnt stack velocity.
                                                                //---GROUND COLLISION---//
        Ground ground = FindObjectOfType<Ground>();             // Part2: Declares a variable for the Ground game object.

        bool hasCollided = ground.GroundCollision(transform.position, objectSize.y * 2);    // Part2: Declares a bool for the activation of the GroundCollision method in the Ground script.
        if (hasCollided)
        {
            velocity = Vector3.zero;                                                // Part2: Velcocity for this object is reset.
            acceleration = Vector3.zero;                                            // Part2: Acceleration for this object is reset.
            float radiusY = objectSize.y;                                           // Part2: Float to hold the Y axis points of the falling object.
            float distanceToGround = Mathf.Abs(transform.position.y - radiusY);     // Part2: Float to hold the remaining Y axis distance from the ground.
            Vector3 upOffset = Vector3.up * distanceToGround;                       // Part2: Declares vector thats pushing upwards based on the total distance from the ground. 

            transform.position += upOffset;                                         // Part2: Objects position and upOffset are added, replacing the objects position.
        }

        //---CONTROL VECTORS---//

        Vector3 direction = Vector3.zero;           // Part2: When no key is pressed, all player created directional force reverts back to zero.
        float speed = 5f;                           // Part3: Simple float to hold object speed. Readonly.

        if (Input.GetKey(KeyCode.I))                // Part2: If set key is pressed...
        {
            direction += Vector3.up * speed * dt;   // Part2: Object moves in set direction with a magnitude of speed by time. 
        }
        else if (Input.GetKey(KeyCode.K))
        {
            direction += Vector3.down * speed * dt;
        }
        if (Input.GetKey(KeyCode.J))
        {
            direction += Vector3.left * speed * dt;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            direction += Vector3.right * speed * dt;
        }
        transform.position += direction;            // Part2: Applies velocity to object position.

        if (isJumping)
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;

            if (jumpProgress <= 1.0f)
            {
                float jumpDistance = jumpHeight * (1 - Mathf.Pow((2 * jumpProgress - 1), 4)); // Simple quadratic jump curve
                transform.position = initialPosition + Vector3.up * jumpDistance;
            }
            else
            {
                isJumping = false;
                transform.position = initialPosition;
            }
        }


        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (CapsuleCollider(mousePos, (Vector3)transform.position, capsuleRadius, capsuleHeight))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    jumpStartTime = Time.time;
                }
            }
        }
    }

    bool CapsuleCollider(Vector2 point, Vector2 capsuleCenter, float radius, float height)
    {
        float distanceX = Mathf.Abs(point.x - capsuleCenter.x);
        float distanceY = Mathf.Abs(point.y - capsuleCenter.y);

        if (distanceX <= radius && (distanceY >= height * 0.5f - radius || distanceY <= radius - height * 0.5f))
        {
            return true;
        }

        return false;
    }
}


