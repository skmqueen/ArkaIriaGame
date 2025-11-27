using UnityEngine;

public class Pelota : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    public float velocidad = 400f;

    private Vector2 velocidadPelota;

    private Transform jugador;

    private bool enMovimiento = false;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        PosicionInicial();
    }

    // Update is called once per frame
    void Update()
    {
        if (enMovimiento == false && Input.GetKeyDown(KeyCode.Space))
        {
            LanzarPelota();
        }
    }

    private void FixedUpdate()
    {
        if (enMovimiento)
        {
            float velocidadActual = 
        }
    }
}
