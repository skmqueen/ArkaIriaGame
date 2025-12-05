using UnityEngine;

public class Bloque : MonoBehaviour
{
    [SerializeField] int vidasMaximas = 1;
    [SerializeField] int puntos = 10;

    [Header("Partículas")]
    [SerializeField] private GameObject particulasPrefab; // <--- PREFAB AQUÍ

    private int vidasActuales;
    private SpriteRenderer spriteRenderer;

    private static int bloquesDestruidos = 0; 
    private static int bloquesActivos = 50;

    void Start()
    {
        vidasActuales = vidasMaximas;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pelota"))
        {
            RecibirGolpe();
        }
    }

    void RecibirGolpe()
    {
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
        Score.instance.SumarPuntos(puntos);
        bloquesDestruidos++;

     if (particulasPrefab != null)
{
    // Instanciamos las partículas en la posición del bloque
    GameObject fx = Instantiate(particulasPrefab, transform.position, Quaternion.identity);

    // Accedemos al ParticleSystem
    ParticleSystem ps = fx.GetComponent<ParticleSystem>();
    if (ps != null)
    {
        var main = ps.main;

        // Creamos el gradient amarillo → azul → rojo
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.yellow, 0f), // inicio
                new GradientColorKey(Color.blue, 0.5f),  // medio
                new GradientColorKey(Color.red, 1f)     // final
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f), // opacidad al inicio
                new GradientAlphaKey(1f, 0.5f),
                new GradientAlphaKey(0f, 1f)  // desaparece al final
            }
        );

        main.startColor = grad;
    }

    // Destruir el sistema de partículas automáticamente después de 2 segundos
    Destroy(fx, 2f);
}

        // ----------------------------------

        if (bloquesDestruidos >= bloquesActivos)
        {
            Menus.instance.Victoria("Victoria");
        }

        Destroy(gameObject);
    }
}
