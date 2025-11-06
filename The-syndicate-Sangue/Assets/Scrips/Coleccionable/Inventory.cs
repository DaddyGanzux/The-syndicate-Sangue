using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public GraphicRaycaster graphRay;
    public Database bd;
    public int slotsCount = 18;
    [ReadOnly] public bool isOpen; // Indica cuando esta abierto el inventario

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject inventoryToggle;

    [SerializeField]
    private Transform slotPrefab;

    [SerializeField]
    private Transform itemPrefab;

    public DeletionPrompt deletionPrompt;

    public DescriptionUI descriptionUI;

    [SerializeField]
    private List<ItemUI> items = new List<ItemUI>();
    bool itemsDeleteModeEnabled;

    [SerializeField]
    private Transform slotsContainer;

    private List<Transform> slots = new List<Transform>();

    // Nos permite tener una instancia unica del inventario que es accesible desde cualquier otro lado
    public static Inventory instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }



}
