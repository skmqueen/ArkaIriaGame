using UnityEngine;

public class Pelota : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    public float velocidad = 400f;

    private Vector2 velocidadPelota;

    private Transform jugador;

    public float minY;

    private bool enMovimiento = false;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {


 

    }
}
