using UnityEngine;
using UnityEngine.EventSystems;

public class Seleccion : MonoBehaviour, IPointerClickHandler
{
    GameObject objetoSeleccionado;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerPress; // El objeto sobre el que hiciste clic
        // Aquí puedes manejar el evento de clic
        Debug.Log("Objeto clickeado: " + gameObject.name);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
