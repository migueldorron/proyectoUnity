using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirBalas : MonoBehaviour
{
    float limite=80;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


        if(transform.position.x > limite || transform.position.x<-limite || transform.position.z > limite || transform.position.z<-limite)
        {
            Destroy(gameObject);
        }
    }
}
