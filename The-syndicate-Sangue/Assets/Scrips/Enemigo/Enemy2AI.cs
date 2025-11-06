using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy2AI : MonoBehaviour
{

    //Conf movimiento IA
    public Transform target; // El objetivo que el enemigo debe seguir
    public Transform[] patrolPoints; // Puntos dentro de la zona marcada
    public float movementSpeed = 3.0f; // Velocidad de movimiento del enemigo
    public float numPasos = 3;// Número de pasos que el enemigo puede dar en su turno
    gridController grid;

    //llamar referencias
    public TurnosController turnosController;
    public AtaquePlayer playerStats;
    public AtaquePlayer ataquePlayer;

    //NavMeshAgent para movimiento IA
    private NavMeshAgent navMeshAgent;
    

    //Conf stats
    public float health = 100f;


    void Start()
    { 

        //movimiento IA
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    void Update()
    {

        if (turnosController.turnoActual == 1)
        {

            // arranca solo si NO hay una coroutine ya en proceso
            if (!estaEjecutandoTurno)//Verifica si la bandera es diferente de true
            {
                estaEjecutandoTurno = true;// pone la bandera para evitar múltiples corrutinas
                StartCoroutine(TurnoEnemigo());
            }
        }
        else
        {
            estaEjecutandoTurno = false;// resetea la bandera cuando no es su turno
        }
    }

    bool estaEjecutandoTurno = false;// bandera para evitar múltiples corrutinas



    public IEnumerator TurnoEnemigo()
    {
        Debug.Log("Enemigo EMPIEZA su turno");

        ResetNumPasos();

        while (numPasos > 0)
        {
            if (target != null)
                navMeshAgent.SetDestination(target.position);

            numPasos -= Time.deltaTime;
            yield return null;
        }

        DetenerMovinientoEnemigo();

        //Logica de ataque
        yield return new WaitForSeconds(1);//yield retorna el control despues de esperar 1 segundo

        playerStats.health -= 10f;
        Debug.Log("Enemigo ataca al jugador, vida jugador: " + playerStats.health);
        playerStats.puedeAtacar = true;

        FinalizarTurnoEnemigo();
    }



    public void ResetNumPasos()
    {
        numPasos = 3;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoberturaAlta"))
        {

        ataquePlayer.coberturaPenalizacion = 0.50f;
        Debug.Log("Cobertura Alta");
        }
        else if (other.CompareTag("CoberturaBaja"))
        {
            ataquePlayer.coberturaPenalizacion = 0.25f;
            Debug.Log("Cobertura Baja");
        }
                
    }


    public void OnTriggerExit(Collider other)
    {   
        if (other.CompareTag("CoberturaAlta") || other.CompareTag("CoberturaBaja"))
            ataquePlayer.coberturaPenalizacion = 0;
        Debug.Log("Fuera de Cobertura");
    }

    void DetenerMovinientoEnemigo()
    {
        // deja de moverse
        navMeshAgent.SetDestination(transform.position);
        Debug.Log("Enemigo movimiento TERMINADO");

    }

    public void FinalizarTurnoEnemigo()
    {
        turnosController.CambiarTurno();
    }
}
