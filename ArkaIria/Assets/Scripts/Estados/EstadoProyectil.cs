using UnityEngine;

public class EstadoDisparo : IEstado
{
    private float tiempoRestante = 10f;
    private bool estadoActivo = false;

    public void Entrar(GameController controlador)
    {
        Debug.Log("Estado: Modo Disparo activado - 10 segundos");
        tiempoRestante = 10f;
        estadoActivo = true;
    }

    public void Ejecutar(GameController controlador)
    {
        if (!estadoActivo) return;

        tiempoRestante -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            controlador.DispararProyectil();
        }

        if (tiempoRestante <= 0f)
        {
            estadoActivo = false;
            controlador.CambiarEstado(new EstadoNeutral());
        }
    }

    public void Salir(GameController controlador)
    {
        Debug.Log("Estado: Modo Disparo finalizado");
        estadoActivo = false;
    }

    public float ObtenerTiempoRestante()
    {
        return tiempoRestante;
    }
}