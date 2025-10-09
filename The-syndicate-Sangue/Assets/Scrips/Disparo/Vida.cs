using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vida : MonoBehaviour
{

    public int vida = 100;

    void Update()
    {
        if (vida <= 0)
        {
            Debug.Log(gameObject.name + " ha sido destruido!");
            Destroy(gameObject);
        }
    }

    /*
    public int vida = 100;

    DisparoBreach disparoBreach; //Referencia al script DisparoBreach

    //------------------------------------------------
    void Start()//Metodo que se ejecuta al iniciar el juego
    {
        disparoBreach = GetComponent<DisparoBreach>();//Referencia al script DisparoBreach situado en el mismo objeto
    }

    //---------------------------------------------------------------
    //Metodo para recibir danio
    public void RecibirDaño(int cantidad)//Metodo para recibir danio
    {
        vida -= cantidad;//Resta la cantidad de danio a la vida
        Debug.Log("Vida actual de " + gameObject.name + ": " + vida);//Imprime la vida actual en la consola
    }

    //----------------------------------------------------------------
    // Update is called once per frame
    void Update()//Metodo que se ejecuta en cada frame
    {
        if (vida <= 0)//Si la vida es menor o igual a 0
        {
            Debug.Log(gameObject.name + " ha sido destruido.");//Imprime en la consola que el objeto ha sido destruido
            Destroy(gameObject);//Destruye el objeto
        }
    }
    */
}
