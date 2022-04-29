using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject pickUpItems;

    
    
    

    // Start is called before the first frame update
    void Start()
    {

        
        for (int i = 0; i<10; i++)
        {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.position = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Im_Dead(EnemyScript enemy)
    {
        if(enemy is EnemyScript) {
            EnemyScript killed_enemy = enemy as EnemyScript;
            
            
            
        }
       
            
        
    }
}
