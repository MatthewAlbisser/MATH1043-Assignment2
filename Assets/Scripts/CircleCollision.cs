using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CircleCollision : MonoBehaviour
{
    public float radius;         // Part1: Of circle
    private Vector3 objectSize;                     // Part2: Vector for Initial object size.
    private Vector3 velocity = Vector3.zero;         // Part2: Vector for Initial velocity.

    private float jumpHeight = 10.0f;        // Part3:
    private float jumpDuration = 0.5f;       // Part3:
    private float jumpStartTime;            // Part3:
    private bool isJumping = false;         // Part3:
    private Vector3 initialPosition;        // Part3:
    private float circleRadius = 0.5f;       // Part3:

    void Start()
    {
        objectSize = transform.localScale;          // Part2: Vector for this objects size saved as variable on game start.
        initialPosition = transform.position;
    }

    void Update()
    {
        float dt = Time.deltaTime;              // Part1: Float that holds real time.



        //---GRAVITY FORCE---//

        float mass = 15f;          // Part2: Declared float for mass.
        float gravity = 9.8f;       // Part2: Declared float for gravity.
        Vector3 force = mass * gravity * Vector3.down;          // Part2: Calculates this objects downward force using mass and gravity. 
        Vector3 acceleration = force / mass;                    // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * dt;                          // Part2: Updates velocity using acceleration and real time.
        transform.position += velocity * dt;                    // Part2: Updates object position based on velocity and real time.



        //---GROUND COLLISION---//

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



        //---CONTROL VECTORS---//

        Vector3 direction = Vector3.zero;           // Part2: When no key is pressed, all player created directional force reverts back to zero.
        float speed = 5f;                           // Part3: Simple float to hold object speed. Readonly.

        if (Input.GetKey(KeyCode.T))                // Part2: If set key is pressed...
        {
            direction += Vector3.up * speed * dt;   // Part2: Object moves in set direction with a magnitude of speed by time. 
        }
        else if (Input.GetKey(KeyCode.G))
        {
            direction += Vector3.down * speed * dt;
        }
        if (Input.GetKey(KeyCode.F))
        {
            direction += Vector3.left * speed * dt;
        }
        else if (Input.GetKey(KeyCode.H))
        {
            direction += Vector3.right * speed * dt;
        }
        transform.position += direction;                // Part2: Applies velocity to object position.



        //---IMPULSE JUMP---//

        if (isJumping)                                                                          // Part3: If isJumping is activated...
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;                    // Part3: Float to constantly update (time minus jumpStartTime) divided by jumpDiraction.
            if (jumpProgress <= 1.0f)                                                           // Part3: If jumpProgress is less or equal to 1.0f...
            {
                float jumpDistance = jumpHeight * (1 - Mathf.Pow((2 * jumpProgress - 1), 3));   // Part3: Quadratic jump curve
                transform.position = initialPosition + Vector3.up * jumpDistance;               // Part3:
            }
            else
            {
                isJumping = false;                          // Part3: Else isJumping is deactivated.
                transform.position = initialPosition;       // Part3: Reset the object's position after the jump
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            if (CircleCollider(worldMousePos, transform.position, circleRadius))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    jumpStartTime = Time.time;
                }
            }
        }
    }

    bool CircleCollider(Vector3 point, Vector3 circleCenter, float radius)
    {
        float distanceSquared = (point.x - circleCenter.x) * (point.x - circleCenter.x) +           // Calculates the distance between the point and the center of the circle
                                (point.y - circleCenter.y) * (point.y - circleCenter.y);

        return distanceSquared <= (radius * radius);                                            // Returns true if the distance is less than or equal to the radius squared
    }
}
