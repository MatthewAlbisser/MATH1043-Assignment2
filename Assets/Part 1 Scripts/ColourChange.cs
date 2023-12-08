//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using UnityEngine;

//public class ColorChange : MonoBehaviour
//{

//    public Color originalMaterial = Color.red; // Calls for RED
//    public Color customColor = Color.green;   // Calls GREEN for use 

//    private bool circleOverlaps = false;
//    private bool capsuleOverlaps = false;      // all 3 Bools the colour switch, starts off (RED)
//    private bool squareOverlaps = false;

//    public GameObject circle;
//    public GameObject square;
//    public GameObject capsule;

//    private void OnTriggerEnter2D(Collider2D other)  // REMOVE
//    {
//        if (other.gameObject.name == "Circle")          // 
//        {
//            circleOverlaps = true;
//        }
//        else if (other.gameObject.name == "Capsule")    // Checks which objects are colliding, trips bool.
//        {
//            capsuleOverlaps = true;
//        }
//        else if (other.gameObject.name == "Square")
//        {
//            squareOverlaps = true;
//        }                                               //

//        // Checks for all overlap variables
//        if ((circleOverlaps && capsuleOverlaps) || (circleOverlaps && squareOverlaps) || (capsuleOverlaps && squareOverlaps) || (circleOverlaps && capsuleOverlaps && squareOverlaps))
//        {
//            Debug.Log("colourswap");
//            TurnObjectsGreen();
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other) // REMOVE
//    {
//        if (other.gameObject.name == "Circle")
//        {
//            circleOverlaps = false;
//        }
//        else if (other.gameObject.name == "Capsule")        // Finds objects via name, 
//        {
//            capsuleOverlaps = false;
//        }
//        else if (other.gameObject.name == "Square")
//        {
//            squareOverlaps = false;
//        }

//        if (!circleOverlaps && !capsuleOverlaps && !squareOverlaps) // Checks for no overlap
//        {
//            ResetObjectsColor();
//        }
//    }

//    private void TurnObjectsGreen() // Changes object colour to green
//    {
//        circle.GetComponent<Renderer>().material.color = customColor;
//        capsule.GetComponent<Renderer>().material.color = customColor;
//        square.GetComponent<Renderer>().material.color = customColor;
//    }


//    private void ResetObjectsColor() // Resets object to original colour 
//    {
//        circle.GetComponent<Renderer>().material.color = originalMaterial;
//        capsule.GetComponent<Renderer>().material.color = originalMaterial;
//        square.GetComponent<Renderer>().material.color = originalMaterial; 
//    }
//}
////(capsuleOverlaps && squareOverlaps) || (circleOverlaps && capsuleOverlaps && squareOverlaps))


//    //if (circleOverlaps && capsuleOverlaps)
//    //{
//    //        circle.GetComponent<Renderer>().material.color = customColor;
//    //        capsule.GetComponent<Renderer>().material.color = customColor;
//    //}

//    //if (circleOverlaps && squareOverlaps)
//    //if (capsuleOverlaps && squareOverlaps)
//    //if (circleOverlaps && capsuleOverlaps && squareOverlaps)
  
