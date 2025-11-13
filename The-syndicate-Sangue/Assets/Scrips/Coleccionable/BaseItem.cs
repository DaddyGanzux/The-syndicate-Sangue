using UnityEngine;

[System.Serializable]
public abstract class BaseItem : MonoBehaviour
{
    public int id;
    public Database.InventoryItem itemData;

    private void Start()
    {
        if (Inventory.Instance != null && Inventory.Instance.bd != null)
        {
            SetDataById(id);
        }
        else
        {
            Debug.LogError("Inventory or Database (bd) not ready when BaseItem started! Check script execution order.");
        }
    }

    public void SetDataById(int id)
    {
        itemData.ID = id;
        itemData.description = Inventory.Instance.bd.dataBase[id].description;
        itemData.icon = Inventory.Instance.bd.dataBase[id].icon;
        itemData.name = Inventory.Instance.bd.dataBase[id].name;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("player"))
        {
            Inventory.Instance.AddItem(id); // Llama al método del Inventario para asignar el ítem por su ID.

            Destroy(this.gameObject); // 2. Destruye el objeto físico del mundo.
        }
    }

}
