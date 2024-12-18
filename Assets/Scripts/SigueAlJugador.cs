using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaSeguimiento : MonoBehaviour
{
    public GameObject jugador;
    public Vector3 posicionRelativa;

    void Update()
    {
        // Calcula la posición de la pistola en base a la posición y rotación del jugador
        transform.position = jugador.transform.position + jugador.transform.rotation * posicionRelativa;

        // Asegura que la pistola rote junto al jugador
        transform.rotation = jugador.transform.rotation;
    }
}
