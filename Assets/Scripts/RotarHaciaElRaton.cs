using UnityEngine;

public class RotarHaciaElRaton : MonoBehaviour
{
    public Camera cam; // Cámara principal
    public float alturaPlano = 0f; // Altura del plano donde el objeto se moverá
    private bool jugadorMuerto = false; // Variable para verificar si el jugador está muerto

    void Update()
    {
        // Verifica si el jugador está muerto
        if (jugadorMuerto)
        {
            return; // Si el jugador está muerto, no permite la rotación
        }

        // Lanza un rayo desde la posición del ratón
        Ray rayo = cam.ScreenPointToRay(Input.mousePosition);

        // Plano en el que queremos calcular la intersección
        Plane plano = new Plane(Vector3.up, new Vector3(0, alturaPlano, 0));

        // Verifica si el rayo intersecta el plano
        if (plano.Raycast(rayo, out float distancia))
        {
            // Calcula el punto de impacto en el plano
            Vector3 puntoImpacto = rayo.GetPoint(distancia);

            // Calcula la dirección desde el objeto al punto de impacto
            Vector3 direccion = puntoImpacto - transform.position;
            direccion.y = 0; // Ignora el eje Y para rotar solo en el plano horizontal

            // Ajusta la rotación del objeto hacia la dirección calculada
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

    // Método para llamar cuando el jugador muere
    public void MuerteJugador()
    {
        jugadorMuerto = true; // Marca al jugador como muerto
    }
}
