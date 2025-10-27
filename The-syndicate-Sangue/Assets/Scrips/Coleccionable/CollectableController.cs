using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario si usas una escena para el menú

public class CollectableController : MonoBehaviour
{
    // Patrón Singleton para acceso global
    public static CollectableController Instance { get; private set; }

    // Lista para guardar los coleccionables obtenidos (Inventario)
    private List<CollectableData> collectedItems = new List<CollectableData>();

    // Referencia al Panel de UI que muestra el coleccionable en grande
    public GameObject individualDisplayPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: para que persista entre escenas.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCollectable(CollectableItem item)
    {
        CollectableData newItem = new CollectableData(item.collectableID, item.displayName);

        // Solo se añade si no se ha recogido ya
        if (!collectedItems.Exists(c => c.id == newItem.id))
        {
            collectedItems.Add(newItem);
            Debug.Log("Coleccionable obtenido: " + newItem.name);

            // Mostrar la ventana individual.
            ShowIndividualDisplay(item.displayName, item.displayModelPrefab);

            // Opcional: Guardar el progreso (PlayerPrefs, JSON, etc.)
            // SaveGameData();
        }
    }

    // Muestra la ventana temporal al recoger el objeto
    private void ShowIndividualDisplay(string name, GameObject modelPrefab)
    {
        // Activar el panel de UI
        individualDisplayPanel.SetActive(true);

        // Aquí deberías tener lógica para:
        // 1. Mostrar el 'name' en un texto de UI.
        // 2. Instanciar el 'modelPrefab' dentro del panel para mostrarlo.
        // 3. Poner un temporizador o esperar un input para cerrar el panel.

        // (La implementación detallada de la UI depende del motor y es extensa).
    }

    // EJEMPLO: Abrir el menú de coleccionables
    public void OpenCollectablesMenu()
    {
        // Opción 1: Abrir un panel de UI en la escena actual
        // collectablesMenuPanel.SetActive(true);

        // Opción 2: Cargar una escena dedicada (Asegúrate de que está en Build Settings)
        // SceneManager.LoadScene("CollectablesMenuScene");
    }

    // Método para ser usado por el Menú de Coleccionables
    public List<CollectableData> GetCollectedItems()
    {
        return collectedItems;
    }
}