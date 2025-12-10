using UnityEngine;

public class EstadoDestructorTotal : IEstado
{
    private bool powerUpUsado = false;
    Rigidbody2D rb;

    public void Entrar(GameController controlador)
    {
        powerUpUsado = false;
    }

    public void Ejecutar(GameController controlador)
    {
    }

    public void Salir(GameController controlador)
    {

    }

    public void UsarPowerUp(GameController controlador)
    {
        if (powerUpUsado) return;

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

