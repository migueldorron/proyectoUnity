using UnityEngine;
using System.Collections;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f; // Velocidad del enemigo
    public float distanciaAtaque = 2f; // Distancia mínima para atacar
    public GameObject objetivo; // Referencia al jugador u objetivo
    private Animator animator; // Referencia al Animator
    private bool puedeAtacar = true; // Control para evitar ataques continuos
    public Generador generador;
    public Mover jugador;
    public int vida; // Vida del enemigo
    private Rigidbody rb; // Referencia al Rigidbody
    private Collider col; // Referencia al Collider del enemigo
    private bool estaMuerto = false; // Estado del enemigo

    void Start()
    {
        if (objetivo == null)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player");
        }
        animator = GetComponent<Animator>();
        vida = Generador.numeroOleada;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // Congelar rotación en los ejes X y Z
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (estaMuerto) return; // No hacer nada si el enemigo está muerto

        // Mantener personaje a una altura fija
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        if (objetivo == null) return; // Si no hay objetivo, no hace nada

        Transform objetivoTransform = objetivo.transform;
        float distancia = Vector3.Distance(transform.position, objetivoTransform.position);

        if (distancia > distanciaAtaque)
        {
            // Moverse hacia el jugador
            Vector3 direccion = (objetivoTransform.position - transform.position).normalized;
            transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

            // Girar para mirar al jugador
            MirarAlJugador();

            // Activar animación de caminar
            animator.SetFloat("MoveSpeed", 1f);
        }
        else
        {
            if (puedeAtacar)
            {
                // Activar animación de ataque
                animator.SetTrigger("Attack");

                // Quitar vida al jugador
                Mover jugadorScript = objetivo.GetComponent<Mover>();
                jugadorScript.Restarvida(1);
                jugadorScript.ActivarAnimacionDeDaño();

                // Detener el movimiento
                animator.SetFloat("MoveSpeed", 0f);

                // Temporizador para evitar ataques continuos
                puedeAtacar = false;
                Invoke(nameof(ResetearAtaque), 1.5f); // Tiempo entre ataques
            }
        }
    }

    private void ResetearAtaque()
    {
        puedeAtacar = true;
    }

    public void Morir()
    {
        if (estaMuerto) return; // Asegurarse de que no se procese dos veces

        estaMuerto = true; // Marcar como muerto
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        // Detener movimiento y colisiones
        velocidad = 0f;

        if (col != null)
        {
            col.enabled = false; // Desactivar el collider para evitar colisiones
        }

        if (rb != null)
        {
            rb.isKinematic = true; // Desactivar la física del Rigidbody
        }

        // Restablecer el trigger de ataque para evitar que la animación continúe
        animator.ResetTrigger("Attack");

        // Activar animación de morir
        animator.SetTrigger("Dead");

        // Sumar puntos al jugador
        GestionPuntos controladorPuntos = FindObjectOfType<GestionPuntos>();
        controladorPuntos.SumarPuntos();

        // Destruir el enemigo después de 3 segundos
        StartCoroutine(EsperarYDestruir(3f));
    }


    private IEnumerator EsperarYDestruir(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        Destroy(gameObject);
    }

    private void MirarAlJugador()
    {
        Vector3 direccion = objetivo.transform.position - transform.position;
        direccion.y = 0; // Mantener la rotación solo en el eje Y

        Quaternion rotacionHaciaJugador = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionHaciaJugador, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (estaMuerto) return; // Ignorar colisiones si está muerto

        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Detener cualquier impulso
            rb.angularVelocity = Vector3.zero; // Detener cualquier rotación
        }
    }
}