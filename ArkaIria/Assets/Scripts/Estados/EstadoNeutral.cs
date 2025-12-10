using UnityEngine;

// Estado neutral del juego - comportamiento normal
public class EstadoNeutral : IEstado
{
    public void Entrar(GameController controlador)
    {
        Debug.Log("Estado: Neutral - Juego normal");
    }

    public void Ejecutar(GameController controlador)
    {
        // En estado neutral, el juego funciona normalmente
        // No hay lógica especial aquí
    }

    public void Salir(GameController controlador)
    {
        Debug.Log("Saliendo del estado neutral");
    }
}