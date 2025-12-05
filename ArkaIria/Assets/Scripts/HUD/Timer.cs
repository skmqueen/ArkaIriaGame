using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoTiempo;

    public static float tiempoTranscurrido = 0f;
    private bool contando = true;

    void Update()
    {
        if (contando)
        {
            tiempoTranscurrido += Time.deltaTime;
            MostrarTiempo();
        }
    }

    void MostrarTiempo()
    {
        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60f);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

            textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    public void DetenerTiempo()
    {
        contando = false;
    }
}
