using UnityEngine;

public class EstadoDestructorTotal : IEstado
{
    private bool powerUpUsado = false;
    Rigidbody2D rb;

    public void Entrar(GameController controlador)
    {
        Debug.Log("Estado: Destructor Total activado - La próxima colisión destruirá todos los bloques");
        powerUpUsado = false;
    }

    public void Ejecutar(GameController controlador)
    {
    }

    public void Salir(GameController controlador)
    {

        Debug.Log("Estado: Destructor Total finalizado");
    }

    public void UsarPowerUp(GameController controlador)
    {
        if (powerUpUsado) return;

        Debug.Log("¡Power-Up usado! Destruyendo todos los bloques...");

        Bloque[] todosLosBloques = controlador.GetComponentsInChildren<Bloque>();

        foreach (Bloque bloque in todosLosBloques)
        {
            if (bloque != null && bloque.gameObject != null)
            {
                bloque.DestruirInstantaneamente();
            }
        }

        powerUpUsado = true;
        controlador.CambiarEstado(new EstadoNeutral());
    }
}

