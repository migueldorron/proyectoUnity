using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generador : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabEnemigo;
    private float rangoGeneracion = 60.0f;
    public int numeroEnemigos;
    public static int numeroOleada = 1; // Oleada actual
    public int numeroOleadaInicial = 1;  // Valor inicial de la oleada
    public float distanciaMinimaJugador = 20.0f; // Distancia mínima entre el jugador y los enemigos generados
    public GameObject jugador; // Referencia al jugador
    public GestionOleadas controladorOleadas;
    public GestionEnemigosVivos controladorEnemigosVivos;

    void Start()
    {
        // Buscar el jugador por su tag
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");
        }
        numeroOleada=1;
        GeneradorEnemigos(numeroOleada);  // Generar enemigos en la primera oleada


    }

    // Update is called once per frame
    void Update()
    {
        numeroEnemigos = FindObjectsOfType<Enemigo>().Length;

        controladorEnemigosVivos = FindObjectOfType<GestionEnemigosVivos>();
        controladorEnemigosVivos.ActualizarEnemigosVivos(numeroEnemigos);

        if (numeroEnemigos == 0)
        {
            numeroOleada++;  // Incrementar el número de oleada
            GeneradorEnemigos(numeroOleada);

            controladorOleadas = FindObjectOfType<GestionOleadas>();
            controladorOleadas.ActualizarTextoOleadas(numeroOleada);
        }
    }

    // Método para obtener una posición aleatoria para los enemigos
    Vector3 DamePosicionGeneracion()
    {
        float posXGeneracion = Random.Range(-rangoGeneracion, rangoGeneracion);
        float posYGeneracion = Random.Range(-rangoGeneracion, rangoGeneracion);
        Vector3 posAleatoria = new Vector3(posXGeneracion, 0, posYGeneracion);
        return posAleatoria;
    }

    // Método para generar enemigos
    void GeneradorEnemigos(int enemigos)
    {
        for (int i = 0; i < enemigos; i++)
        {
            Vector3 posicionGeneracion;

            // Generar una posición aleatoria hasta que esté lejos del jugador
            do
            {
                posicionGeneracion = DamePosicionGeneracion();
            } while (Vector3.Distance(posicionGeneracion, jugador.transform.position) < distanciaMinimaJugador);

            Instantiate(prefabEnemigo, posicionGeneracion, prefabEnemigo.transform.rotation);
        }
    }

    // Método para reiniciar las oleadas
    public void ReiniciarGenerador()
    {
        // Restablecer el número de oleadas al valor inicial
        numeroOleada = numeroOleadaInicial;
       
    }
}