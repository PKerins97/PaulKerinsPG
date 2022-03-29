using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour,IDamageable
{

    enum Enemy_States { Idle, Move_to_Target, Attack, Dead};

    Enemy_States isCurrently = Enemy_States.Idle;


    public Transform target;
    public float speed = 1f;
    Animator enemy_animation ;
    public float howclose;
    private float dist;
    private Rigidbody enemy;
    internal ManagerScript theManager;

   public int MHP = 60;
   public int CHP;
    public int DPS = 10;
    private float Attack_Time = 0.8f;
    private float coolDown;
    private IDamageable targetHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemy_animation = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        CHP = MHP;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        switch (isCurrently)
        {
            case Enemy_States.Idle:


                if (dist <= howclose)
                {

                    enemy_animation.SetBool("running_forwards", true);

                    isCurrently = Enemy_States.Move_to_Target;

                }


                break;

            case Enemy_States.Move_to_Target:

                Vector3 fromMetoTarget = target.position - transform.position;
                fromMetoTarget = new Vector3(fromMetoTarget.x, 0, fromMetoTarget.z);
                Vector3 direction = fromMetoTarget.normalized;
                transform.position += speed * direction * Time.deltaTime;
                transform.LookAt(transform.position + direction);

                if (dist < 1.5f)
                {
                    isCurrently = Enemy_States.Attack;
                    enemy_animation.SetBool("running_forwards", false);
                    enemy_animation.SetBool("attacking", true);
                    targetHealth = target.GetComponent<IDamageable>();
                }
                break;

            case Enemy_States.Attack:

                coolDown -= Time.deltaTime;

                if (coolDown <0f)
                if (targetHealth != null)
                {
                    targetHealth.takeDamage(10);
                    coolDown = Attack_Time;
                }
                else
                    targetHealth = target.GetComponent<IDamageable>();

                break;

            case Enemy_States.Dead:
               


                break;

        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(40);
        }
    }
    internal void ImtheMan(ManagerScript manager)
    {
        theManager = manager;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            print("hit");
            takeDamage(20);

        }
    }

    public void takeDamage(int amountOfDamage)
    {
        print("Hit");
        CHP -= amountOfDamage;

        if (CHP <= 0) 
        {
           
            enemy_animation.SetBool("died", true);
            theManager.Im_Dead(this); 
        }
    }

    
}
