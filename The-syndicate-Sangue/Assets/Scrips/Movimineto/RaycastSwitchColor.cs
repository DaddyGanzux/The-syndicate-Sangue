using UnityEngine;

public class RaycastSwitchColor : MonoBehaviour
{
    public float maxDistance = 5f; // Distancia máxima del raycast
    private RaycastHit hit;        // Guarda la información del objeto impactado
    private gridController lastGrid; // Última casilla que fue tocada por el raycast

    private void FixedUpdate()
    {
        // Dibuja un rayo rojo hacia abajo para depuración
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);

        // Lanza el raycast hacia abajo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance))
        {
            // Intenta obtener el componente gridController del objeto tocado
            gridController currentGrid = hit.collider.GetComponent<gridController>();

            // Si golpea una nueva casilla diferente a la anterior
            if (currentGrid != null && currentGrid != lastGrid)
            {
                // Si había una casilla anterior, la restauramos
                if (lastGrid != null)
                    lastGrid.Resart();

                // Cambiamos el color de la nueva casilla
                currentGrid.CambiarColorVisitado();

                // Actualizamos la referencia
                lastGrid = currentGrid;
            }
        }
        else
        {
            // Si el raycast no toca nada y había una casilla previa, la restauramos
            if (lastGrid != null)
            {
                lastGrid.Resart();
                lastGrid = null;
            }
        }
    }
}
