using UnityEngine;

public class Bloque : MonoBehaviour
{
    [SerializeField]
    int vidasMaximas = 1; // 1=Normal, 2=Dura, 4=Hierro
    [SerializeField] 
    int puntos = 10; // 10=Normal, 20=Dura, 40=Hierro
    
    private int vidasActuales;
    private SpriteRenderer spriteRenderer;
    private int bloquesDestruidos;
    private int bloquesActivos = 50;

    void Start()
    {
        vidasActuales = vidasMaximas;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con la pelota
        if (collision.gameObject.CompareTag("Pelota"))
        {
            RecibirGolpe();
        }
    }

    void RecibirGolpe()
    {
        vidasActuales--;
        
        // Cambiar apariencia según vidas restantes
        ActualizarApariencia();
        
        if (vidasActuales <= 0)
        {
            DestruirBloque();
        }
    }

    void ActualizarApariencia()
    {
        // Hacer más transparente según vidas restantes
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
        // Añadir puntos usando el sistema Score
        Score.instance.SumarPuntos(puntos);
        bloquesDestruidos++;

        if(bloquesDestruidos >= bloquesActivos)
        {
            Menus.instance.Victoria("Victoria");
        }
        // Destruir el bloque
        Destroy(gameObject);
    }
}