using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public int ataqueBase = 10;
    public float health = 100f;
    public float chanceAtaqueExitoso = 0;
    public float ReducirChanceAtaqueExitoso = 0;

    public Enemy2AI enemyAI;
    public float probabilidadTiro = 1f;
    public TurnosController turnos; // referencia al controlador de turnos
    public Player playerController;



    public void atacar()
    {
        // solo puede atacar si es su turno
        if (turnos.turnoActual != 0) return;

        chanceAtaqueExitoso -= ReducirChanceAtaqueExitoso;

        if (probabilidadTiro >= chanceAtaqueExitoso)
        {
            Debug.Log("Ataque Exitoso");
            enemyAI.health -= ataqueBase;
        }
        else
        {
            Debug.Log("Ataque Fallido");
        }

        // TERMINA SU TURNO
        turnos.CambiarTurno();
        playerController.FinalizarTurnoJugador();
    }

    public void Ramdomizar()
    {
        ReducirChanceAtaqueExitoso = Random.Range(0f, 0.5f);
    }


}
