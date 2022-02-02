using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCameraScript : MonoBehaviour 
{ 

    float angle = 0f, distance= 5f;
    private Vector3 desired_camera_position;
    Transform owning_character_transform;
    private MainCharcterScrpit owning_character;
    private float SENSITIVITY_VERTICAL_ROTATE = 0.02f;
    private float focal_height = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position,0.01f);
        transform.LookAt(owning_character_transform.position + focal_height* Vector3.up);
    }

    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        angle -= SENSITIVITY_VERTICAL_ROTATE * vertical_adjustment;
        angle = Mathf.Clamp(angle, -2,0);

        desired_camera_position = new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }

    internal void you_belong_to(MainCharcterScrpit mainCharcterScrpit)
    {
        owning_character_transform = mainCharcterScrpit.transform;
        owning_character = mainCharcterScrpit;
    }
}
