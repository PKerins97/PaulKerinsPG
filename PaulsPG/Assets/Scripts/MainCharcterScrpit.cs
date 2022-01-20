using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MainCharcterScrpit : MonoBehaviour
{


    public GameObject MainCharacter;
    
    void Update()
    {
        if(Input.GetButtonDown("Forwards"))
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
        }
    }
}
