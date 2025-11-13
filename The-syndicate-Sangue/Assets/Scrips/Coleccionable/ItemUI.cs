using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour, IPointerClickHandler // IPointerClickHandler: Detecta los clicks del raton
{
    [SerializeField] // Hace que aunque la referencia sea privada, se pueda asignar desde el inspector
    private Database bd; // Referencia privada a la base de datos de los coleccionables

    public int id;

    [HideInInspector] // Hace que la variable no se muestre en el inspector apesar de ser publica
    public Database.InventoryItem itemData; //Es un objeto que rexupera las propiedades de InventoryItem de Database.cs

    Image iconoImage; // Referencia a la imagen del icono del coleccionable

    public void InitializedItem(int id, Database database)
    {
        itemData.ID = id;
        // Usa la database pasada como argumento
        itemData.description = database.dataBase[id].description;
        itemData.icon = database.dataBase[id].icon;
        itemData.name = database.dataBase[id].name;

        iconoImage.sprite = itemData.icon;
    }
    void Awake()
    {
        iconoImage = transform.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            if (!Inventory.Instance.IsDescriptionShowing)
            {
                Inventory.Instance.ShowDescription(this);
            }
            else
            {
                Inventory.Instance.HideDescription();
            }
        }
    }
}