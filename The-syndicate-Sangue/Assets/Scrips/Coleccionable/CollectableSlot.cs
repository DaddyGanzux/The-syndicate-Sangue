using UnityEngine;
using UnityEngine.UI;

public class CollectableSlot : MonoBehaviour
{
    public Text nameText;
    public Button selectButton;
    [HideInInspector] public CollectableData data;
    [HideInInspector] public CollectablesMenuManager manager;

    // Inicializa el slot
    public void Setup(CollectableData itemData, CollectablesMenuManager menuManager)
    {
        data = itemData;
        manager = menuManager;
        nameText.text = itemData.name;
        // Aquí puedes cargar la imagen de icono si la tienes:
        // GetComponent<Image>().sprite = ...

        // Asigna el evento de clic
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnItemSelected);
    }

    // Se llama cuando el usuario hace clic en este slot
    private void OnItemSelected()
    {
        // Llama al manager para que muestre la vista detallada del ítem
        manager.DisplayDetailedView(data);
    }
}