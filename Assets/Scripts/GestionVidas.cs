using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionVidas : MonoBehaviour
{
    public TMP_Text textoVidas; // El texto de los puntos del Canvas
    private bool jugadorMuerto = false; // Variable que verifica si el jugador está muerto

    // Método para actualizar el texto del Canvas
    public void ActualizarTextoVidas(int vidas)
    {
        // Si el jugador está muerto, no actualizamos el texto
        if (jugadorMuerto)
            return;

        textoVidas.text = "Vidas: " + vidas;
    }

    // Método que se llama cuando el jugador muere
    public void JugadorMuerto()
    {
        jugadorMuerto = true; // Marcamos que el jugador está muerto
    }
}
