using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour, IPointerClickHandler// IPointerClickHandler: Detecta los clicks del raton
{
    [SerializeField] // Hace que aunque la referencia se a privada, se pueda asignar desde el inspector
    private Database db; // Referencia privada a la base de datos de los coleccionables

    public int id;

    [HideInInspector] // Hace que la variable no se muestre en el inspector apesar de ser publica
    public Database.InventoryItem itemData; //Es un objeto que rexupera las propiedades de InventoryItem de Database.cs

    Image iconoImage; // Referencia a la imagen del icono del coleccionable

    void Awake()
    {
        iconoImage = transform.GetComponent<Image>();

        InitializedItem(id);
    }

    public void InitializedItem(int id)
    {
        itemData.ID = id;
        itemData.description = db.dataBase[id].description;
        itemData.icon = db.dataBase[id].icon;
        itemData.name = db.dataBase[id].name;

        iconoImage.sprite = itemData.icon;
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