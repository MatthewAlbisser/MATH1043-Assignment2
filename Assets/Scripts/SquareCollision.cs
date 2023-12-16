using UnityEngine;

public class SquareCollision : MonoBehaviour
{
    public float sideLength;        // Part1: Of square
    public Vector2 size;            // Part1: Of square
    readonly float speed = 5.0f;    // Part1: Simple float to hold object speed. Readonly.

    float mass = 10f;          // Part2: Declared float for mass.
    float gravity = 9.8f;       // Part2: Declared float for gravity.
    private Vector3 objectSize;                     // Part2: Vector for Initial object size.
    public Vector3 velocity = Vector3.zero;         // Part2: Vector for Initial velocity.

    public float jumpHeight = 10.0f;    // Part3:
    public float jumpDuration = 0.01f;  // Part3:
    private float jumpStartTime;        // Part3:
    private bool isJumping = false;     // Part3:
    private Vector3 initialPosition;    // Part3:

    void Start()
    {
        objectSize = transform.localScale;  // Part2: Vector for this objects size saved as variable on game start.
    }

    void Update()
    {
        float dt = Time.deltaTime;  // Part1: Float that holds real time.

    //---GRAVITY FORCE---//

        Vector3 force = mass * gravity * Vector3.down;          // Part2: Calculates this objects downward force using mass and gravity. 
        Vector3 acceleration = force / mass;                    // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * dt;                          // Part2: Updates velocity using acceleration and real time.
        transform.position += velocity * dt;                    // Part2: Updates object position based on velocity and real time.

    //---GROUND COLLISION---//

        Ground ground = FindObjectOfType<Ground>();             // Part2: Declares a variable for the Ground game object.
        bool hasCollided = ground.GroundCollision(transform.position, objectSize.y);    // Part2: Declares a bool for the activation of the GroundCollision method in the Ground script.
        if (hasCollided)
        {
            velocity = Vector3.zero;                                            // Part2: Velcocity for this object is reset.
            acceleration = Vector3.zero;                                        // Part2: Acceleration for this object is reset.
            float radiusY = objectSize.y / 2;                                   // Part2: Float to hold the Y axis points of the falling object.
            float distanceToGround = Mathf.Abs(transform.position.y - radiusY); // Part2: Float to hold the remaining Y axis distance from the ground.
            Vector3 upOffset = Vector3.up * distanceToGround;                   // Part2: Declares vector thats pushing upwards based on the total distance from the ground. 
            transform.position += upOffset;                                     // Part2: Objects position and upOffset are added, replacing the objects position.
        }
    //---CONTROL VECTORS---//

        Vector3 direction = Vector3.zero;                                       // Part1: When no key is pressed, all player created directional force reverts back to zero.
        if (Input.GetKey(KeyCode.W))                                            // Part1: If set key is pressed...
        {
            transform.Translate(Vector3.up * speed * dt);                       // Part1: Object moves in set direction with a magnitude of speed by time. 
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

    //---IMPULSE JUMP---//

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isJumping)                                     // Part3: If mouse has right clicked and isJumping is not activated...
        {
            isJumping = true;                                                                   // Part3: Then isJumping is activated.
            jumpStartTime = Time.time;                                                          // Part3: Then jumpStartTime holds current time. 
        }
        if (isJumping)                                                                          // Part3: If isJumping is activated...
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;                    // Part3: Float to constantly update (time minus jumpStartTime) divided by jumpDiraction.
            if (jumpProgress <= 1.0f)                                                           // Part3: If jumpProgress is less or equal to 1.0f...
            {
                float jumpDistance = jumpHeight * (1 - Mathf.Pow((2 * jumpProgress - 1), 2));   // Part3:
                transform.position =  Vector3.up * jumpDistance;                                // Part3:
            }
            else
            {
                isJumping = false;                                                              // Part3: Else isJumping is deactivated.
                transform.position = initialPosition;                                           // Part3: object position reverts to initial position.
            }
        }
    }
}