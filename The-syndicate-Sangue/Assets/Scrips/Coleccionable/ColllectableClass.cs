[System.Serializable]
public class CollectableData
{
    public string id;
    public string name;
    // Puedes guardar más info aquí (descripción, ruta del modelo, etc.)

    public CollectableData(string _id, string _name)
    {
        id = _id;
        name = _name;
    }
}