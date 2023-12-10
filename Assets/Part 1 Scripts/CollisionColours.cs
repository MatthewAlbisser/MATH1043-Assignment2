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
        bool capsuleCircle = CollisionCapsuleCircle(capsuleT.position, circleT.position, 0.5f, 0.5f, 1f);
        bool squareCapsule = CollisionSquareCapsule(squareT.position, capsuleT.position, 1f, 0.5f, 1f);

        Color circle = Color.red;
        Color square = Color.red;       // Part1: Declares object colours.
        Color capsule = Color.red;

        if (circleSquare)               // Part1: If bool is true...
        {
            circle = Color.green;       // Part1: Both objects change to green.
            square = Color.green;

            if (!circleSquare)          // Part1: If bool is false...
            {
                circle = Color.red;     // Part1: Both change back to red.
                square = Color.red;
            }
        }

        if (capsuleCircle)
        {
            circle = Color.green;
            capsule = Color.green;

            if (!capsuleCircle)
            {
                circle = Color.red;
                capsule = Color.red;
            }
        }

        if (squareCapsule)
        {
            capsule = Color.green;
            square = Color.green;

            if (!squareCapsule)
            {
                capsule = Color.red;
                square = Color.red;
            }
        }

        // 3. Shader with colors
        circleT.GetComponent<SpriteRenderer>().color = circle;
        squareT.GetComponent<SpriteRenderer>().color = square;
        capsuleT.GetComponent<SpriteRenderer>().color = capsule;
    }
    bool CollisionCircleSquare(Vector2 circleCenter, Vector2 squareCenter, float circleRadius, float squareHalfLength)
    {
        float minX = squareCenter.x - squareHalfLength;
        float maxX = squareCenter.x + squareHalfLength;
        float minY = squareCenter.y - squareHalfLength;     
        float maxY = squareCenter.y + squareHalfLength;

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

        float cornerDistanceSquared = Mathf.Pow((distanceX - capsuleRadius), 2) + Mathf.Pow((distanceY - capsuleRadius), 2);

        return cornerDistanceSquared <= (circleRadius * circleRadius);
    }

    bool CollisionSquareCapsule(Vector2 squareCenter, Vector2 capsuleCenter, float squareHalfLength, float capsuleRadius, float capsuleHeight)
    {
        float halfCapsuleHeight = capsuleHeight / 2f;

        float minX = squareCenter.x - squareHalfLength;
        float maxX = squareCenter.x + squareHalfLength;
        float minY = squareCenter.y - squareHalfLength;
        float maxY = squareCenter.y + squareHalfLength;


        float nearestX = Mathf.Clamp(capsuleCenter.x, minX, maxX);
        float nearestY = Mathf.Clamp(capsuleCenter.y, minY, maxY);

        float deltaX = capsuleCenter.x - nearestX;
        float deltaY = capsuleCenter.y - nearestY;

        bool insideSquare = (deltaX * deltaX) + (deltaY * deltaY) < capsuleRadius * capsuleRadius;
        bool insideHeight = Mathf.Abs(capsuleCenter.y - squareCenter.y) < halfCapsuleHeight + squareHalfLength;

        return insideSquare && insideHeight;

    }
}
