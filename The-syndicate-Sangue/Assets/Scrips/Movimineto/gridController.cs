using UnityEngine;

public class gridController : MonoBehaviour
{
    public Material gridMaterialStart; // Material inicial
    public Material gridMaterialEnd;   // Material cuando es visitado

    private Renderer rend;             // Renderer del objeto
    private bool fueVisitado = false;  // Si la casilla ya fue visitada

    private void Start()
    {
        // Obtiene el Renderer al iniciar
        rend = GetComponent<Renderer>();
        // Asegura que empiece con el material inicial
        rend.material = gridMaterialStart;
    }

    // Método público para cambiar el color cuando el raycast la toca
    public void CambiarColorVisitado()
    {
        if (!fueVisitado) // Solo cambia una vez
        {
            rend.material = gridMaterialEnd; // Cambia al material de visitado
            fueVisitado = true;// Marca como visitado
        }
    }
}
