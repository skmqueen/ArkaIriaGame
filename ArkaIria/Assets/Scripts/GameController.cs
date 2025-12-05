using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject bloqueNormal; // 1 vida, 10 puntos
    [SerializeField] private GameObject bloqueDuro;   // 2 vidas, 20 puntos
    [SerializeField] private GameObject bloqueHierro; // 4 vidas, 40 puntos
    
    [SerializeField] private GameObject[] posicionesBloques;
    [SerializeField] private GameObject Pausa;
    [SerializeField] private GameObject mensaje;
    
    private int maxBloquesHierro = 10;
    private int bloquesTotales = 0;
    private float tiempoMensaje = 3f;


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
        mensaje.SetActive(true);
        Image img = mensaje.GetComponentInChildren<Image>();
        PopInImagen(img);
    }


    private void Update()
    {
        tiempoMensaje -= Time.deltaTime;
        if (tiempoMensaje <= 0f)
        {
            mensaje.SetActive(false);
        }
    }

    void GenerarBloques()
    {
        int contadorHierro = 0;
        
        for (int i = 0; i < posicionesBloques.Length; i++)
        {
            int tipoBloque = Random.Range(0, 3); // 0=Normal, 1=Duro, 2=Hierro
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
            
            AnimarBloque(bloque);
            bloquesTotales++;
        }
    }


    void AnimarBloque(GameObject bloque)
    {
        bloque.transform.localScale = Vector3.zero;

        LeanTween.scale(bloque, Vector3.one, 0.3f)
            .setEase(LeanTweenType.easeOutBounce);
    }
}
