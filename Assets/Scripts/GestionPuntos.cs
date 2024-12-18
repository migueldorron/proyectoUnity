using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GestionPuntos : MonoBehaviour
{
    public TMP_Text textoPuntos; // El texto de los puntos del Canvas
    public TMP_Text textoMayorPuntuacion; // El texto de los puntos del Canvas
    private static int puntos; // Puntos del jugador
    private static int mayorPuntuacion; // Mayor puntuacion registrada


   void Start()
    {
        ActualizarTextoMayorPuntuacion();
        puntos=0;
    }


    // Método para agregar puntos
    public void SumarPuntos()
    {
        puntos++; // Incrementar puntos

        //Comprobar si es la puntuacion más alta
        if (puntos >= mayorPuntuacion)
        {
            mayorPuntuacion = puntos;
        }

        ActualizarTextoMayorPuntuacion();
        ActualizarTextoPuntos(); // Actualizar el texto en pantalla
       

       
    }

    // Método para actualizar el texto del Canvas
    private void ActualizarTextoPuntos()
    {
        textoPuntos.text = "Puntos: " + puntos;
    }

    // Método para actualizar el texto de la mayor puntuacion
    private void ActualizarTextoMayorPuntuacion()
    {
        textoMayorPuntuacion.text = "Mayor Puntuacion: " + mayorPuntuacion;
    }
}