using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisparoBranch : MonoBehaviour, IPointerClickHandler 
{
    Vida vida; //Referencia al script Vida
    public int da�o = 10;

    public void OnPointerClick(PointerEventData eventData)//Implementacion de la interfaz IPointerClickHandler
    {
        // Detecta si el objeto que fue clickeado tiene el componente Vida
        GameObject objetoClickeado = eventData.pointerPress; // El objeto sobre el que hiciste clic

        Debug.Log("Se llamo al click ");

        if (objetoClickeado != null)//Verifica que el objeto no sea nulo
        {
            Vida vida = objetoClickeado.GetComponent<Vida>(); // Busca el script Vida
            if (vida != null)//Verifica que el objeto tenga el componente Vida
            {
                vida.vida -= da�o;// Resta el da�o a la vida
                Debug.Log("Le hiciste " + da�o + " de da�o a " + objetoClickeado.gameObject.name);// Muestra en consola el da�o hecho
                Debug.Log("Vida restante: " + vida.vida);// Muestra en consola la vida restante
            }
            else
            {
                Debug.Log("Ese objeto no tiene vida.");// Mensaje si el objeto no tiene el componente Vida
            }
        }
    }

    /*
    public Vida objetivo; // Aqu� guardaremos el enemigo o personaje al que queremos atacar
    public int da�o = 10; // Da�o que hace el disparo
    public bool Click = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (objetivo != null) // Si hay objetivo asignado
        {
            objetivo.RecibirDa�o(da�o); // Le bajamos la vida al objetivo
            Debug.Log(gameObject.name + " atac� a " + objetivo.name);
        }
        else
        {
            Debug.LogWarning("No hay objetivo asignado para el ataque.");
        }

        Click = true;
    }

    /*----------------------------------------------------------------
    public void OnPointerDown (PointerEventData eventData)//Implementacion de la interfaz IPointerDownHandler
    {
        Vida vida = GetComponent<Vida>();//Referencia al script Vida    situado en el mismo objeto
        vida.vida -= 10;//Resta 10 a la vida

        Debug.Log("Vida actual de " + gameObject.name + ": " + vida.vida);

        Click = true;
        Debug.Log("Bot�n presionado sobre " + gameObject.name);
    }
    //+----------------------------------------------------------------
    public void OnPointerUp (PointerEventData eventData)//Implementacion de la interfaz IPointerUpHandler
    {
        Click = false;
        Debug.Log("Bot�n soltado sobre " + gameObject.name);
    }
    +----------------------------------------------------------------

    private void Start()
    {
        
    }
    */
}