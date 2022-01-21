using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MainCharcterScrpit : MonoBehaviour
{
    private float current_speed;
    private float WALKING_SPEED = 1, RUNNUNG_SPEED = 3;
    private float turning_speed;
    private float mouse_sesitivity_x = 1;
    Animator char_animation;

    public GameObject MainCharacter;
    void Start()
    {
        current_speed = WALKING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        char_animation.SetBool("walking_forward", false);
        char_animation.SetBool("walking_backwards", false);


        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        //if (should_turn_left()) turn_left();

        turn(Input.GetAxis("Horizontal"));

/*
        if (Input.GetButtonDown("Forwards"))
        {
            MainCharacter.GetComponent<Animator>().Play("Run");
        }
        if (Input.GetButtonDown("Backwards"))
        {
            MainCharacter.GetComponent<Animator>().Play("Backwards");
        }
        if (Input.GetButtonDown("Left"))
        {
            MainCharacter.GetComponent<Animator>().Play("Left");
        }
        if (Input.GetButtonDown("Right"))
        {
            MainCharacter.GetComponent<Animator>().Play("Right");
        }*/
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
        transform.position -= current_speed * transform.forward * Time.deltaTime;      
        char_animation.SetBool("walking_backwards", true);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void move_forward()
    {//move in frame rate independant using s = u*t
        transform.position += current_speed * transform.forward * Time.deltaTime;
        char_animation.SetBool("walking_forward", true);
       
    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}
