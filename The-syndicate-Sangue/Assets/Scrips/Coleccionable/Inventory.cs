using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public GraphicRaycaster graphRay;
    public Database bd;
    public int slotsCount = 18;
    public bool isOpen; // Indica cuando esta abierto el inventario

    public bool IsDescriptionShowing = false;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject inventoryToggle;

    [SerializeField]
    private Transform slotPrefab;

    [SerializeField]
    private Transform itemPrefab;

    //public DeletionPrompt deletionPrompt;

    public DescriptionUI descriptionUI;

    [SerializeField]
    private List<ItemUI> items = new List<ItemUI>();

    [SerializeField]
    private Transform slotsContainer;

    private List<Transform> slots = new List<Transform>();

    private List<bool> isSlotOccupied = new List<bool>();

    // Nos permite tener una instancia unica del inventario que es accesible desde cualquier otro lado
    public static Inventory Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            Transform newSlot = Instantiate(slotPrefab, slotsContainer);
            slots.Add(newSlot);
            isSlotOccupied.Add(false); // inicializa para que todos los slots esten vacios al inicio.
        }

        isOpen = true;
        ToogleInventory(); // Esto lo abre si isOpen era false, o lo cierra si era true.
    }

    public void ToogleInventory()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Mantiene el inventario centrado

        inventoryToggle.SetActive(!isOpen); // Esto oculta/muestra el Canvas o el contenedor del inventario

        isOpen = !isOpen; // Invierte el estado
    }

    public void ShowDescription(ItemUI item)
    {
        descriptionUI.gameObject.SetActive(true);
        descriptionUI.Show(item);

        IsDescriptionShowing = true;
    }

    public void HideDescription()
    {
        descriptionUI.gameObject.SetActive(false);

        IsDescriptionShowing = false;
    }

    public void AddItem(int id)
    {
        // Buscar el primer slot vacio
        int firstEmptySlotIndex = -1;
        for (int i = 0; i < isSlotOccupied.Count; i++)
        {
            if (isSlotOccupied[i] == false)
            {
                firstEmptySlotIndex = i;
                break;
            }
        }

        if (firstEmptySlotIndex != -1)
        {
            // Obtener el slot y asignar el item
            Transform targetSlot = slots[firstEmptySlotIndex];
            Transform newItemTransform = Instantiate(itemPrefab, targetSlot);
            ItemUI newItemUI = newItemTransform.GetComponent<ItemUI>();

            // inicializa el ItemUI con la Database del Inventario
            newItemUI.InitializedItem(id, bd);

            // marca como ocupado 
            isSlotOccupied[firstEmptySlotIndex] = true;
            items.Add(newItemUI);
        }
        else
        {
        }
    }




}
