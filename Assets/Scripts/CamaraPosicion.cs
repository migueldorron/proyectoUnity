using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SigueAlJugador : MonoBehaviour
{
    public GameObject jugador;
    public int x, y, z;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.position=jugador.transform.position+new Vector3(x,y,z);
        transform.LookAt(jugador.transform);
    }
}
