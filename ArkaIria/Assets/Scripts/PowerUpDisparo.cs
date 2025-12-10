using UnityEngine;

public class PowerUpDisparo : MonoBehaviour
{
    [SerializeField] private float velocidadCaida = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            GameController gameController = Object.FindFirstObjectByType<GameController>();
            
            if (gameController != null)
            {
                gameController.CambiarEstado(new EstadoDisparo());
            }
            else
            {
            }
            
            Destroy(gameObject);
        }

        if(collision.CompareTag("Muerte"))
        {
            Destroy(gameObject);
        }
    }
}