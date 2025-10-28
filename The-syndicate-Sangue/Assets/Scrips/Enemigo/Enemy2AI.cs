using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2AI : MonoBehaviour
{
    public Transform target; // El objetivo que el enemigo debe seguir
    public Transform[] patrolPoints; // Puntos dentro de la zona marcada
    public float movementSpeed = 3.0f; // Velocidad de movimiento del enemigo
    private int currentPatrolIndex = 0;
    public float numPasos = 3;
    gridController grid;
    public TurnosController turnosController;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    void Update()
    {
        if (turnosController.turnoActual == 1)// Verifica si es el turno del enemigo
        {
            if (numPasos > 0)
            {
                // Si hay un objetivo asignado, sigue al objetivo
                if (target != null)
                {
                    numPasos -= Time.deltaTime;
                    Debug.Log("Pasos restantes del enemigo: " + numPasos);


                    navMeshAgent.SetDestination(target.position);
                }
            }
            else
            {
                navMeshAgent.SetDestination(gameObject.transform.position);
            }

        }

    }

}
