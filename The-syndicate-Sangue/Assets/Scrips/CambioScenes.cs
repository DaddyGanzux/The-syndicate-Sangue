using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScenes : MonoBehaviour
{
    [SerializeField] private string nombreEscena;
    public void CambiarEscena(string _NombreEscena)//Parametro que recibe el nombre de la escena a cargar
    {
        SceneManager.LoadScene(_NombreEscena);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
