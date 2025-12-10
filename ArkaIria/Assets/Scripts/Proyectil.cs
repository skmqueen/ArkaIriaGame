using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float tiempoVida = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        // Movimiento hacia adelante según la rotación del proyectil
        transform.Translate(transform.up * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Bloque bloque = other.GetComponent<Bloque>();
        if (bloque != null)
        {
            bloque.RecibirGolpe();
            Destroy(gameObject);
        }
    }
}
