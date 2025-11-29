using UnityEngine;

public class Pelota : MonoBehaviour
{
    float playerOffsetY = 0.4f;
    float velocidad = 12f;
    Rigidbody2D rb;
    bool lanzada = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!lanzada)
        {
            PosicionarSobrePlayer();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lanzar();
            }
        }
    }

    void PosicionarSobrePlayer()
    {
        var player = FindFirstObjectByType<Jugador>();
        var playerPos = player.transform.position;

        transform.position = playerPos + new Vector3(0, playerOffsetY, 0);
    }


    void Lanzar()
    {
        var dir = new Vector2(2f, 1f);
        rb.linearVelocity = dir.normalized * velocidad;
        lanzada = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var contacto = collision.GetContact(0);
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, contacto.normal);
    }
}
