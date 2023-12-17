using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColours : MonoBehaviour
{
    public Transform circleT;
    public Transform squareT;       // Part1: Declares object positions
    public Transform capsuleT;

    void Update()
    {
        bool circleSquare = CollisionCircleSquare(circleT.position, squareT.position, 0.5f, 0.5f);
        bool capsuleCircle = CollisionCapsuleCircle(capsuleT.position, circleT.position, 0.5f, 0.5f, 1f);   // Colour bools for each method combination of collision. 
        bool squareCapsule = CollisionSquareCapsule(squareT.position, capsuleT.position, 0.5f, 0.5f, 1f);

        Color circleC = Color.red;
        Color squareC = Color.red;       // Part1: Declares object colours.
        Color capsuleC = Color.red;

        if (circleSquare)           // Part1: If bool method is true...
        {
            circleC = Color.green;  // Part1: Both objects change to green.
            squareC = Color.green;  // Part1: Both objects change to green.
        }
        if (capsuleCircle)
        {
            circleC = Color.green;
            capsuleC = Color.green;
        }
        if (squareCapsule)
        {
            capsuleC = Color.green;
            squareC = Color.green;
        }
        circleT.GetComponent<SpriteRenderer>().color = circleC;
        squareT.GetComponent<SpriteRenderer>().color = squareC;
        capsuleT.GetComponent<SpriteRenderer>().color = capsuleC;
    }
    bool CollisionCircleSquare(Vector2 circleCenter, Vector2 squareCenter, float circleRadius, float squareHalfL)
    {
        float minX = squareCenter.x - squareHalfL;     // Declares float for "-X" by calculating the center of the squares X axis, lowering by the squares half length.
        float maxX = squareCenter.x + squareHalfL;
        float minY = squareCenter.y - squareHalfL;
        float maxY = squareCenter.y + squareHalfL;

        float closestX = Mathf.Clamp(circleCenter.x, minX, maxX);
        float closestY = Mathf.Clamp(circleCenter.y, minY, maxY);

        float distanceX = circleCenter.x - closestX;
        float distanceY = circleCenter.y - closestY;

        float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

        return distanceSquared < (circleRadius * circleRadius);
    }

    bool CollisionCapsuleCircle(Vector2 circleCenter, Vector2 capsuleCenter, float circleRadius, float capsuleRadius, float capsuleHeight)
    {
        float distanceX = Mathf.Abs(circleCenter.x - capsuleCenter.x);
        float distanceY = Mathf.Abs(circleCenter.y - capsuleCenter.y);

        if (distanceX > (capsuleRadius + circleRadius)) return false;
        if (distanceY > (capsuleRadius + circleRadius)) return false;

        if (distanceX <= capsuleRadius || distanceY <= capsuleRadius) return true;

        float halfHeight = capsuleHeight * 0.5f;
        float distanceToTopSphere = Mathf.Sqrt((circleCenter.x - capsuleCenter.x) * (circleCenter.x - capsuleCenter.x) +
                                                       (circleCenter.y - capsuleCenter.y - halfHeight) * (circleCenter.y - capsuleCenter.y - halfHeight));

        float distanceToBottomSphere = Mathf.Sqrt((circleCenter.x - capsuleCenter.x) * (circleCenter.x - capsuleCenter.x) +
                                                  (circleCenter.y - capsuleCenter.y + halfHeight) * (circleCenter.y - capsuleCenter.y + halfHeight));

        if (distanceToTopSphere <= circleRadius || distanceToBottomSphere <= circleRadius)
        {
            return true; 
        }
        return false; 
    }
    bool CollisionSquareCapsule(Vector2 squareCenter, Vector2 capsuleCenter, float squareHalfLength, float capsuleRadius, float capsuleHeight)
    {
        float halfSquareSide = squareHalfLength;
        float deltaX = Mathf.Abs(capsuleCenter.x - squareCenter.x); 
        float deltaY = Mathf.Abs(capsuleCenter.y - squareCenter.y);

        float combinedRadius = capsuleRadius + halfSquareSide;      

        bool xOverlap = deltaX <= combinedRadius;                   
        bool yOverlap = deltaY <= combinedRadius;
        if (xOverlap && yOverlap)                                   
        {
            return true;                                            
        }
        float cornerDistanceSquared = Mathf.Pow(deltaX - halfSquareSide, 2) + Mathf.Pow(deltaY - halfSquareSide, 2);   
        return cornerDistanceSquared <= (capsuleRadius * capsuleRadius);
    }
}