using UnityEngine;

[System.Serializable]
public abstract class BaseItem : MonoBehaviour
{
    public int id;

    public Database.InventoryItem itemData;

    private void Start()
    {
        SetDataById(id);
    }

    public void SetDataById(int id)
    {
        itemData.ID = id;
        itemData.description = Inventory.Instance.db.dataBase[id].description;
        itemData.icon = Inventory.Instance.db.dataBase[id].icon;
        itemData.name = Inventory.Instance.db.dataBase[id].name;
    }

    public abstract void Use();

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("player"))
        {
            Inventory.Instance.AddItem(id);
            Destroy(this.gameObject);
        }
    }

}
