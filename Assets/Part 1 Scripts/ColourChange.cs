using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    public Color originalMaterial = Color.red; // Calls for RED
    public Color customColor = Color.green;   // Calls GREEN for use 
    private bool circleOverlaps = false;
    private bool capsuleOverlaps = false;      // all 3 Bools the colourswitch, starts off (RED)
    private bool squareOverlaps = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Circle")
        {
            circleOverlaps = true;
        }
        else if (other.gameObject.name == "Capsule")  // Checks which object is colliding, trips bool
        {
            capsuleOverlaps = true;
        }
        else if (other.gameObject.name == "Square")
        {
            squareOverlaps = true;
        }

        // Checks for all overlap variables
        if ((circleOverlaps && capsuleOverlaps) || (circleOverlaps && squareOverlaps) || (capsuleOverlaps && squareOverlaps) || (circleOverlaps && capsuleOverlaps && squareOverlaps))
        {
            Debug.Log("colourswap");
            TurnObjectsGreen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Circle")
        {
            circleOverlaps = false;
        }
        else if (other.gameObject.name == "Capsule")
        {
            capsuleOverlaps = false;
        }
        else if (other.gameObject.name == "Square")
        {
            squareOverlaps = false;
        }

        if (!circleOverlaps && !capsuleOverlaps && !squareOverlaps) // Checks for no overlap
        {
            ResetObjectsColor();
        }
    }

    private void TurnObjectsGreen() // Change object colour to green
    {
        GameObject.Find("Circle").GetComponent<Renderer>().material.color = customColor;
        GameObject.Find("Capsule").GetComponent<Renderer>().material.color = customColor;
        GameObject.Find("Square").GetComponent<Renderer>().material.color = customColor;
    }


    private void ResetObjectsColor() // Reset objects to original colour 
    {
        GameObject.Find("Circle").GetComponent<Renderer>().material.color = originalMaterial; 
        GameObject.Find("Capsule").GetComponent<Renderer>().material.color = originalMaterial; 
        GameObject.Find("Square").GetComponent<Renderer>().material.color = originalMaterial; 
    }
}
