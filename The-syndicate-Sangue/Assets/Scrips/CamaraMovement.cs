using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // La variable 'public' permite ajustar la velocidad en el Inspector de Unity.
    public float velocidadMovimiento = 10f; // 1. Velocidad de Movimiento

    void Update() // 2. El método Update() se llama una vez por fotograma.
    {

        // 3. Captura de la Entrada del Teclado (Input)
        float inputX = Input.GetAxis("Horizontal"); // Input.GetAxis("Horizontal") devuelve: -1.0 si se pulsa 'A', 1.0 si se pulsa 'D' o 0.0 si no se pulsa ninguna o ambas.

        float inputZ = Input.GetAxis("Vertical"); // Input.GetAxis("Vertical") devuelve: 1.0 si se pulsa 'W' -1.0 si se pulsa 'S'

        // 4. Cálculo del Vector de Movimiento (Dirección)
        Vector3 movimiento = new Vector3(inputX, 0f, inputZ); // Creamos un vector de 3 dimensiones, el movimiento se asigna a X y Z, dejamos Y en 0f para evitar mover la cámara verticalmente (arriba/abajo) con 'W' y 'S'

        movimiento = Vector3.ClampMagnitude(movimiento, 1f); // Esto asegura que la velocidad nunca supere 1.0. Es crucial para que moverse en diagonal no sea más rápido que moverse en línea recta

        // 6. Aplicación del Movimiento
        transform.Translate(movimiento * velocidadMovimiento * Time.deltaTime, Space.Self); // transform.Translate() mueve el objeto desde su posición actual
    }
}