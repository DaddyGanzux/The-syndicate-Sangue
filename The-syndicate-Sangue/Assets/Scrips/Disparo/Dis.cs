using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public int da�o = 10;

    public void OnPointerDown(PointerEventData eventData)//Implementacion de la interfaz IPointerDownHandler
    {
        Debug.Log("Bot�n presionado sobre " + gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)//Implementacion de la interfaz IPointerUpHandler
    {
        Debug.Log("Bot�n soltado sobre " + gameObject.name);
    }
}