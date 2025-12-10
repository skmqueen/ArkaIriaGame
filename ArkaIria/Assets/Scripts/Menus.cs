using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public static Menus instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Cargar la siguiente escena en el Build Settings
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }

    // Reiniciar la partida cargando una escena específica
    public void Restart(string SampleScene)
    {
        // Reiniciar puntuación
        if (Score.instance != null)
            Score.instance.ResetearScore();

        // Reiniciar tiempo
        Timer.ReiniciarTiempo();

        // Forzar estado neutral
        GameController controlador = Object.FindFirstObjectByType<GameController>();
        if (controlador != null)
            controlador.CambiarEstado(new EstadoNeutral());

        Bloque.ResetContadores();


        // Cargar la escena indicada
        SceneManager.LoadScene(SampleScene);
    }

    // Cargar escena de Game Over
    public void GameOver(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }

    // Cargar escena de Victoria
    public void Victoria(string Victoria)
    {
        // Guardar puntuación final antes de cambiar de escena
        if (Score.instance != null)
            Score.instance.GuardarPuntosFinales();

        SceneManager.LoadScene(Victoria);
    }
}
