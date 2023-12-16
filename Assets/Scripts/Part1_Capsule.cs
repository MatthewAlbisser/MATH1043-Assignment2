using UnityEngine;

public class Part1_Capsule : MonoBehaviour
{
    private float speed = 5.0f;     // Part1: Simple float to hold object speed.
    private float moveX;            // Part1: Float for x axis direction.
    private float moveY;            // Part1: Float for Y axis direction.

    void Update()
    {
        if (Input.GetKey(KeyCode.J))        // Part1: If direction input via GetKey...
        {
            moveX = -1;                     // Part1: Force direction of movement.
        }
        else if (Input.GetKey(KeyCode.L))   // Part1: Else if opposite direction input via GetKey...
        {
            moveX = 1;                      // Part1: Force direction of movement.
        }
        else                                // Part1: Else no button press...
        {
            moveX = 0;                      // Part1: direction is removed.
        }

        if (Input.GetKey(KeyCode.I))
        {
            moveY = 1;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            moveY = -1;
        }
        else
        {
            moveY = 0;
        }
        float newX = transform.position.x + moveX * speed * Time.deltaTime; // Part1: Calculates the new position based on current position and movement direction.
        float newY = transform.position.y + moveY * speed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, transform.position.z); // Part1: Updates the objects position using the new position.
    }
}