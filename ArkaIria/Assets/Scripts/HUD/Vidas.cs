using UnityEngine;

public class Vidas : MonoBehaviour
{
    public static Vidas instance;

    public GameObject[] corazones; // Array con los corazones del canvas
    private int vidas;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        vidas = corazones.Length;
    }

    public void PerderVida()
    {
        vidas--;
        corazones[vidas].SetActive(false);

        if (vidas == 0)
        {
            Debug.Log("Game Over");
            // Aquí puedes añadir lógica de reinicio o menú de Game Over
            Score.instance.GuardarPuntosFinales();
            Menus.instance.GameOver("GameOver");
        }
    }
}
