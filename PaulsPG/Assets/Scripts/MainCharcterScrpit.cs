using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainCharcterScrpit : MonoBehaviour, IDamageable
{
    private float current_speed;
    private float BACKWARDS_SPEED = 2, RUNNING_SPEED = 5, SPRINT_SPEED = 10;
    private float turning_speed = 220;
    private float mouse_sesitivity_x = 0.05f;
    Animator char_animation;
    private bool isGrounded = true;
    Transform cameraT;
    private Rigidbody rigg;
    public Text healthUI;
    public bool CanAttack = true;
    public float AttackCooldown = 0.01f;
    public bool isAttacking = false; 
    

    public float turnTime = 0.2f;
    public float speedTime = 0.1f;
    float speedVelocity;
    float currentSpeed;
    float turnVelocity;

    public int points = 0;
    public float MHP;
    public float CHP;
    public float pointsIncreasedPerSecond;
    private float DPS = 10f;

    
    private float coolDown = 4f;
    GameObject punch, sword;

    
    enum CharacterState { Idle,punching, swording, Died}
    CharacterState isCurrently = CharacterState.Idle;
    public GameObject MainCharacter;
    void Start()
    {
        Transform[] all_bones = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform bone in all_bones)
        {
            if (bone.gameObject.name.Contains("MayaSword"))
                sword = bone.gameObject;
            if (bone.gameObject.name == "Character L Hand")
                punch = bone.gameObject;
        }

        current_speed = RUNNING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        cameraT = Camera.main.transform;
        
        rigg = GetComponent<Rigidbody>();
        MHP = 100;
        CHP = 100;
        pointsIncreasedPerSecond = 1f;
        ; }

    void Update()
    {
        char_animation.SetBool("running_forward", false);
        char_animation.SetBool("walking_backwards", false);
        char_animation.SetBool("walking_left", false);
        char_animation.SetBool("walking_right", false);
        char_animation.SetBool("jumping", false);
        char_animation.SetBool("sprint", false);
        isAttacking = false;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;



        CHP += pointsIncreasedPerSecond * Time.deltaTime;

        if (CHP > MHP)
        {
            CHP = 100;
        }
        if (CHP < 0)
        {
            CHP = 0;
        }
        
        healthUI.text = (int)CHP + " Health";


        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        if (should_move_left()) move_left();
        if (should_move_right()) move_right();
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

        if (Input.GetMouseButton(1))
        {
            if (CanAttack)
            {
                PunchAttack();
            }
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnVelocity, turnTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

            currentSpeed = SPRINT_SPEED;
            char_animation.SetBool("sprint", true);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "floor")

        {
            
            char_animation.SetBool("jump_landing", true);
            isGrounded = true;

        }

        if(collision.gameObject.tag == "Border")
        {
            transform.Rotate(0, -180, 0);
        }

        

        if(collision.gameObject.tag == "coin")
        {
            points++;
            

        }


       

        if (collision.gameObject.tag == "EndGame")
        {
            SceneManager.LoadScene("WinnerScene");
        }
    }

  
    

    public void SwordAttack()
    {
        isAttacking = true;
        char_animation.SetTrigger("attack");
        CanAttack = false;
        isCurrently = CharacterState.swording;
        StartCoroutine(ResetAttackCooldown());

    }
    public void PunchAttack()
    {
        isAttacking = true;
        char_animation.SetTrigger("Punch");
        CanAttack = false;
        isCurrently = CharacterState.punching;
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttack());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        isCurrently = CharacterState.Idle;
    }
    IEnumerator ResetAttack()
    {

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        check_for_hit();

    }

    private void check_for_hit()
    {
        Collider[] hit_objects;

        switch (isCurrently)
        {
            case CharacterState.swording:

                 hit_objects = Physics.OverlapSphere(sword.transform.position + sword.transform.up, 0.5f);
                break;

            case CharacterState.punching:

                hit_objects = Physics.OverlapSphere(punch.transform.position , 0.5f);
                break;

            default:
                hit_objects = new Collider[0];

                break;


        }
        foreach (Collider obj in hit_objects)
        {
           if (this != obj.gameObject.GetComponent<MainCharcterScrpit>())
            {
                IDamageable obj_damage = obj.GetComponent<IDamageable>();
                if (obj_damage != null)
                    obj_damage.takeDamage(20);
                //set up particles here

            }
        }
    }

    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sesitivity_x * mouse_turn_value_x * Time.deltaTime);
        if (Mathf.Abs(mouse_turn_value_x) > 0.5f) char_animation.SetBool("walking_backwards", true);
    }

    private void move_left()
    {
        transform.position -= BACKWARDS_SPEED * transform.right * Time.deltaTime;
        char_animation.SetBool("walking_left", true);
    }

    private bool should_move_left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void move_right()
    {
        transform.position += BACKWARDS_SPEED * transform.right * Time.deltaTime;
        char_animation.SetBool("walking_right", true);
    }

    private bool should_move_right()
    {
        return Input.GetKey(KeyCode.D);
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

    public void takeDamage(int amountOfDamage)
    {
        print("Ouch");
        CHP -= amountOfDamage;
        
        if(CHP <= 0)
        {
            char_animation.SetBool("died", true);
            if (coolDown > 0f)
            {
                SceneManager.LoadScene("GameOverMenu");
            }
        }
        
        
    }

  

    private void OnGUI()
    {
        //Gui for Collecting Coins
        GUI.Label(new Rect(500, 10, 100, 20), "Score: " + points);
    }
}
