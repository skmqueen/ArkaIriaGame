using UnityEngine;
using TMPro;

public class MostrarPuntosFinales : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntosFinal;

    void Start()
    {
        if (textoPuntosFinal != null)
        {
            textoPuntosFinal.text = "Puntos: " + Score.puntosFinales.ToString();
        }
    }
}