using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float playerOffsetY = 0.4f;
    public float velocidad = 8f;
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
            if (Input.GetButtonDown("Jump"))
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
        AudioManager.instance.ReproducirSonidoSacarPelota();
        var dir = new Vector2(0f, 1f);
        rb.linearVelocity = dir.normalized * velocidad;
        lanzada = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var contacto = collision.GetContact(0);
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, contacto.normal);
        CorregirAngulo();
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Muerte"))
    {
        Vidas.instance.PerderVida();
        ReiniciarPelota();
    }
}

    void ReiniciarPelota()
    {
        lanzada = false;
        rb.linearVelocity = Vector2.zero;
        PosicionarSobrePlayer();
    }

    void CorregirAngulo()
    //Script para que la pelota tenga las direcciones lo más parecidas a la original
    {
        float anguloMinimo = 15f;
        Vector2 vel = rb.linearVelocity;
        
        float angulo = Mathf.Abs(Vector2.Angle(vel, Vector2.right));
        
        if (angulo > 90f - anguloMinimo && angulo < 90f + anguloMinimo)
        {
            float signoX = vel.x >= 0 ? 1f : -1f;
            float signoY = vel.y >= 0 ? 1f : -1f;
            
            vel = new Vector2(signoX * 2f, signoY * 1f).normalized * velocidad;
            rb.linearVelocity = vel;
        }
    }

}



