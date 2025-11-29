using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidadJugador = 25f;

    private float movimientoValor;
    private Vector2 direccion;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detecta si el jugador pulsa derecha o izquierda
        movimientoValor = Input.GetAxisRaw("Horizontal");

        if (movimientoValor == 1)
        {
            direccion = Vector2.right;
        }
        else if (movimientoValor == -1)
        {
            direccion = Vector2.left;
        }
        else
        {
            direccion = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Movimiento tipo Arkanoid: directo y sin física realista
        rb.linearVelocity = direccion * velocidadJugador;
    }
}
