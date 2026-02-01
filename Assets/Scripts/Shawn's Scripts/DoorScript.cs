using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Transform teleportPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Sets the touched object's position to the teleport point's position
        other.transform.position = teleportPoint.position;
        print("overlapped with something");
    }
}

