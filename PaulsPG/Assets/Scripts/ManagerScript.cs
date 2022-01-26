using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<100; i++)
        {
           GameObject g= GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.position = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
