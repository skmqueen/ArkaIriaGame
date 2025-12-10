using UnityEngine;

public class Vidas : MonoBehaviour
{
    public static Vidas instance;

    public GameObject[] corazones;
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
            Score.instance.GuardarPuntosFinales();
            Menus.instance.GameOver("GameOver");
        }
    }
}
