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
    [SerializeField] private GameObject powerUpDisparoPrefab;
    [SerializeField] private GameObject proyectilPrefab;
    
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
        System.Collections.Generic.List<GameObject> bloquesNormalesGenerados = new System.Collections.Generic.List<GameObject>();
        
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
            else if (prefabSeleccionado == bloqueNormal)
            {
                bloquesNormalesGenerados.Add(bloque);
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
                Debug.Log("Bloque rojo seleccionado para soltar power-up destructor");
            }
        }

        if (bloquesNormalesGenerados.Count > 0 && powerUpDisparoPrefab != null)
        {
            int indiceAleatorio = Random.Range(0, bloquesNormalesGenerados.Count);
            GameObject bloqueSeleccionado = bloquesNormalesGenerados[indiceAleatorio];
            
            Bloque scriptBloque = bloqueSeleccionado.GetComponent<Bloque>();
            if (scriptBloque != null)
            {
                scriptBloque.ConfigurarPowerUp(powerUpDisparoPrefab);
                Debug.Log("Bloque normal seleccionado para soltar power-up de disparo");
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

    public void DispararProyectil()
    {
        Jugador jugador = FindFirstObjectByType<Jugador>();
        if (jugador != null && proyectilPrefab != null)
        {
            Vector3 posicionDisparo = jugador.transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(proyectilPrefab, posicionDisparo, Quaternion.identity);
            
            if (AudioManager.instance != null)
            {
                AudioManager.instance.ReproducirSonidoColision();
            }
        }
    }
}