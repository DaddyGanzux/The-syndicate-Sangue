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
        DibujarRaycastCobertura();
        // SOLO AQUÍ verificar cobertura UNA vez
        DectectarCobertura();
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

        // deja de moverse
        navMeshAgent.SetDestination(transform.position);
        Debug.Log("Enemigo movimiento TERMINADO");


        yield return new WaitForSeconds(1);

        playerStats.health -= 10f;
        Debug.Log("Enemigo ataca al jugador, vida jugador: " + playerStats.health);

        turnosController.CambiarTurno();
        playerStats.puedeAtacar = true;
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
        if (hitInfo1.collider != null && hitInfo1.collider.CompareTag("CoberturaAlta"))
        {
            playerStats.ReducirChanceAtaqueExitoso = .25f;
            Debug.Log("CoberturaAlta detectada al frente");
        }
        else if(hitInfo1.collider != null && hitInfo1.collider.CompareTag("CoberturaBaja"))//hitinfo 1
        {
            playerStats.ReducirChanceAtaqueExitoso = .10f;
            Debug.Log("CoberturaBaja detectada al frente");
        }
        else if (hitInfo2.collider != null && hitInfo2.collider.CompareTag("CoberturaAlta"))
        {
            playerStats.ReducirChanceAtaqueExitoso = .25f;
            Debug.Log("CoberturaAlta detectada atrás");
        }
        else if (hitInfo2.collider != null && hitInfo2.collider.CompareTag("CoberturaBaja"))//hitinfo2
        {
            playerStats.ReducirChanceAtaqueExitoso = .10f;
            Debug.Log("CoberturaBaja detectada atrás");
        }
        else if (hitInfo3.collider != null && hitInfo3.collider.CompareTag("CoberturaAlta"))
        {
            playerStats.ReducirChanceAtaqueExitoso = .25f;
            Debug.Log("CoberturaAlta detectada a la derecha");
        }
        else if (hitInfo3.collider != null && hitInfo3.collider.CompareTag("CoberturaBaja"))//hitinfo3
        {
            playerStats.ReducirChanceAtaqueExitoso = .10f;
            Debug.Log("CoberturaBaja detectada a la derecha");
        }
        else if (hitInfo4.collider != null && hitInfo4.collider.CompareTag("CoberturaAlta"))
        {
            playerStats.ReducirChanceAtaqueExitoso = .25f;
            Debug.Log("CoberturaAlta detectada a la izquierda");
        }
        else if (hitInfo4.collider != null && hitInfo4.collider.CompareTag("CoberturaBaja"))//hitinfo4
        {
            playerStats.ReducirChanceAtaqueExitoso = 0.10f;
            Debug.Log("CoberturaBaja detectada a la izquierda");
        }
        else
        {
            playerStats.ReducirChanceAtaqueExitoso = 0;
            Debug.Log("No hay cobertura detectada");
        }
    }

    public void DibujarRaycastCobertura()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 3, Color.red);// Dibuja el raycast en la escena para visualización
        Debug.DrawRay(transform.position, Vector3.back * 3, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * 3, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * 3, Color.red);
    }

}
