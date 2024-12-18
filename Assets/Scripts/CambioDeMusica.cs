using UnityEngine;

public class CambioDeMusica : MonoBehaviour
{
    public AudioSource musicaNormal;  // AudioSource para la música normal
    public AudioClip clipNormal; //AudioClip para la música normal
    public AudioSource musicaTension;  // AudioSource para la música de tensión
    public AudioClip clipTension; //AudioClip para la música de tensión
    public AudioSource musicaMuerte;  // AudioSource para la música de muerte        
    public AudioClip clipMuerte; //AudioClip para la música de muerte
    public GameObject jugador;  // Referencia al jugador
    public Mover jugadorScript; //Referencia a los scripts del jugador
    void Start()
    {   
        
        
        jugador = GameObject.FindGameObjectWithTag("Player");
        jugadorScript = jugador.GetComponent<Mover>();

        musicaNormal.clip=clipNormal;
        musicaTension.clip=clipTension;
        musicaMuerte.clip=clipMuerte;

        musicaTension.Play();
        musicaTension.Stop();
        musicaMuerte.Play();
        musicaMuerte.Stop();        
        // Asegurarse de que la música normal esté activa al inicio
        if (musicaNormal != null)
        {
            musicaNormal.Play();
        }

        if (musicaTension != null)
        {
            musicaTension.Stop();
        }
    }

    void Update()
    {

        if (jugadorScript.getVidas() == 1)
        {
            CambiarAMusicaTension();
        }

        if(jugadorScript.getVidas() == 0){
            CambiarAMusicaMuerte();
        }
    }

    void CambiarAMusicaTension()
    {
        if (musicaNormal.isPlaying)
        {
            musicaNormal.Stop();
        }

        if (!musicaTension.isPlaying)
        {
            musicaTension.Play();
        }
    }

    void CambiarAMusicaMuerte()
    {
        if (musicaTension.isPlaying)
        {
            musicaTension.Stop();
        }

        if (!musicaMuerte.isPlaying)
        {
            musicaMuerte.Play();
        }
    }
}