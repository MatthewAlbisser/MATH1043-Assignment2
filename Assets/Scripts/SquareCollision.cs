using UnityEngine;

public class SquareCollision : MonoBehaviour
{
    public float sideLength;        // Part1: Of square
    public Vector2 size;            // Part1: Of square
    private Vector3 objectSize;                 // Part2: Vector for Initial object size.
    private Vector3 velocity = Vector3.zero;    // Part2: Vector for Initial velocity.

    private float jumpHeight = 10.0f;       // Part3:
    private float jumpDuration = 0.5f;      // Part3:
    private float jumpStartTime;            // Part3:
    private bool isJumping = false;         // Part3:
    private Vector3 initialPosition;        // Part3:
    private float squareHalfLength = 0.5f;  // Part3:

    void Start()
    {
        objectSize = transform.localScale;      // Part2: Vector for this objects size saved as variable on game start.
        initialPosition = transform.position;   // Part3: 
    }

    void Update()
    {
        float dt = Time.deltaTime;          // Part1: Float that holds real time.



        //---GRAVITY FORCE---//


        float mass = 10f;                               // Part2: Declared float for mass.
        float gravity = 9.8f;                           // Part2: Declared float for gravity.
        Vector3 force = mass * gravity * Vector3.down;  // Part2: Calculates this objects downward force using mass and gravity. 
        Vector3 acceleration = force / mass;            // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * dt;                  // Part2: Updates velocity using acceleration and real time.
        transform.position += velocity * dt;            // Part2: Updates object position based on velocity and real time.



        //---GROUND COLLISION---//

        Ground ground = FindObjectOfType<Ground>();                                     // Part2: Declares a variable for the Ground game object.
        bool hasCollided = ground.GroundCollision(transform.position, objectSize.y);    // Part2: Declares a bool for the activation of the GroundCollision method in the Ground script.
        if (hasCollided)
        {
            velocity = Vector3.zero;                                            // Part2: Velcocity for this object is reset.
            float radiusY = objectSize.y;                                   // Part2: Float to hold the Y axis points of the falling object.
            float distanceToGround = Mathf.Abs(transform.position.y - radiusY); // Part2: Float to hold the remaining Y axis distance from the ground.
            Vector3 upOffset = Vector3.up * distanceToGround;                   // Part2: Declares vector thats pushing upwards based on the total distance from the ground. 
            transform.position += upOffset;                                     // Part2: Objects position and upOffset are added, replacing the objects position.
        }



        //---CONTROL VECTORS---//

        Vector3 direction = Vector3.zero;           // Part2: When no key is pressed, all player created directional force reverts back to zero.
        float speed = 5f;                           // Part3: Simple float to hold object speed. Readonly.

        if (Input.GetKey(KeyCode.W))                // Part2: If set key is pressed...
        {
            direction += Vector3.up * speed * dt;   // Part2: Object moves in set direction with a magnitude of speed by time. 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down * speed * dt;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left * speed * dt;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right * speed * dt;
        }
        transform.position += direction;                // Part2: Applies velocity to object position.



        //---IMPULSE JUMP---//

        if (isJumping)                                                                          // Part3: If isJumping is activated...
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;                    // Part3: Float to constantly update (time minus jumpStartTime) divided by jumpDiraction.
            if (jumpProgress <= 1.5f)                                                           // Part3: If jumpProgress is less or equal to 1.0f...
            {
                float jumpDistance = jumpHeight * (1 - Mathf.Pow((2 * jumpProgress - 1), 4));   // Part3: Quadratic jump curve
                transform.position = initialPosition + Vector3.up * jumpDistance;               // Part3:
            }
            else
            {
                isJumping = false;                          // Part3: Else isJumping is deactivated.
                transform.position = initialPosition;       // Part3: Reset the object's position after the jump
            }
        }

        if (Input.GetMouseButtonDown(0))                                        // Part3: If left mouse is clicked...
        {
            Vector3 mousePos = Input.mousePosition;                             // Part3: Create a vector for last mouse click position.
            mousePos.z = Camera.main.transform.position.z;                      // Part3: Vectors Z value is set to camera Z value.

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);   // Part3: New vector for screenspace position using clicked position.
            if (SquareCollider(worldMousePos, transform.position, squareHalfLength))
            {
                if (!isJumping)                 // Part3: If not jumping...
                {
                    isJumping = true;           // Part3: Is jumping is set to true.
                    jumpStartTime = Time.time;  // Part3: Sets float to hold time.
                }
            }
        }
    }


    bool SquareCollider(Vector3 point, Vector3 squareCenter, float squareHalfLength)
    {
        float minX = squareCenter.x - squareHalfLength;
        float maxX = squareCenter.x + squareHalfLength;
        float minY = squareCenter.y - squareHalfLength;
        float maxY = squareCenter.y + squareHalfLength;


        if (point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY)   // Part3: Check if the mouse click is within the boundaries of the square.
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}