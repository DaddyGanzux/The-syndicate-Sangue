using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public int ataqueBase = 10;
    public float health = 100f;
    public float chanceAtaqueExitoso = 1f;
    public float ReducirChanceAtaqueExitoso = 0f;

    public Enemy2AI enemyAI;
    public float probabilidadTiro = 1f;
    public TurnosController turnos; // referencia al controlador de turnos
    public Player playerController;
    public bool puedeAtacar = true;


    public void atacar()
    {
        // solo puede atacar si es su turno
        if (turnos.turnoActual != 0) return;

        probabilidadTiro -= ReducirChanceAtaqueExitoso;
        Debug.Log("Probabilidad de Tiro: " + probabilidadTiro + " - Reducir chance de ataque: " + ReducirChanceAtaqueExitoso);
        Debug.Log("Chance de Ataque Exitoso: " + chanceAtaqueExitoso);

        if (puedeAtacar == true)
        {
            Debug.Log("El jugador puede atacar");

            if (probabilidadTiro >= chanceAtaqueExitoso)
            {
                Debug.Log("Ataque Exitoso");
                enemyAI.health -= ataqueBase;
                Debug.Log("Vida Enemigo: " + enemyAI.health);
                puedeAtacar = false;
            }
            else
            {
                Debug.Log("Ataque Fallido");
                puedeAtacar = false;
            }
        }
        else  
        {
            Debug.Log("El jugador NO puede atacar");
        }

    }

    public void Ramdomizar()
    {
        ReducirChanceAtaqueExitoso = Random.Range(0f, 0.5f);
    }


}
