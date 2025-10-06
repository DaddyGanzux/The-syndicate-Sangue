
using UnityEngine;
using UnityEngine.InputSystem;

// Define la clase principal para gestionar la lógica del tablero.
public class Suelo : MonoBehaviour
{
    // =================================================================================================================================================================================================================================
    //              CAMPOS LOGICOS
    // =================================================================================================================================================================================================================================

    private const int TILE_COUNT_X = 24; // Constantes privadas para el tamaño del suelo en x
    private const int TILE_COUNT_Y = 8; // Constantes privadas para el tamaño del suelo en y
    private GameObject[,] tiles; // Arreglo 2D para almacenar todos los GameObjects de las casillas

    private Vector2Int currentHover = -Vector2Int.one; // Almacena la posición de la casilla sobre la que el ratón está actualmente

    private Vector3 bounds; // Define los límites de la cuadrícula para centrarla correctamente

    // =================================================================================================================================================================================================================================
    //              CONFIG DE ARTE
    // =================================================================================================================================================================================================================================

    [Header("Art Stuff")]

    [SerializeField] private Material tileMaterial; // Material que se asignará a cada casilla generada. Debe usar URP

    [SerializeField] private float tileSize = 0.7f; // Tamaño de cada casilla (en unidades de Unity). El video usa 0.7
    [SerializeField] private float yOffset = 0.15f; // Desplazamiento en el eje Y para que las casillas floten ligeramente sobre el tablero de arte

    [SerializeField] private Vector3 boardCenter = Vector3.zero; // Centro del tablero (ajuste fino si el punto de pivote del modelo 3D no es 0,0,0)

    // =================================================================================================================================================================================================================================
    //              MÉTODOS MONOBEHAVIOUR
    // =================================================================================================================================================================================================================================

    private void Awake()  // Llamado al inicio, antes del primer frame. Usado para inicializar la cuadrícula
    {
        GenerateAllTiles(tileSize, TILE_COUNT_X, TILE_COUNT_Y);
    }

    private void Update() // Llamado una vez por frame. Usado para la lógica constante de interacción
    {

        // 1. Verificar la cámara principal
        if (Camera.main == null)
        {
            Debug.LogError("La etiqueta 'MainCamera' es necesaria para el Raycast.");
            return;
        }

        // 2. Raycasting (Lanzamiento de Rayo)
        RaycastHit info; // Almacena la información del impacto

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Crea un rayo desde la posición del ratón en la pantalla
        print(ray.direction);
        if (Physics.Raycast(ray, out info, 200, LayerMask.GetMask("Tile"))) // Lanza el rayo y verifica si impacta algo en la capa "Tile" dentro de una distancia de 100
        {
            // Rayo impactó una casilla (Tile)

            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject); // Obtiene los índices (X, Y) del tile impactado

            if (currentHover == -Vector2Int.one) // Si estamos cubriendo un cuadro despues de no cubrir ninguno
            {
                print("the hit position is :");
                print(hitPosition);
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if (currentHover != hitPosition) // Si ya estamos cubriendo un cuadro, cambiear el anterior
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

        }
        else
        {
            // Rayo NO impactó una casilla (el ratón salió del tablero)
            if (currentHover != -Vector2Int.one) // Resetear el estado cuando se sale del tablero
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile"); // Reset a la capa "Tile" para quitar el highlight

                currentHover = -Vector2Int.one; // Marca el estado como no "hovering"
            }
        }
    }

    // =================================================================================================================================================================================================================================
    //              MÉTODOS DE GENERACIÓN
    // =================================================================================================================================================================================================================================

    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY) // Función principal para generar todas las casillas del tablero
    {
        // 1. Ajustar el offset Y
        yOffset += transform.position.y; // Suma la posición Y del GameObject padre (transform) al offset inicial

        // 2. Calcular los límites (Bounds)
        float halfBoard = (float)tileCountX / 2 * tileSize; // Determina el centro del área generada para poder centrar las casillas, se calcula la mitad del la cuadricula y se multiplica por el tamaño del tile

        bounds = new Vector3(halfBoard, 0, halfBoard) + boardCenter; // Bounds final es la mitad del tablero más el ajuste fino del boardCenter

        // 3. Inicializar el arreglo
        tiles = new GameObject[tileCountX, tileCountY];

        // 4. Iterar y generar cada casilla
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                tiles[x, y] = GenerateSingleTile(tileSize, x, y); // Llama al método para crear una casilla individual
    }

    private GameObject GenerateSingleTile(float tileSize, int x, int y) // Función para crear un GameObject de casilla individual y su geometría
    {
        // 1. Crear GameObject vacío
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));

        tileObject.transform.parent = transform; // Asignar el tablero actual (this.transform) como padre

        // 2. Generación del Mesh (Geometría)
        Mesh mesh = new Mesh(); // Crea un nuevo objeto Mesh

        Vector3[] vertices = new Vector3[4]; // Define las 4 esquinas (vértices) del cuadrado, centradas en el GameObject

        //float xCoord = x * tileSize - bounds.x; // Cálculo de las coordenadas ajustadas por el límite (bounds) para centrar
        //float yCoord = y * tileSize - bounds.z; // Usamos .z porque el tablero está en el plano XZ.

        //vertices[0] = new Vector3(xCoord, yOffset, yCoord);
        //vertices[1] = new Vector3(xCoord, yOffset, yCoord + tileSize);
        //vertices[2] = new Vector3(xCoord + tileSize, yOffset, yCoord);
        //vertices[3] = new Vector3(xCoord + tileSize, yOffset, yCoord + tileSize);

        vertices[0] = new Vector3(x * tileSize, 0, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0, (y + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0, y * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0, (y + 1) * tileSize);

        int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; // Define los dos triángulos que componen el cuadrado, usando los índices de los vértices

        mesh.vertices = vertices; // Asigna los vértices al objeto Mesh
        mesh.triangles = triangles; // Asigna los  triángulos al objeto Mesh

        // 3. Asignar componentes de renderizado y colisión
        tileObject.AddComponent<MeshFilter>().mesh = mesh; // Añade y configura el MeshFilter (para la geometría)

        tileObject.AddComponent<MeshRenderer>().material = tileMaterial; // Añade el MeshRenderer (para el material e iluminación)

        tileObject.AddComponent<BoxCollider>();

        //BoxCollider boxCollider = tileObject.AddComponent<BoxCollider>(); // Añade el BoxCollider (necesario para el Raycast)

        //boxCollider.size = new Vector3(tileSize, 0.1f, tileSize); // Ajusta el tamaño del BoxCollider para coincidir con la casilla
        //boxCollider.center = new Vector3(tileSize / 2, 0, tileSize / 2); // Ajusta el centro del BoxCollider para coincidir con la casilla

        mesh.RecalculateNormals();  // Recalcula las normales para asegurar que la iluminación se aplique correctamente

        // 4. Asignar la capa "Tile"
        tileObject.layer = LayerMask.NameToLayer("Tile");

        return tileObject;
    }

    // =================================================================================================================================================================================================================================
    //              MÉTODOS DE UTILIDAD
    // =================================================================================================================================================================================================================================

    private Vector2Int LookupTileIndex(GameObject hitInfo) // Busca los índices (X, Y) en el arreglo 'tiles' basándose en el GameObject impactado
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
            for (int y = 0; y < TILE_COUNT_Y; y++)
                if (tiles[x, y] == hitInfo) // Compara la referencia del transform (más directo que el GameObject completo)
                    return new Vector2Int(x, y); // Devuelve los índices X, Y

        return -Vector2Int.one; // Equivale a (-1, -1)
    }
}