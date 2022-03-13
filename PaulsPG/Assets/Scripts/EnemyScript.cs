using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform target;
    public float speed = 1f;
    Animator enemy_animation ;
    public float howclose;
    private float dist;
    private Rigidbody enemy;
   


    // Start is called before the first frame update
    void Start()
    {
        enemy_animation = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
       
        dist = Vector3.Distance(target.position, transform.position);
        

        if(dist <= howclose)
        {
            
            enemy_animation.SetBool("running_forwards", true);
            Vector3 fromMetoTarget = target.position - transform.position;
            fromMetoTarget = new Vector3(fromMetoTarget.x, 0, fromMetoTarget.z);
            Vector3 direction = fromMetoTarget.normalized;
            transform.position += speed * direction * Time.deltaTime;
            transform.LookAt(transform.position + direction);

            if(dist <= 1.5f)
            {
                
            }


        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            print("hit");
            

        }
    }

    
}
