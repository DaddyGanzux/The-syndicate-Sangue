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
    public float numPasos = 3;
    gridController grid;
    public TurnosController turnosController;
    public AtaquePlayer playerStats;

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
        DectectarCobertura();
        DibujarRaycastCobertura();


        if (turnosController.turnoActual == 1)
        {
            // arranca solo si NO hay una coroutine ya en proceso
            if (!estaEjecutandoTurno)
            {
                estaEjecutandoTurno = true;
                StartCoroutine(TurnoEnemigo());
            }
        }
        else
        {
            estaEjecutandoTurno = false;
        }
    }

    bool estaEjecutandoTurno = false;

    public IEnumerator TurnoEnemigo()
    {
        Debug.Log("Enemigo EMPIEZA su turno");

        // reinicia pasos
        ResetNumPasos();

        // mover enemigo hasta gastar pasos
        while (numPasos > 0)
        {
            if (target != null)
                navMeshAgent.SetDestination(target.position);

            numPasos -= Time.deltaTime;
            yield return null;
        }

        // deja de moverse
        navMeshAgent.SetDestination(transform.position);
        Debug.Log("Enemigo movimiento TERMINADO");

        // espera un momento antes de atacar
        yield return new WaitForSeconds(2);

        // ataque al jugador
        Debug.Log("Enemigo ATACA al jugador!");
        playerStats.health -= 10f; //<--- aquí restas vida al player

        // TERMINA el turno
        turnosController.CambiarTurno();
    }


    public void ResetNumPasos()
    {
        numPasos = 3;
    }

    public void DectectarCobertura()
    {
        Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitInfo1);
        Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitInfo2);
        Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitInfo3);
        Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitInfo4);
    }

    public void DibujarRaycastCobertura()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);// Dibuja el raycast en la escena para visualización
        Debug.DrawRay(transform.position, Vector3.back * 10, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * 10, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * 10, Color.red);
    }

}
