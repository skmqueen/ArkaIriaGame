using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject bloqueNormal; // 1 vida, 10 puntos
    [SerializeField] private GameObject bloqueDuro; // 2 vidas, 20 puntos
    [SerializeField] private GameObject bloqueHierro; // 4 vidas, 40 puntos
    
    [SerializeField] private GameObject[] posicionesBloques;
    [SerializeField] private GameObject Pausa;
    private int maxBloquesHierro = 10;
    private int puntuacion = 0;
    private int bloquesTotales = 0;

    void Start()
    {
        //Pausa.setActive(false);
        GenerarBloques();
    }


    private void Update()
    {

    }

    void GenerarBloques()
    {
        int contadorHierro = 0;
        
        // Recorrer todas las posiciones
        for (int i = 0; i < posicionesBloques.Length; i++)
        {
            // Elegir tipo de bloque aleatorio
            int tipoBloque = Random.Range(0, 3); // 0=Normal, 1=Duro, 2=Hierro
            GameObject prefabSeleccionado = bloqueNormal;
            
            // Seleccionar prefab según tipo
            if (tipoBloque == 1)
            {
                prefabSeleccionado = bloqueDuro;
            }
            else if (tipoBloque == 2 && contadorHierro < maxBloquesHierro)
            {
                prefabSeleccionado = bloqueHierro;
                contadorHierro++;
            }
            
            // Instanciar bloque en la posición del GameObject
            GameObject bloque = Instantiate(prefabSeleccionado, posicionesBloques[i].transform.position, Quaternion.identity, transform);
            
            // Animar todos a la vez
            AnimarBloque(bloque);
            
            bloquesTotales++;
        }
    }

    void AnimarBloque(GameObject bloque)
    {
        bloque.transform.localScale = Vector3.zero;
        
        // Efecto pop
        LeanTween.scale(bloque, Vector3.one, 0.3f)
            .setEase(LeanTweenType.easeOutBounce);
    }

    

}