using UnityEngine;

public class gridController : MonoBehaviour
{
    [Header("Materiales de la casilla")]
    public Material gridMaterialStart; // Material inicial (color base)
    public Material gridMaterialEnd;   // Material cuando es visitado

    private Renderer rend;             // Componente Renderer del objeto
    private bool fueVisitado = false;  // Indica si la casilla ya fue visitada
    public int contadorPasosEnemigo = 1; // (opcional, puedes usarlo después para IA o conteo de pasos)

    private void Start()
    {
        // Obtiene el componente Renderer al iniciar
        rend = GetComponent<Renderer>();

        // Asegura que el material comience con el color original
        if (gridMaterialStart != null)//La condicion es para evitar errores en caso de que no se haya asignado el material
            rend.material = gridMaterialStart;// Establece el material inicial
    }

    // 🔹 Cambia el color al material de visitado
    public void CambiarColorVisitado()
    {
        if (!fueVisitado && gridMaterialEnd != null)//La condicion es cuando no ha sido visitada y el material de destino no es nulo
        {
            rend.material = gridMaterialEnd; // Cambia el color
            fueVisitado = true; // Marca que fue visitada
            Debug.Log("Casilla visitada. Contador de pasos del enemigo: " + contadorPasosEnemigo);
        }
    }

    // 🔹 Restaura el color original cuando el jugador ya no está encima
    public void Resart()
    {
        if (gridMaterialStart != null)//Cuando el material inicial no es nulo
        {
            rend.material = gridMaterialStart;
            fueVisitado = false; // Permite volver a cambiar más adelante si se pisa otra vez
        }
    }
}
