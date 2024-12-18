using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        // Asegúrate de que la escena del juego esté añadida en Build Settings
        SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        // Cierra la aplicación
        Application.Quit();
        // Si estás en el editor de Unity, esto detiene el juego
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}