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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Jugar()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Salir()
    {
        Debug.Log("Salir ... ");
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
