using UnityEngine;

public class TurnosController : MonoBehaviour
{
    public int turnoActual = 0; // 0 = jugador, 1 = enemigo
    public Player playerController;
    //public CentrarObjetos centrar;


    public void CambiarTurno()
    {
        turnoActual = (turnoActual + 1) % 2;//(turnoActual + 1) % 2; el más 1 hace que cambie de 0 a 1 y viceversa,
                                            //el %2 hace que vuelva a 0 cuando llega a 2
        Debug.Log("Turno cambiado a: " + (turnoActual == 0 ? "Jugador" : "Enemigo"));

        playerController.ActualizarDelimitaciones();
        // centrar.Centrar();
    }
}
