using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool juegoTerminado = false;
    public GameObject textoGameOver; // Referencia al objeto de texto "Game Over"
    public GameObject botonReintentar; // Referencia al botón "Reintentar"
    public GameObject botonMenu;  // Referencia al botón "Menu principal"

    void Start()
    {
        Time.timeScale = 1f;
        // Asegúrate de que los elementos estén desactivados al inicio
        if (textoGameOver != null) textoGameOver.SetActive(false);
        if (botonReintentar != null) botonReintentar.SetActive(false);
        if (botonMenu != null) botonMenu.SetActive(false);
    }

    public void MostrarGameOver()
    {
        // Mostrar "Game Over" y habilitar el botón
        if (textoGameOver != null) textoGameOver.SetActive(true);
        if (botonReintentar != null) botonReintentar.SetActive(true);
        if (botonMenu != null) botonMenu.SetActive(true);
    }

    public void ReiniciarJuego()
    {
        // Restablecer Time.timeScale para que el juego se reanude
        Time.timeScale = 1f;

        // Cargar la escena actual nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Asegurarse de que los elementos UI estén ocultos de nuevo
        if (textoGameOver != null) textoGameOver.SetActive(false);
        if (botonReintentar != null) botonReintentar.SetActive(false);
        if (botonMenu != null) botonMenu.SetActive(true);

        // Restablecer las oleadas al valor inicial
        Generador generador = FindObjectOfType<Generador>();
        if (generador != null)
        {
            generador.ReiniciarGenerador();  // Restablecer las oleadas
        }

        //Reiniciar el estado de juego
        juegoTerminado = false;
    }

    public static void TerminarJuego()
    {
        juegoTerminado = true;
        Time.timeScale = 0f; // Pausar el juego cuando termine
        Debug.Log("¡Juego terminado!");
    }

    public static void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");        
    }
}
