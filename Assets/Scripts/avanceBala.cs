using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class avanceBala : MonoBehaviour
{
    // Start is called before the first frame updatepublic
    public float velocidad = 40f;

    private Rigidbody rb; // Referencia al Rigidbody de la bala

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();

        // Hacer que la bala se mueva hacia adelante al ser disparada
        if (rb != null)
        {
            rb.velocity = transform.forward * velocidad; // Aplicar velocidad a la bala
        }
    }

    // M�todo que se llama cuando la bala colisiona con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la colisi�n es con un enemigo
        if (other.gameObject.CompareTag("Enemigo"))
        {
            // Llamar al m�todo Morir en el enemigo para destruirlo
            Enemigo enemigo = other.gameObject.GetComponent<Enemigo>();

            
            if (enemigo==true)
            {
                enemigo.vida--;
                if(enemigo.vida<=0){                
                enemigo.Morir(); // M�todo para destruir el enemigo
                }
            }
            
            // Destruir la bala despu�s de colisionar
            Destroy(gameObject);
        }
        else
        {
            // Destruir la bala si colisiona con otros objetos que no sean enemigos
            Destroy(gameObject);
        }


        // Update is called once per frame
        
    }

    void Update()
        {
            
        }

}
