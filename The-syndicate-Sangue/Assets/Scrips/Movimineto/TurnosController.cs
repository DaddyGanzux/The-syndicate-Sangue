using UnityEngine;
using UnityEngine.UI;

public class TurnosController : MonoBehaviour
{
    public int turnoActual = 0; // 0 para jugador, 1 para enemigo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnoActual = 0; // Inicia con el turno del jugador
    }

    // Update is called once per frame
    void Update()
    {
        quienJuega();
    }

    public void quienJuega()
    {
        if (turnoActual == 0)
        {
            Debug.Log("Turno Mafiosos");
            // Lógica para el turno del jugador
            turnoActual = 0; // Cambia al turno del enemigo
        }
        else if (turnoActual == 1)
        {
            Debug.Log("Turno Enemigos");
            // Lógica para el turno del enemigo
            turnoActual = 1; // Cambia al turno del jugador
        }
    }

    public void CambiarTurno()
    {
        turnoActual = (turnoActual + 1) % 2; // Alterna entre 0 y 1
        Debug.Log("Turno cambiado a: " + (turnoActual == 0 ? "Jugador" : "Enemigo"));// este es un if ternario
        //Ese if recibe una condicion, si es verdadera devuelve el primer valor, si es falsa devuelve el segundo valor.
    }

   
}
