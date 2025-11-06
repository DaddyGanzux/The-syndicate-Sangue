using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public float ataqueBase = 0;
    public float health = 100f;

    public Enemy2AI enemyAI;
    public float probabilidadTiro = 1f;
    public TurnosController turnos; // referencia al controlador de turnos
    public Player playerController;

    public bool puedeAtacar = true;

    public float probabilidadBase = 0.65f;   // 65%
    public float coberturaPenalizacion = 0f;        // -0.25 o -0.50


    public void atacar()
    {
        RandomAtaque();

        // “dado”
        float randomShot = Random.value; // 0 - 1.
        //Se uso random.value porque genera un valor float entre 0.0 y 1.0
        // calcular la probabilidad final

        float chanceFinal = probabilidadBase - coberturaPenalizacion;

        // solo puede atacar si es su turno
        if (turnos.turnoActual != 0) return;

        if (puedeAtacar == true)
        {
            Debug.Log("El jugador puede atacar \n" + " Si,Random Shot = " + randomShot * 100f + 
                "%. es menor o igual a " + "Chance Final = " + chanceFinal * 100f + "% \n +" +
                "El jugador puede atacar");

            Debug.Log("|---- HIT ----|----------------------------- MISS ------------------------|\r\n" +
                      "                 0.0              " + chanceFinal + "                      1.0");

            //randoShot es menor o igual a chanceFinal porque asi se considera un acierto
            if (randomShot <= chanceFinal)
            {
                Debug.Log("Hit!");
                enemyAI.health -= ataqueBase;
            }
            else
            {
                Debug.Log("Miss!");
            }
        }
        else
        {
            Debug.Log("El jugador NO puede atacar");
        }

        puedeAtacar = false;

    }

    void RandomAtaque()
    {
       ataqueBase = Random.Range(20f, 50f);
    }

}
