using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around, default is the Y axis

    void Update()
    {
        // Rotate the object around the specified axis, multiplied by Time.deltaTime for smoothness
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
