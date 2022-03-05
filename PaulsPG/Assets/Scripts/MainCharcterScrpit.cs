using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharcterScrpit : MonoBehaviour
{
    private float current_speed;
    private float BACKWARDS_SPEED = 1, RUNNING_SPEED = 5, WALKING_SPEED = 1;
    private float turning_speed = 220;
    private float mouse_sesitivity_x = 0.05f;
    Animator char_animation;
    private bool isGrounded = true;
    Transform cameraT;
    private Rigidbody rigg;
    public HealthBar healthBar;
    public bool CanAttack = true;
    public float AttackCooldown = 0.01f;
    public bool isAttacking = false;

    public float turnTime = 0.2f;
    public float speedTime = 0.1f;
    float speedVelocity;
    float currentSpeed;
    float turnVelocity;




    
    public GameObject MainCharacter;
    void Start()
    {
        current_speed = RUNNING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        cameraT = Camera.main.transform;
        
        rigg = GetComponent<Rigidbody>();

        ; }

    void Update()
    {
        char_animation.SetBool("running_forward", false);
        char_animation.SetBool("walking_backwards", false);
        char_animation.SetBool("jumping", false);





        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        //if (should_turn_left()) turn_left();
        turn(Input.GetAxis("Horizontal"));
        

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigg.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            char_animation.SetBool("jumping", true);
            char_animation.SetBool("jump_landing", false);
            isGrounded = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnVelocity, turnTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? RUNNING_SPEED : WALKING_SPEED) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, speedTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
       char_animation.SetFloat("speedPercent", animationSpeedPercent, speedTime, Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if (collision.gameObject.tag == "floor")

        {
            print("floor");
            char_animation.SetBool("jump_landing", true);
            isGrounded = true;

        }

        if(collision.gameObject.tag == "Enemy")
        {
            if (healthBar)
            {
                healthBar.onTakeDamage(10);

                
            }
            
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && isAttacking )
        {
            print(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");

        }
    }


    public void SwordAttack()
    {
        isAttacking = true;
        char_animation.SetTrigger("attack");
        CanAttack = false;
       
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttack());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
    IEnumerator ResetAttack()
    {

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

   



    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sesitivity_x * mouse_turn_value_x * Time.deltaTime);
        if (Mathf.Abs(mouse_turn_value_x) > 0.5f) char_animation.SetBool("walking_backwards", true);
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
