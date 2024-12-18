using UnityEngine;
 
[RequireComponent(typeof(Rigidbody))]
public class BloqueoDeMovimientoYRotacion : MonoBehaviour
{
    private Vector3 posicionInicial; // Almacena la posición inicial del objeto
    private Quaternion rotacionInicial; // Almacena la rotación inicial del objeto
    private Rigidbody rb;
 
    void Start()
    {
        // Guarda la posición y rotación inicial del objeto
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
 
        // Obtiene el Rigidbody y lo configura
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Permite colisiones físicas
        rb.useGravity = false; // Evita que la gravedad afecte al objeto
        rb.constraints = RigidbodyConstraints.FreezeAll; // Congela completamente el objeto
    }
 
    void Update()
    {
        // Restablece la posición y rotación iniciales como medida adicional
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}