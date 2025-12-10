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


    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }


    public void Restart(string SampleScene)
    {

        if (Score.instance != null)
            Score.instance.ResetearScore();

        // Reiniciar tiempo
        Timer.ReiniciarTiempo();

        // Forzar estado neutral
        GameController controlador = Object.FindFirstObjectByType<GameController>();
        if (controlador != null)
            controlador.CambiarEstado(new EstadoNeutral());

        Bloque.ResetContadores();



        SceneManager.LoadScene(SampleScene);
    }

 
    public void GameOver(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }


    public void Victoria(string Victoria)
    {

        if (Score.instance != null)
            Score.instance.GuardarPuntosFinales();

        SceneManager.LoadScene(Victoria);
    }
}
