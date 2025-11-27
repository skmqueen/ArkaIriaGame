using UnityEngine;

public class Jugador : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    private float movimientoValor;

    public float velocidadJugador = 25f;

    private Vector2 direccion;


    void Start()
    {
        
    }

    void Update()
    {
        movimientoValor = Input.GetAxisRaw("Horizontal");

        if(movimientoValor == 1)
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

        rigidBody2D.AddForce(direccion * velocidadJugador * Time.deltaTime * 100);
    }
}
