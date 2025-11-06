using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform itemSpawn;
    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.ToogleInventory();
        }

    }
}
