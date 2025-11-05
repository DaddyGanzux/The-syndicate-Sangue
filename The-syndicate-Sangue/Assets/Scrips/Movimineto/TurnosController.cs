using UnityEngine;

public class TurnosController : MonoBehaviour
{
    public int turnoActual = 0; // 0 = jugador, 1 = enemigo
    public Player playerController;

    public void CambiarTurno()
    {
        turnoActual = (turnoActual + 1) % 2;
        Debug.Log("Turno cambiado a: " + (turnoActual == 0 ? "Jugador" : "Enemigo"));

    }
}
