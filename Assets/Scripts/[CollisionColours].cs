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

    bool CollisionCapsuleCircle(Vector2 capsuleCenter, Vector2 circleCenter,float capsuleHeight, float capsuleRadius, float circleRadius)
    {
        float deltaX = Mathf.Abs(capsuleCenter.x - circleCenter.x);
        float deltaY = Mathf.Abs(capsuleCenter.y - circleCenter.y);

        if (deltaX <= capsuleRadius + circleRadius && deltaY <= capsuleHeight / 2 + circleRadius)
        {
            float minY = capsuleCenter.y - capsuleHeight / 2;
            float maxY = capsuleCenter.y + capsuleHeight / 2;

            if (circleCenter.y >= minY && circleCenter.y <= maxY)
            {
                return true;
            }
        }

        float cornerDistanceSquared = Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY - capsuleHeight / 2, 2);
        return cornerDistanceSquared <= Mathf.Pow(capsuleRadius + circleRadius, 2);
    }
    bool CollisionSquareCapsule(Vector2 squareCenter, Vector2 capsuleCenter, float squareHalfLength, float capsuleRadius, float capsuleHeight)
    {
        float halfSquareSide = squareHalfLength;

        float deltaX = Mathf.Abs(capsuleCenter.x - squareCenter.x);
        float deltaY = Mathf.Abs(capsuleCenter.y - squareCenter.y);
        float combinedRadius = capsuleRadius + halfSquareSide;

        bool xOverlap = deltaX <= combinedRadius;
        bool yOverlap = deltaY <= combinedRadius;

        if (xOverlap || yOverlap)
        {
            float minY = capsuleCenter.y - capsuleHeight / 2;
            float maxY = capsuleCenter.y + capsuleHeight / 2;

            float squareMinY = squareCenter.y - halfSquareSide;
            float squareMaxY = squareCenter.y + halfSquareSide;

            if (squareMaxY >= minY && squareMinY <= maxY)
            {
                return true;
            }
        }
        float cornerDistanceSquared = Mathf.Pow(deltaX - halfSquareSide, 2) + Mathf.Pow(deltaY - halfSquareSide, 2);
        return cornerDistanceSquared <= (capsuleRadius * capsuleRadius);
    }
}