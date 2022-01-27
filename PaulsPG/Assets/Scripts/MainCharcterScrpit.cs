using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MainCharcterScrpit : MonoBehaviour
{
    private float current_speed;
    private float BACKWARDS_SPEED = 1, RUNNING_SPEED = 3;
    private float turning_speed = 90;
    private float mouse_sesitivity_x = 1;
    Animator char_animation;
    private Vector3 jump ;
    private float jumpforce = 8f ;
    private float jumpheight;
    private float gravitySpeed = 30f ;
    private bool isGrounded;
    PlayerCameraScript my_camera;
    private Rigidbody rigg;


    public GameObject MainCharacter;
    void Start()
    {
        current_speed = RUNNING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        my_camera = GetComponentInChildren<PlayerCameraScript>();
        my_camera.you_belong_to(this);
        rigg = GetComponent<Rigidbody>();
        jump = new Vector3(0f, jumpheight, 0f);
;    }
    
    void Update()
    {
        char_animation.SetBool("running_forward", false);
        char_animation.SetBool("walking_backwards", false);

        
        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        //if (should_turn_left()) turn_left();
        turn(Input.GetAxis("Horizontal"));
        adjust_camera(Input.GetAxis("Vertical"));

        if(rigg.velocity.y <= 0)
        {
            if (Input.GetKey(KeyCode.Space)&& isGrounded)
            {
                rigg.AddForce(jump * jumpforce, ForceMode.Impulse);
                isGrounded = true;
            }
        }

        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "floor")
            isGrounded = true;
    }

    private void adjust_camera(float vertical_adjustment)
    {
        my_camera.adjust_vertical_angle(vertical_adjustment);
    }

   

    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sesitivity_x * mouse_turn_value_x * Time.deltaTime);
        if(Mathf.Abs(mouse_turn_value_x)>0.5f)char_animation.SetBool("walking_backwards", true);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);
        char_animation.SetBool("walking_backwards", true);
    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void move_backward()
    {
        transform.position -= BACKWARDS_SPEED * transform.forward * Time.deltaTime;      
        char_animation.SetBool("walking_backwards", true);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void move_forward()
    {//move in frame rate independant using s = u*t
        transform.position += current_speed * transform.forward * Time.deltaTime;
        char_animation.SetBool("running_forward", true);
       
    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}
