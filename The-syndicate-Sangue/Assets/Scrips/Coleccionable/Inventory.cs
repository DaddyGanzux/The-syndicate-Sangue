using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        for (int i = 0; i < slots.Count; i++)
        {
            Transform newSlot = Instantiate(slotPrefab, slotsContainer);
            slots.Add(newSlot);
        }

        isOpen = true;
        ToogleInventory();
    }

    public void ToogleInventory()
    {
       GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
       inventoryToggle.SetActive(!isOpen);
       isOpen = !isOpen;
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




}
