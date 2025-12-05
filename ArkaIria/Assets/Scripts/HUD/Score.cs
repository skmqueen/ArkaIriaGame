using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TextMeshProUGUI textoScore; 
    private int puntos;
    public static int puntosFinales;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        puntos = 0;
        ActualizarTexto();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarTexto();
    }

    public void ResetearScore()
    {
        puntos = 0;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoScore != null)
            textoScore.text = puntos.ToString();
    }

    public int ObtenerPuntos()
    {
        return puntos;
    }
    public void GuardarPuntosFinales()
    {
        puntosFinales = puntos;
    }
}