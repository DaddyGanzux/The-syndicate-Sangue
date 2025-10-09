
// ChessPiceType = CharacterMovement
// ChessPieces = Personajes
// ChessPiece = Personaje
// whiteTeam = CharacterTeam
// blackTeam = EnemyTeam

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Define la clase principal para gestionar la logica del tablero
public class Suelo : MonoBehaviour
{
    // =================================================================================================================================================================================================================================
    //              CAMPOS LOGICOS
    // =================================================================================================================================================================================================================================

    private Personaje[,] Personajes; // Arreglo 2D para almacenar las referencias a los scripts de movimiento de los personajes
    private Personaje currentlyDragging;
    private const int TILE_COUNT_X = 24; // Constantes privadas para el tamano del suelo en X
    private const int TILE_COUNT_Y = 9;  // Constantes privadas para el tamano del suelo en Y
    private GameObject[,] tiles;        // Arreglo 2D para almacenar todos los GameObjects de las casillas

    private Vector2Int currentHover = -Vector2Int.one; // Almacena la posicion de la casilla sobre la que el raton esta actualmente

    private Vector3 bounds; // Define los limites de la cuadricula para centrarla correctamente en el mundo

    // =================================================================================================================================================================================================================================
    //              CONFIG DE ARTE
    // =================================================================================================================================================================================================================================

    [Header("Art Stuff")]

    [SerializeField] private Material tileMaterial; // Material que se asignara a cada casilla generada
    [SerializeField] private float tileSize = 2f;  // Tamano de cada casilla
    [SerializeField] private float yOffset = 0.15f;  // Desplazamiento en el eje Y para que las casillas floten

    [SerializeField] private Vector3 boardCenter = Vector3.zero; // Centro del tablero (ajuste fino si el punto de pivote del modelo 3D no es 000)

    // =================================================================================================================================================================================================================================
    //              PREFABS Y MATERIALES
    // =================================================================================================================================================================================================================================

    [Header("Prefabs & Material")]
    [SerializeField] private GameObject[] prefabs;   // Arreglo de prefabs de personajes
    [SerializeField] private Material[] teamMaterials; // Materiales para los equipos de personajes


    // =================================================================================================================================================================================================================================
    //              METODOS MONOBEHAVIOUR
    // =================================================================================================================================================================================================================================

    private void Awake()  // Llamado al inicio antes del primer frame
    {
        GenerateAllTiles(tileSize, TILE_COUNT_X, TILE_COUNT_Y); // Genera toda la geometria de las casillas y calcula los limites centrados
        SpawnAllCharacters(); // Spawnea todos los personajes en el arreglo Personajes
        PositionAllCharacters(); // Coloca todos los personajes en sus posiciones iniciales centradas
    }

    private void Update() // Llamado una vez por frame para la logica constante de interaccion
    {
        // 1. Verificar la camara principal (debe tener el Tag MainCamera)
        if (Camera.main == null)
        {
            Debug.LogError("La etiqueta 'MainCamera' es necesaria para el Raycast");
            return;
        }

        // 2. Raycasting (Lanzamiento de Rayo)
        RaycastHit info; // Almacena la informacion del impacto
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Crea un rayo desde la posicion del raton

        if (Physics.Raycast(ray, out info, 200, LayerMask.GetMask("Tile"))) // Lanza el rayo y verifica si impacta algo en la capa "Tile"
        {
            // Rayo impacto una casilla
            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject); // Obtiene los indices del tile impactado

            if (currentHover == -Vector2Int.one) // Si no habia hover antes
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if (currentHover != hitPosition) // Si cambiamos de casilla
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile"); // Resetea la casilla anterior a la capa "Tile"

                // Establece el nuevo hover en la capa "Hover"
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if(Input.GetMouseButtonDown(0)) // Si precionamos el boton del mouse
            {
                if (Personajes[hitPosition.x, hitPosition.y] != null)
                {
                    if (true)
                    {
                        currentlyDragging = Personajes[hitPosition.x, hitPosition.y];
                    }
                }
            }

            if (currentlyDragging != null && Input.GetMouseButtonUp(0)) // Si soltamoso el boton del mouse 
            {
                Vector2 previousPosition = new Vector2Int(currentlyDragging.currentX, currentlyDragging.currentY);

                bool validMove = MoveTo(currentlyDragging, hitPosition.x, hitPosition.y);
                if (!validMove)
                {
                    currentlyDragging.transform.position = GetTileCenter(currentlyDragging.currentX, currentlyDragging.currentY);
                    currentlyDragging = null;
                }
            }

        }
        else // Rayo NO impacto una casilla (raton salio del tablero)
        {
            if (currentHover != -Vector2Int.one) // Si antes habia un hover activo
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile"); // Resetea la casilla a la capa "Tile"
                currentHover = -Vector2Int.one; // Marca el estado como no "hovering"
            }
        }
    }

    // =================================================================================================================================================================================================================================
    //              METODOS DE GENERACION DE TABLERO
    // =================================================================================================================================================================================================================================

    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        // 1. Ajustar el offset Y
        yOffset += transform.position.y;

        // 2. Calcular los limites (Bounds)
        float halfBoardX = (float)tileCountX / 2 * tileSize; // Centro en X
        float halfBoardY = (float)tileCountY / 2 * tileSize; // Centro en Z

        // Guarda el vector de centrado (X, 0, Z)
        bounds = new Vector3(halfBoardX, 0, halfBoardY) + boardCenter;

        // 3. Inicializar el arreglo
        Personajes = new Personaje[tileCountX, tileCountY]; // Inicializa el arreglo de personajes
        tiles = new GameObject[tileCountX, tileCountY]; // Inicializa el arreglo de casillas

        // 4. Iterar y generar cada casilla
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                tiles[x, y] = GenerateSingleTile(tileSize, x, y);
    }

    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        // 1. Crear GameObject y asignarle padre
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tileObject.transform.parent = transform;

        // 2. Generacion del Mesh (Geometria)
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];

        float xCoord = x * tileSize - bounds.x; // Centrado en X
        float yCoord = y * tileSize - bounds.z; // Centrado en Z (el eje vertical de los bounds)

        // Definicion de los vertices ya centrados
        vertices[0] = new Vector3(xCoord, yOffset, yCoord);
        vertices[1] = new Vector3(xCoord, yOffset, yCoord + tileSize);
        vertices[2] = new Vector3(xCoord + tileSize, yOffset, yCoord);
        vertices[3] = new Vector3(xCoord + tileSize, yOffset, yCoord + tileSize);

        int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; // Definicion de los dos triangulos

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 3. Asignar componentes de renderizado y colision
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;
        tileObject.AddComponent<BoxCollider>(); // Se crea el BoxCollider con el tamano y centro por defecto del mesh

        mesh.RecalculateNormals();  // Recalcula las normales para la iluminacion

        // 4. Asignar la capa "Tile"
        tileObject.layer = LayerMask.NameToLayer("Tile");

        return tileObject;
    }

    // =================================================================================================================================================================================================================================
    //              SPAWNEA Y POSICIONA PERSONAJES
    // =================================================================================================================================================================================================================================

    private void SpawnAllCharacters()
    {
        int ChatacterTeam = 0, EnemyTeam = 1;

        Personajes[0, 4] = SpawnSingleCharacter(CharacterMovement.Character1, ChatacterTeam); // Se spawnea Character1 en la posicion [0, 4]

        Personajes[23,4] = SpawnSingleCharacter(CharacterMovement.Enemy1, EnemyTeam); // Se Spawnea Enemy1 en la posicion [23, 4]
    }

    private Personaje SpawnSingleCharacter(CharacterMovement type, int team)
    {
        Personaje cp = Instantiate(prefabs[(int)type - 1], transform).GetComponent<Personaje>(); // Instancia el prefab y obtiene el componente movimiento

        // Asigna las propiedades
        cp.type = type;
        cp.team = team;

        return cp;
    }

    private void PositionAllCharacters()
    {
        // Itera sobre todas las casillas del tablero
        for (int x = 0; x < TILE_COUNT_X; x++)
            for (int y = 0; y < TILE_COUNT_Y; y++)
                // Si hay un personaje en esa posicion, lo mueve
                if (Personajes[x, y] != null)
                    PositionSingleCharacter(x, y, true);
    }

    private void PositionSingleCharacter(int x, int y, bool force = false)
    {
        // Actualiza las coordenadas internas del personaje
        Personajes[x, y].currentX = x;
        Personajes[x, y].currentY = y;

        Personajes[x, y].transform.position = GetTileCenter(x, y); // Asigna la posicion global centrada del tile
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        // Usa la misma logica de centrado que se uso para generar el mesh
        // (esquina inferior de la casilla) - (offset de centrado del tablero) + (mitad del tamano del tile)
        return new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
    }

    // =================================================================================================================================================================================================================================
    //              METODOS DE UTILIDAD
    // =================================================================================================================================================================================================================================

    private bool MoveTo(Personaje cp, int x, int y)
    {
        Vector2Int previousPosition = new Vector2Int(cp.currentX, cp.currentY);

        Personajes[x, y] = cp;
        Personajes[previousPosition.x, previousPosition.y] = null;

        PositionSingleCharacter(x, y);
        return true;
    }
    private Vector2Int LookupTileIndex(GameObject hitInfo) // Busca los indices (X, Y) en el arreglo 'tiles'
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
            for (int y = 0; y < TILE_COUNT_Y; y++)
                if (tiles[x, y] == hitInfo) // Compara la referencia del GameObject
                    return new Vector2Int(x, y);

        return -Vector2Int.one; // Devuelve un valor invalido si no se encuentra (no deberia pasar con un raycast exitoso)
    }
}