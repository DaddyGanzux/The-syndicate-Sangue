using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    // Identificador único para este coleccionable.
    public string collectableID;
    // Opcional: Nombre del coleccionable para mostrar.
    public string displayName;
    // Opcional: Modelo 3D a mostrar en el menú (si es diferente al del mundo).
    public GameObject displayModelPrefab;

    private bool isPlayerNearby = false;

    // Se llama cuando el jugador entra en el área de colisión (trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Pulsa E para recoger " + displayName);
            // Mostrar un prompt de UI (ej: "Pulsa E")
        }
    }

    // Se llama cuando el jugador sale del área de colisión
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // Ocultar el prompt de UI
        }
    }

    // Lógica para detectar la pulsación de la tecla 'E'
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void Collect()
    {
        // 1. Notificar al controlador.
        if (CollectableController.Instance != null)
        {
            CollectableController.Instance.AddCollectable(this);
        }

        // 2. Destruir el objeto del mundo.
        Destroy(gameObject);
    }
}