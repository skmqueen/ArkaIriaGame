using UnityEngine;

// Script para el prefab del power-up que cae
public class PowerUpDestructor : MonoBehaviour
{
    [SerializeField] private float velocidadCaida = 3f;

    void Update()
    {
        // El power-up cae hacia abajo
        transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime);

        // Destruir si sale de la pantalla
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Jugador"))
        {
            // Buscar el GameController y cambiar al estado destructor
            GameController gameController = Object.FindFirstObjectByType<GameController>();
            
            if (gameController != null)
            {
                gameController.CambiarEstado(new EstadoDestructorTotal());
            }
            else
            {
            }
            
            // Destruir el power-up
            Destroy(gameObject);
        }

        if(collision.CompareTag("Muerte"))
        {
            Destroy(gameObject);
        }
    }
}