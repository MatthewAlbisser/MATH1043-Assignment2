using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ground : MonoBehaviour
{
    public bool GroundCollision(Vector3 fallingObjectPos, float objectHeight)   // Part2: Medthod activates from all 3 shape collision scripts. 
    {
        float groundH = 1.6f;                                                   // Part2: Hight of the ground object.
        float halfL = objectHeight / 2;                                         // Part2: Declares float to hold the falling Objects radius by taking its hieght and dividing by 2.
        float groundY = transform.position.y + groundH / 2;                     // Part2: Declares float to hold the top of the ground objects position on the Y axis.                   
        float distanceToGround = groundY - (fallingObjectPos.y - halfL);        // Part2: Declares float to hold variable for remaining distance to the ground from falling objects radius.
        if (fallingObjectPos.y - halfL <= groundY)                              // Part2: IF falling objects radius is less or greater then our ground objects Y axis... 
        {
            return true;                                                        // Part2: When there is a collision, set bool to true.
        }
        return false;                                                           // Part2: When there is no collision, set bool to false.
    }
}