using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public int daño = 10;

    public void OnPointerDown(PointerEventData eventData)//Implementacion de la interfaz IPointerDownHandler
    {
        Debug.Log("Botón presionado sobre " + gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)//Implementacion de la interfaz IPointerUpHandler
    {
        Debug.Log("Botón soltado sobre " + gameObject.name);
    }
}