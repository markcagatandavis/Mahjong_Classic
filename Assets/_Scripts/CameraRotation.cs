using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    /*THIS SCRIPT IS RESPONSIBLE FOR THE ROTATION OF CAMER WHEN RIGHT MOUSE BUTTON CLICKED*/
    public Transform target;
    int degrees = 5;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * degrees);
        }
    }
}

