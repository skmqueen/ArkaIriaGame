using UnityEngine;
using UnityEngine.SceneManagement;

public class Bloque : MonoBehaviour
{
    [SerializeField] int vidasMaximas = 1;
    [SerializeField] int puntos = 10;

    [Header("Partículas")]
    [SerializeField] private GameObject particulasPrefab;

    private int vidasActuales;
    private SpriteRenderer spriteRenderer;
    private GameObject powerUpPrefab;

    private static int bloquesDestruidos = 0; 
    private static int bloquesActivos = 18;

    void Start()
    {
        vidasActuales = vidasMaximas;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ConfigurarPowerUp(GameObject prefabPowerUp)
    {
        powerUpPrefab = prefabPowerUp;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pelota"))
        {
            GameController gameController = Object.FindFirstObjectByType<GameController>();
            
            if (gameController != null)
            {
                IEstado estadoActual = gameController.ObtenerEstadoActual();
                
                if (estadoActual is EstadoDestructorTotal estadoDestructor)
                {
                    estadoDestructor.UsarPowerUp(gameController);
                    return;
                }
            }
            
            RecibirGolpe();
        }
    }

    public void RecibirGolpe()
    {
        AudioManager.instance.ReproducirSonidoColision();
        vidasActuales--;
        ActualizarApariencia();

        if (vidasActuales <= 0)
        {
            DestruirBloque();
        }
    }

    void ActualizarApariencia()
    {
        if (spriteRenderer != null)
        {
            float alpha = (float)vidasActuales / vidasMaximas;
            Color color = spriteRenderer.color;
            color.a = Mathf.Max(alpha, 0.5f);
            spriteRenderer.color = color;
        }
    }

    void DestruirBloque()
    {
        if (Score.instance != null)
            Score.instance.SumarPuntos(puntos);

        bloquesDestruidos++;

        if (powerUpPrefab != null)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }

        GenerarParticulas();

        if (bloquesDestruidos >= bloquesActivos)
        {
            Menus.instance.Victoria("Victoria");
        }

        Destroy(gameObject);
    }

    public void DestruirInstantaneamente()
    {
        if (Score.instance != null)
            Score.instance.SumarPuntos(puntos);

        if (powerUpPrefab != null)
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);

        GenerarParticulas();

        bloquesDestruidos++;

        if (bloquesDestruidos >= bloquesActivos)
        {
            GameController gc = Object.FindFirstObjectByType<GameController>();
            if (gc != null)
            {
                gc.IniciarContadorVictoria(3f);
            }
        }

        Destroy(gameObject);
    }

    void GenerarParticulas()
    {
        if (particulasPrefab != null)
        {
            GameObject fx = Instantiate(particulasPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = fx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                Gradient grad = new Gradient();
                grad.SetKeys(
                    new GradientColorKey[] {
                        new GradientColorKey(Color.yellow, 0f),
                        new GradientColorKey(Color.blue, 0.5f),
                        new GradientColorKey(Color.red, 1f)
                    },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1f, 0f),
                        new GradientAlphaKey(1f, 0.5f),
                        new GradientAlphaKey(0f, 1f)
                    }
                );
                main.startColor = grad;
            }
            Destroy(fx, 2f);
        }
    }
    
    public static void ResetContadores()
    {
        bloquesDestruidos = 0;
    }
}
