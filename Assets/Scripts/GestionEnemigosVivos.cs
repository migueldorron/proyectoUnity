using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GestionEnemigosVivos : MonoBehaviour
{
    public TMP_Text textoEnemigosVivos; // El texto de los puntos del Canvas


    // Método para actualizar el texto del Canvas
    public void ActualizarEnemigosVivos(int enemigosVivos)
    {
        textoEnemigosVivos.text = "Enemigos vivos: " + enemigosVivos;
    }
}
