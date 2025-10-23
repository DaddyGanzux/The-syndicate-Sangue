using UnityEngine;

public class RaycastSwitchColor : MonoBehaviour
{
    public float maxDistance = 5f; // Distancia máxima del raycast
    private RaycastHit hit;        // guarda la información del objeto impactodo

    private void FixedUpdate()
    {
        // Dibuja un rayo rojo hacia abajo para depuración
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);

        // Lanza el raycast hacia abajo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance))
        {
            // Muestra en consola el nombre del objeto tocado
            Debug.Log("Toca suelo con: " + hit.collider.name);

            // Unitiy busca el componente gridController en el objeto impactado
            gridController grid = hit.collider.GetComponent<gridController>();//Si lo hace se gusrada en grid y es diferente de null

            // Si lo encuentra, cambia su color
            if (grid != null)
            {
                grid.CambiarColorVisitado(); // Llama al método del grid para cambiar color
            }
        }
    }
}
