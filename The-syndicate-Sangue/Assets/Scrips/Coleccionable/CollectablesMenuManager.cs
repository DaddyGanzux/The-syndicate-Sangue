using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectablesMenuManager : MonoBehaviour
{
    // Vincula estos objetos en el Inspector de Unity
    [Header("UI References")]
    public GameObject contentParent;         // ContentPanel (el que tiene el Grid Layout Group)
    public GameObject slotPrefab;            // ¡El PREFAB del slot que acabas de crear en el Paso 5!
    public GameObject detailedViewPanel;     // DetailedViewPanel (para mostrar el item en grande)
    public Text detailedNameText;
    public Transform detailedModelParent;    // DetailedModelParent (el punto de instanciación 3D)

    private List<GameObject> activeSlots = new List<GameObject>();
    private CollectableController controller; // Referencia al controlador principal

    void Awake()
    {
        // Obtener la referencia al controlador principal
        controller = CollectableController.Instance;
        if (controller == null)
        {
            Debug.LogError("CollectableController no encontrado. Asegúrate de que existe en la escena.");
        }
    }

    // Se llama cada vez que el menú se activa
    void OnEnable()
    {
        LoadCollectedItems();
    }

    // Función principal: genera los slots de la lista
    public void LoadCollectedItems()
    {
        // 1. Limpiar slots anteriores
        foreach (GameObject slot in activeSlots)
        {
            Destroy(slot);
        }
        activeSlots.Clear();

        // Desactivar la vista detallada al recargar la lista
        detailedViewPanel.SetActive(false);

        // 2. Obtener la lista de datos
        List<CollectableData> items = controller.GetCollectedItems();

        // 3. Instanciar los slots
        foreach (CollectableData item in items)
        {
            // Instancia el prefab del slot como hijo del ContentPanel
            GameObject slotGO = Instantiate(slotPrefab, contentParent.transform);
            CollectableSlot slotScript = slotGO.GetComponent<CollectableSlot>();

            // Configura el slot con los datos del ítem
            slotScript.Setup(item, this);
            activeSlots.Add(slotGO);
        }
    }

    // 4. Mostrar la vista detallada (llamado por CollectableSlot.cs)
    public void DisplayDetailedView(CollectableData data)
    {
        // Limpiar el modelo 3D anterior (destruye cualquier modelo que quede)
        foreach (Transform child in detailedModelParent)
        {
            Destroy(child.gameObject);
        }

        detailedNameText.text = data.name;

        // **PASO CRUCIAL PARA EL 3D**
        // Obtener la referencia al Prefab 3D
        // NOTA: Debes tener una forma de cargar el prefab, por ejemplo, usando Resources.Load
        // o un sistema de base de datos de coleccionables.
        GameObject modelPrefab = Resources.Load<GameObject>("CollectableModels/" + data.id);

        if (modelPrefab != null)
        {
            // 5. Instanciar el modelo 3D
            GameObject newModel = Instantiate(modelPrefab, detailedModelParent);
            newModel.transform.localPosition = Vector3.zero; // Posicionar en el centro del 'DetailedModelParent'
            newModel.transform.localRotation = Quaternion.identity;

            // Opcional: añadir un script de "rotación automática" al newModel
            // newModel.AddComponent<Rotator>();
        }

        detailedViewPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false); // Oculta el menú
        // Lógica para reanudar el juego (ej: Time.timeScale = 1f)
    }
}