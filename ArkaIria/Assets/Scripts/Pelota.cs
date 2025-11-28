using UnityEngine;

public class Pelota : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float anguloMinimo = 15f;
    [SerializeField] private float anguloMaximo = 75f;
    [SerializeField] private Transform paleta;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, 0f);
    
    private Rigidbody2D rb;
    private bool enMovimiento = false;
    private bool primerRebote = false; // Para permitir salida vertical

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        // Pelota sigue a la paleta hasta el saque
        if (!enMovimiento)
        {
            transform.position = paleta.position + offset;
            
            // Lanzar pelota en vertical
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.linearVelocity = Vector2.up * velocidad;
                enMovimiento = true;
                primerRebote = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (enMovimiento && rb.linearVelocity.magnitude > 0.1f)
        {
            // Mantener velocidad constante
            rb.linearVelocity = rb.linearVelocity.normalized * velocidad;
            
            // Solo corregir ángulos después del primer rebote
            if (primerRebote)
            {
                CorregirAngulo();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Activar corrección después del primer rebote
        primerRebote = true;
    }

    void CorregirAngulo()
    {
        Vector2 vel = rb.linearVelocity;
        
        // Evitar movimiento muy horizontal (Y muy pequeña)
        if (Mathf.Abs(vel.y) < 2f)
        {
            vel.y = Mathf.Sign(vel.y) * 2f;
        }
        
        // Evitar movimiento muy vertical (X muy pequeña)
        if (Mathf.Abs(vel.x) < 2f)
        {
            vel.x = Mathf.Sign(vel.x) * 2f;
        }
        
        rb.linearVelocity = vel.normalized * velocidad;
    }
}