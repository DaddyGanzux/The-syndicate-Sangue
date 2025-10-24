using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject prefabDelimitation;
    public GameObject delimitacionVisual;
    public Transform player;
    public Rigidbody rb;
    [SerializeField] private float x = 2;
    [SerializeField] private float z = 2;
    public float moveSpeed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guardamos posición inicial del jugador multiplicada por 2 (según tu lógica)
        x = player.position.x * 1;
        z = player.position.z * 1;

        // Crear el objeto delimitador
        Delimitation();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Delimitation();
            //Debug.Log("Delimitación creada al presionar espacio.");
        }

    }

    public void Move()
    {
        // Vector de movimiento según teclas WASD o flechas
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(Input.GetAxis("Horizontal") != 0)
        {
            //Debug.Log("Movimiento detectado: " + movement);
        }
        if(Input.GetAxis("Vertical") != 0)
        {
           //Debug.Log("Movimiento detectado: " + movement);
        }


        // Aplicar velocidad al rigidbody
        rb.linearVelocity = movement * moveSpeed;
    }

    public void Delimitation()
    {
        // Instancia el prefab en la posición deseada, con la rotación especificada
        Instantiate(delimitacionVisual, new Vector3(player.position.x + -5, 0.1f, z), Quaternion.Euler(0f, -90f, 0f));//Bloque para ver el limite derecha
        Instantiate(delimitacionVisual, new Vector3(player.position.x + 5, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));//Bloque para ver el limite izquierda
        Instantiate(delimitacionVisual, new Vector3(x, 0.1f, player.position.z + 5f), Quaternion.Euler(0f, 0f, 0f));//Bloque para ver el limite
        Instantiate(delimitacionVisual, new Vector3(x, 0.1f, player.position.z - 5f), Quaternion.Euler(0f, 0f, 0f));//Bloque para ver el limite

        //Debug.Log("Delimitación creada en: " + x + ", " + z);
    }

    

}
