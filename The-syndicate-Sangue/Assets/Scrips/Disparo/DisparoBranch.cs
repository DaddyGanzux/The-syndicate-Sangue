using UnityEngine;
using UnityEngine.EventSystems;

public class DisparoBreach : MonoBehaviour, IPointerClickHandler
{

    public int daño = 10;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Detecta si el objeto que fue clickeado tiene el componente Vida
        GameObject objetoClickeado = eventData.pointerPress; // El objeto sobre el que hiciste clic

        if (objetoClickeado != null)
        {
            Vida vida = objetoClickeado.GetComponent<Vida>(); // Busca el script Vida
            if (vida != null)
            {
                vida.vida -= daño;
                Debug.Log("Le hiciste " + daño + " de daño a " + objetoClickeado.name);
                Debug.Log("Vida restante: " + vida.vida);
            }
            else
            {
                Debug.Log("Ese objeto no tiene vida.");
            }
        }
    }

    /*
    public Vida objetivo; // Aquí guardaremos el enemigo o personaje al que queremos atacar
    public int daño = 10; // Daño que hace el disparo
    public bool Click = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (objetivo != null) // Si hay objetivo asignado
        {
            objetivo.RecibirDaño(daño); // Le bajamos la vida al objetivo
            Debug.Log(gameObject.name + " atacó a " + objetivo.name);
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
        Debug.Log("Botón presionado sobre " + gameObject.name);
    }
    //+----------------------------------------------------------------
    public void OnPointerUp (PointerEventData eventData)//Implementacion de la interfaz IPointerUpHandler
    {
        Click = false;
        Debug.Log("Botón soltado sobre " + gameObject.name);
    }
    +----------------------------------------------------------------

    private void Start()
    {
        
    }
    */
}
