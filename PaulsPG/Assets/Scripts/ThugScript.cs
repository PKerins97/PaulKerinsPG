using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThugScript : MonoBehaviour
{
    private float current_speed;
    private float BACKWARDS_SPEED = 1, RUNNING_SPEED = 5;
    private float turning_speed = 90;
    Animator char_animation;
    private bool isGrounded = true;
    private Rigidbody rigg;

    public GameObject Thug1,Thug2,Thug3,Thug4;
    // Start is called before the first frame update
    void Start()
    {
        current_speed = RUNNING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        rigg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
