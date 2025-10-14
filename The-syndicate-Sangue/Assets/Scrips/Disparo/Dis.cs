using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Enemigo : MonoBehaviour
{
    [SerializeField] private int vida = 3;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Aquí puedes agregar efectos de muerte, animaciones, etc.
        if (animator != null)
        {
            animator.SetTrigger("Morir");
        }
        // Desactivar el objeto después de un breve retraso para permitir que la animación se reproduzca
        Destroy(gameObject, 1f); // Ajusta el tiempo según la duración de tu animación
    }

}