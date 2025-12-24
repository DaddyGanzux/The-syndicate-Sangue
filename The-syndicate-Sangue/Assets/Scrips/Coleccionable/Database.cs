using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Inventory/New Database", order = 1)]
public class Database : ScriptableObject
{
    [System.Serializable]

    public struct InventoryItem
    {
        public string name; // Nombre del coleccionable
        public int ID; // Numero asignado
        public Sprite icon; // Icono del coleccionable que se mostrara em el menu
        public string description; // Descripcion del objeto
        //public BaseItem item;
    }

    public InventoryItem[] dataBase;

    private void OnValidate() // Se llama cuando se carga el scriptable object y cuando este cambia en el Inspector
    {
        if(dataBase != null)
        {
            for(int i = 0;  i < dataBase.Length; i++)
            {
                if (dataBase[i].ID != i)
                {
                    dataBase[i].ID = i;
                }
            }
        }
    }
}
