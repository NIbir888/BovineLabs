using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    // Public variable to adjust rotation speed in the Unity Inspector
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // Default to 50 degrees/second on the Y-axis

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the specified axes
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}