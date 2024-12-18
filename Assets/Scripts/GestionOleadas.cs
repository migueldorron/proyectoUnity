using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GestionOleadas : MonoBehaviour
{
    public TMP_Text textoOleadas; // El texto de los puntos del Canvas


    // Método para actualizar el texto del Canvas
    public void ActualizarTextoOleadas(int oleadas)
    {
        textoOleadas.text = "Oleada: " + oleadas;
    }
}
