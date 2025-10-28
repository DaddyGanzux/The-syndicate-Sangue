using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject delimitacionVisual; // Prefab del bloque delimitador
    public Transform player;              // Transform del jugador
    public Rigidbody rb;
    public float moveSpeed = 5f;

    public TurnosController turnosController;

    // Referencias a las delimitaciones ya instanciadas
    private GameObject limiteIzq;
    private GameObject limiteDer;
    private GameObject limiteSup;
    private GameObject limiteInf;

    private float x, z;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        x = player.position.x;
        z = player.position.z;

        // Crear delimitaciones una sola vez
        CrearDelimitaciones();
    }

    void Update()
    {
        // Solo permitir movimiento si es el turno del jugador
        if (turnosController.turnoActual == 0)
        {
            rb.constraints = RigidbodyConstraints.None; // Descongelar el Rigidbody para permitir movimiento
            Move();

            // Al presionar espacio: cambiar de turno y mover las delimitaciones
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = Vector3.zero; // Detener el movimiento al cambiar de turno
                rb.constraints = RigidbodyConstraints.FreezeAll; // Congelar el Rigidbody
                ActualizarDelimitaciones();
                turnosController.CambiarTurno(); // Cambiar turno al enemigo
            }
        }
    }

    public void Move()
    {
        // Movimiento con WASD o flechas
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.linearVelocity = movement * moveSpeed;
    }

    // 🔹 Crea los objetos de delimitación solo una vez
    void CrearDelimitaciones()
    {
        limiteIzq = Instantiate(delimitacionVisual, new Vector3(player.position.x - 5, 0.1f, z), Quaternion.Euler(0, -90, 0));
        limiteDer = Instantiate(delimitacionVisual, new Vector3(player.position.x + 5, 0.1f, z), Quaternion.Euler(0, 90, 0));
        limiteSup = Instantiate(delimitacionVisual, new Vector3(x, 0.1f, player.position.z + 5f), Quaternion.identity);
        limiteInf = Instantiate(delimitacionVisual, new Vector3(x, 0.1f, player.position.z - 5f), Quaternion.identity);
    }

    // 🔹 En lugar de crear nuevos, mueve los existentes
    void ActualizarDelimitaciones()
    {
        if (limiteIzq == null || limiteDer == null || limiteSup == null || limiteInf == null)
        {
            // Si por alguna razón se destruyeron, las volvemos a crear
            CrearDelimitaciones();
            return;
        }

        // Actualiza las posiciones según la nueva ubicación del jugador
        x = player.position.x;
        z = player.position.z;

        limiteIzq.transform.position = new Vector3(x - 5, 0.1f, z);
        limiteDer.transform.position = new Vector3(x + 5, 0.1f, z);
        limiteSup.transform.position = new Vector3(x, 0.1f, z + 5f);
        limiteInf.transform.position = new Vector3(x, 0.1f, z - 5f);

        Debug.Log("Delimitaciones actualizadas a nueva posición.");
    }
}
