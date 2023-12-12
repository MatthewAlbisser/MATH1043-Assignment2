using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassVelocity : MonoBehaviour
{
    public float mass = 10f;                        // Part2: Declared float for mass.
    public float gravity = 9.81f;                   // Part2: Declared float for gravity.
    public Vector3 velocity = Vector3.zero;         // Part2: Vector for Initial velocity.
    private Vector3 acceleration = Vector3.zero;    // Part2: Vector for Initial acceleration.
    private Vector3 force = Vector3.zero;           // Part2: Vector for Initial force.

    void Update()
    {
        Vector3 gravityMass = mass * Vector3.down * gravity;    // Part2: Calculates this objects downward force and calls it gravityMass.
        force += gravityMass;                                   // Part2: Activates "ApplyForce" method using gravityMass.

        acceleration = force / mass;                            // Part2: Calculates acceleration using force and mass.
        velocity += acceleration * Time.deltaTime;              // Part2: Calculates velocity using acceleration and real time.
        transform.position += velocity * Time.deltaTime;        // Part2: Updates object position based on velocity and real time.

        force = Vector3.zero;                                   // Part2: Resets every frame, keeps it honest. 
    }
}