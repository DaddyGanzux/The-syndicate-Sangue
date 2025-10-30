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
        //Chance de ser atacado






        //Movimiento de la IA
        if (turnosController.turnoActual == 1)// Verifica si es el turno del enemigo
        {
            if (numPasos > 0)
            {
                // Si hay un objetivo asignado, sigue al objetivo
                if (target != null)
                {
                    numPasos -= Time.deltaTime;
                    //Debug.Log("Pasos restantes del enemigo: " + numPasos);


                    navMeshAgent.SetDestination(target.position);
                }
            }
            else
            {
                navMeshAgent.SetDestination(gameObject.transform.position);
            }

        }

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
