using UnityEngine;
using System.Collections;
using System; // Para usar Corutinas

public class Mover : MonoBehaviour
{
    public GameObject escopeta;  // Referencia a la escopeta del jugador
    private Rigidbody escopetaRb;  // Rigidbody de la escopeta

    public float velocidad = 5f;  // Velocidad del jugador
    public GameObject ProyectilPrefab;  // Script de disparar
    public GameObject pistola;
    float limite = 80;  // Límite del mapa
    public Animator animator;  // Animator del personaje
    public int vidasIniciales = 3;  // Vida inicial del jugador
    public int vidasActuales;  // Vida actual del jugador
    public Rigidbody rb;  // Referencia al Rigidbody
    public bool estaMuerto = false;  // Variable para controlar si el jugador está muerto


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        escopetaRb = escopeta.GetComponent<Rigidbody>();  // Obtener el Rigidbody de la escopeta
        vidasActuales = vidasIniciales; // Inicializar las vidas del jugador
        // Congelar rotación en los ejes X y Z
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Si el jugador está muerto, no hacer nada más
        if (estaMuerto) return;

        // Comprobar si el jugador está en el estado de muerte
        if (vidasActuales == 0)
        {
            Morir();
            return;  // Detener el resto de la ejecución cuando el jugador muere
        }

        // Movimiento y otras interacciones si el jugador está vivo
        if (Input.GetKey(KeyCode.Space))
        {
            velocidad = 15f;
        }
        else
        {
            velocidad = 5f;
        }

        // Actualizar las vidas en la UI
        GestionVidas controladorVidas = FindObjectOfType<GestionVidas>();
        controladorVidas.ActualizarTextoVidas(vidasActuales);

        // Mantener personaje a una altura fija
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        // Movimiento del objeto
        float desplazamientoHorizontal = Input.GetAxis("Horizontal");
        float desplazamientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(desplazamientoHorizontal, 0, desplazamientoVertical);

        // Si no hay entrada de movimiento, detener la velocidad
        if (movimiento.magnitude == 0)
        {
            rb.velocity = Vector3.zero;  // Detener el movimiento
        }
        else
        {
            // Si hay entrada, mover el personaje
            transform.Translate(movimiento * Time.deltaTime * velocidad, Space.World);
        }

        // Actualizar animación de movimiento
        animator.SetFloat("movimiento", movimiento.magnitude);

        // Comprobación de límites del mapa
        if (transform.position.x < -limite)
        {
            transform.position = new Vector3(-limite, transform.position.y, transform.position.z);
        }

        if (transform.position.x > limite)
        {
            transform.position = new Vector3(limite, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -limite)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -limite);
        }

        if (transform.position.z > limite)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limite);
        }

        // Comprobación de disparo
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        //if (GameManager.juegoTerminado)
          //  return;  // Salir si el juego ha terminado

        // Instanciar el proyectil
        Vector3 spawnPosition = pistola.transform.position + pistola.transform.forward * 1.5f;
        spawnPosition.y = 1.5f;  // Ajustar la altura del proyectil

        Instantiate(ProyectilPrefab, spawnPosition, pistola.transform.rotation);

        // Activar la animación de disparar
        animator.SetTrigger("disparar");
    }

    private void Morir()
    {
        if (estaMuerto) return;

        estaMuerto = true;
        animator.ResetTrigger("disparar");

        // Desactivar físicas del jugador
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        // Reproducir animación de muerte
        animator.SetTrigger("morir");

        // Hacer que la escopeta se caiga al suelo
        CaerEscopeta();

        // Desactivar el script de rotación hacia el ratón
        RotarHaciaElRaton rotarHaciaElRaton = GetComponent<RotarHaciaElRaton>();
        if (rotarHaciaElRaton != null)
        {
            rotarHaciaElRaton.enabled = false;  // Desactivar el script de rotación
        }

        // Mostrar "Game Over"
        GameManager Manager = FindObjectOfType<GameManager>();
        if (Manager != null)
        {
            Manager.MostrarGameOver(); // Mostrar el mensaje de "Game Over"
        }

        // Iniciar corutina para esperar el final de la animación antes de terminar el juego
        StartCoroutine(EsperarAnimacionDeMuerte());
    }

    private void CaerEscopeta()
    {
        // Desactivar el script de seguimiento para que la escopeta no siga al jugador
        PistolaSeguimiento pistolaSeguimiento = escopeta.GetComponent<PistolaSeguimiento>();
        if (pistolaSeguimiento != null)
        {
            pistolaSeguimiento.enabled = false;  // Desactivar el script de seguimiento
        }

        // Desactivar el modo cinemático y permitir la física para la escopeta
        escopetaRb.isKinematic = false;  // La escopeta se ve afectada por la física
        escopetaRb.useGravity = true;  // Asegurarse de que la gravedad afecte a la escopeta
        escopetaRb.AddForce(Vector3.down * 5f, ForceMode.Impulse);  // Hacer que la escopeta caiga más rápido
    }

    private IEnumerator EsperarAnimacionDeMuerte()
    {
        // Esperar la duración de la animación de muerte
        yield return new WaitForSeconds(2f);

        // Llamar a GameManager para terminar el juego
        GameManager.TerminarJuego();
    }

    // Restar vida al jugador
    public void Restarvida(int vida)
    {
        vidasActuales -= vida;
        Debug.Log("Vida menos, vidas restantes: " + vidasActuales);

        //Para que el contador no baje de 0
        if (vidasActuales < 0)  
        {
            vidasActuales = 0;
        }

        // Actualizar el texto de vidas
        GestionVidas controladorVidas = FindObjectOfType<GestionVidas>();
        if (controladorVidas != null)
        {
            controladorVidas.ActualizarTextoVidas(vidasActuales);  // Actualizar el texto
        }
    }

    // Método para activar la animación de daño
    public void ActivarAnimacionDeDaño()
    {
        animator.SetTrigger("daño");
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        // Detectar colisión con enemigos
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Reducir la vidaJugador del personaje
            Restarvida(1);

            // Activar la animación de daño
            ActivarAnimacionDeDaño();
        }
    }

    public int getVidas(){
        return vidasActuales;
    }
}
