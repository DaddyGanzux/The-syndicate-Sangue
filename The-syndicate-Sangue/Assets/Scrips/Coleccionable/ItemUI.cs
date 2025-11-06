using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour, IPointerClickHandler, TPointerEnterHadler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Database db;

    [SerializeField]
    private GameObject delateButton;

    public int id;
    public int quantity;

    [HideInInspector]
    public Database.InventoryItem itemData;
    [HideInInspector]
    public Transform exParent;

    TextMeshProUGUI quantityText;
    Image iconoImage;
    Vector3 dragOffset;



}
