using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform LookAtObject; // Assign the player's Transform in the Inspector

    void Update()
    {
        if (LookAtObject != null)
        {
            // Make the object look at the player
            transform.LookAt(LookAtObject);
        }
    }
}
