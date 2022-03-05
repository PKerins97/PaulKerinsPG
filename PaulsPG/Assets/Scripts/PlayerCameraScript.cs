using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 2;
    public float rotationTime = .12f;
    Vector3 rotationVelocity;
    Vector3 currentRotation;
    public Vector2 xAxisMinMax = new Vector2(-20, 40);
    public bool lockCursor;
    float yAxis;
    float xAxis;
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void LateUpdate()
    {
        yAxis += Input.GetAxis("Mouse X") * mouseSensitivity;
        xAxis -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        xAxis = Mathf.Clamp(xAxis, xAxisMinMax.x, xAxisMinMax.y);


        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(xAxis, yAxis), ref rotationVelocity, rotationTime);
        
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }

}
