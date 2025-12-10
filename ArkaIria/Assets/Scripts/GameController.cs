using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject bloqueNormal;
    [SerializeField] private GameObject bloqueDuro;
    [SerializeField] private GameObject bloqueHierro;
    
    [SerializeField] private GameObject[] posicionesBloques;
    [SerializeField] private GameObject Pausa;
    [SerializeField] private GameObject mensaje;
    [SerializeField] private GameObject powerUpDestructorPrefab;
    
    private int maxBloquesHierro = 10;
    private int bloquesTotales = 0;
    private float tiempoMensaje = 3f;
    private float tiempoEsperaVictoria = -1f;

    private IEstado estadoActual;

    private void PopInImagen(Image img)
    {
        if (img == null)
        {
            return;
        }

        RectTransform rt = img.rectTransform;
        rt.localScale = Vector3.zero;

        LeanTween.scale(rt, Vector3.one, 0.4f).setEaseOutBack();
    }

    void Start()
    {
        GenerarBloques();
        Timer.ReiniciarTiempo();
        mensaje.SetActive(true);
        Image img = mensaje.GetComponentInChildren<Image>();
        PopInImagen(img);

        CambiarEstado(new EstadoNeutral());
    }

    private void Update()
    {
        tiempoMensaje -= Time.deltaTime;
        if (tiempoMensaje <= 0f)
        {
            mensaje.SetActive(false);
        }

        if (tiempoEsperaVictoria > 0f)
        {
            tiempoEsperaVictoria -= Time.deltaTime;
            if (tiempoEsperaVictoria <= 0f)
            {
                Menus.instance.Victoria("Victoria");
                tiempoEsperaVictoria = -1f;
            }
        }

        if (estadoActual != null)
        {
            estadoActual.Ejecutar(this);
        }
    }

    void GenerarBloques()
    {
        int contadorHierro = 0;
        System.Collections.Generic.List<GameObject> bloquesRojosGenerados = new System.Collections.Generic.List<GameObject>();
        
        for (int i = 0; i < posicionesBloques.Length; i++)
        {
            int tipoBloque = Random.Range(0, 3);
            GameObject prefabSeleccionado = bloqueNormal;
            
            if (tipoBloque == 1)
            {
                prefabSeleccionado = bloqueDuro;
            }
            else if (tipoBloque == 2 && contadorHierro < maxBloquesHierro)
            {
                prefabSeleccionado = bloqueHierro;
                contadorHierro++;
            }
            
            GameObject bloque = Instantiate(
                prefabSeleccionado, 
                posicionesBloques[i].transform.position, 
                Quaternion.identity, 
                transform
            );
            
            if (prefabSeleccionado == bloqueHierro)
            {
                bloquesRojosGenerados.Add(bloque);
            }
            
            AnimarBloque(bloque);
            bloquesTotales++;
        }

        if (bloquesRojosGenerados.Count > 0 && powerUpDestructorPrefab != null)
        {
            int indiceAleatorio = Random.Range(0, bloquesRojosGenerados.Count);
            GameObject bloqueSeleccionado = bloquesRojosGenerados[indiceAleatorio];
            
            Bloque scriptBloque = bloqueSeleccionado.GetComponent<Bloque>();
            if (scriptBloque != null)
            {
                scriptBloque.ConfigurarPowerUp(powerUpDestructorPrefab);
                Debug.Log($"Bloque rojo seleccionado para soltar power-up en posición: {bloqueSeleccionado.transform.position}");
            }
        }
    }

    void AnimarBloque(GameObject bloque)
    {
        bloque.transform.localScale = Vector3.zero;

        LeanTween.scale(bloque, Vector3.one, 0.3f)
            .setEase(LeanTweenType.easeOutBounce);
    }

    public void CambiarEstado(IEstado nuevoEstado)
    {
        if (estadoActual != null)
        {
            estadoActual.Salir(this);
        }

        estadoActual = nuevoEstado;

        if (estadoActual != null)
        {
            estadoActual.Entrar(this);
        }
    }

    public IEstado ObtenerEstadoActual()
    {
        return estadoActual;
    }

    public void IniciarContadorVictoria(float segundos)
    {
        tiempoEsperaVictoria = segundos;
    }
}