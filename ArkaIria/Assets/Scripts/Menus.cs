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
    public void Salir()
    {
        Application.Quit();
    }

    public void Restart(string SampleScene)
    {
        SceneManager.LoadScene(SampleScene);
    }

    public void GameOver(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }

    public void Victoria(string Victoria)
    {
        SceneManager.LoadScene(Victoria);
    }
}
