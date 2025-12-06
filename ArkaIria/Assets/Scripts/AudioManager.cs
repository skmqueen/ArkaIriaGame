using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip musicaFondo;
    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioVictoria;
    [SerializeField] private AudioClip sonidoSacarPelota;
    [SerializeField] private AudioClip sonidoColisionPelota;

    private void Awake()
    {
        // Implementar Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Reproducir música de fondo en bucle
        ReproducirMusicaFondo();
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Detener música y reproducir audio específico según la escena
        if (scene.name == "GameOver")
        {
            DetenerMusicaFondo();
            ReproducirAudio(audioGameOver);
        }
        else if (scene.name == "Victoria")
        {
            DetenerMusicaFondo();
            ReproducirAudio(audioVictoria);
        }
        else
        {
            // Si volvemos a otra escena, reanudar la música de fondo
            if (!musicSource.isPlaying)
            {
                ReproducirMusicaFondo();
            }
        }
    }

    private void ReproducirMusicaFondo()
    {
        if (musicaFondo != null && musicSource != null)
        {
            musicSource.clip = musicaFondo;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void DetenerMusicaFondo()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    private void ReproducirAudio(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.loop = false;
            musicSource.PlayOneShot(clip);
        }
    }

    // Métodos públicos para reproducir efectos de sonido
    public void ReproducirSonidoSacarPelota()
    {
        if (sonidoSacarPelota != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sonidoSacarPelota);
        }
    }

    public void ReproducirSonidoColision()
    {
        if (sonidoColisionPelota != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sonidoColisionPelota);
        }
    }
}