using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    //stats
    public int ataqueBase = 10;
    public float  health = 100f;
    public float chanceAtaqueExitoso = 0;
    public float ReducirChanceAtaqueExitoso = 0;

    public Enemy2AI enemyAI;
    public float probabilidadTiro = 1f; // Probabilidad de éxito del ataque (0 a 1)

    void Update()
    {

    }


    public void atacar()
    {
        chanceAtaqueExitoso -= ReducirChanceAtaqueExitoso;
        if (probabilidadTiro >= chanceAtaqueExitoso)
        {
            Debug.Log("Ataque Exitoso");
            enemyAI.health -= ataqueBase;
            Debug.Log("Enemy Health: " + enemyAI.health);
        }
        else
        {
            Debug.Log("Ataque Fallido");
        }

    }

    public void Ramdomizar()
    {
        ReducirChanceAtaqueExitoso = Random.Range(0f, 0.5f);
    }

}
