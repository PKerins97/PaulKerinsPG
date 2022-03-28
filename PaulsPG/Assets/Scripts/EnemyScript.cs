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
    
   private int maxHealth = 60;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemy_animation = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
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
                    isCurrently = Enemy_States.Attack;

                break;

            case Enemy_States.Attack:
                enemy_animation.SetBool("running_forwards", false);
                enemy_animation.SetBool("attacking",true);
                IDamageable taregtScript =  target.GetComponent<IDamageable>();
                if (taregtScript != null)
                {
                    taregtScript.takeDamage(10);
                }

                break;

            case Enemy_States.Dead:
                    if(currentHealth == 0)
                {
                    enemy_animation.SetBool("died", true);
                    Destroy(gameObject);
                }
                break;





           


        }

        if (Input.GetKey(KeyCode.E))
        {
            takeDamage(10);
        }
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hit");
            

        }
    }

    public void takeDamage(int amountOfDamage)
    {
        print("Hit");
    }
}
