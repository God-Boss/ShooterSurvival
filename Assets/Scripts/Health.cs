using UnityEngine;

public class Health : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public bool esJugador = false;

    private float vidaActual;
    private Animator animator;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponentInChildren<Animator>();
    }

    public void RecibirDanio(float danio)
    {
        vidaActual -= danio;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (esJugador)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.JugadorMuerto();
            }
        }
        else
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemigoMuerto();
            }

            Destroy(gameObject, 3f);
        }
    }

    public float GetVidaActual()
    {
        return vidaActual;
    }
}