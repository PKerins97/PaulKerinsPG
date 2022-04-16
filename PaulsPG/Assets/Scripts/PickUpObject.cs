using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{


    private void Update()
    {
        transform.Rotate( 90 * Time.deltaTime,0, 0);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if( collider.gameObject.tag == "Player")
        {

            collider.GetComponent<MainCharcterScrpit>().points++;
            print("Item Picked Up");
            Destroy(gameObject); 
        }

        if (this.gameObject.tag == "healthBottle " && collider.gameObject.tag == "Player")
        {
            collider.GetComponent<MainCharcterScrpit>();
            print("health gained");
            Destroy(gameObject);
        }
    }
}
