using UnityEngine;

public class TurnosController : MonoBehaviour
{
    public bool turnoActual = true; // 0 para jugador, 1 para enemigo


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnoActual = true; // Inicia con el turno del jugador
    }

    // Update is called once per frame
    void Update()
    {
        quienJuega();
    }

    public void quienJuega()
    {
        if (turnoActual == true)
        {
            Debug.Log("Turno Mafiosos");
            // Lógica para el turno del jugador
            turnoActual = true; // Cambia al turno del enemigo
        }
        else if (turnoActual == false)
        {
            Debug.Log("Turno Enemigos");
            // Lógica para el turno del enemigo
            turnoActual = false; // Cambia al turno del jugador
        }
    }

    public void CambiarTurno()
    {
        turnoActual = !turnoActual; // Cambia el turno
    }
}
