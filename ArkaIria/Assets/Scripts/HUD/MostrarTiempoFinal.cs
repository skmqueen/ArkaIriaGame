using UnityEngine;
using TMPro;

public class MostrarTiempoFinal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoTiempoFinal;

    void Start()
    {
        MostrarTiempo();
    }

    void MostrarTiempo()
    {
        if (textoTiempoFinal != null)
        {
            int minutos = Mathf.FloorToInt(Timer.tiempoTranscurrido / 60f);
            int segundos = Mathf.FloorToInt(Timer.tiempoTranscurrido % 60f);
            
            textoTiempoFinal.text = string.Format("Tiempo: {0:00}:{1:00}", minutos, segundos);
        }
    }
}