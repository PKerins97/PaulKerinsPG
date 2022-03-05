using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;


    private Vector3 offset;
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 10.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitvityX = 4.0f;
    private float sensitvityY = 1.0f;

     void Start()
    {
        offset = transform.position - lookAt.transform.position;
        camTransform = transform;
        cam = Camera.main;

        
    }

   void Update()
    {
        transform.position = lookAt.transform.position + offset;
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

    }

     void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }

}
