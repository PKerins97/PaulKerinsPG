using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpObject : MonoBehaviour
{
    public float healAmount ;

    private void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            
            print("Item Picked Up");
            Destroy(gameObject);
        }

       

    }



}
