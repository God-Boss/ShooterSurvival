using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textoEnemigos;

    private int enemigosRestantes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        EnemyAI[] enemigos = FindObjectsOfType<EnemyAI>();
        enemigosRestantes = enemigos.Length;
        ActualizarUI();
    }

    public void EnemigoMuerto()
    {
        enemigosRestantes--;
        ActualizarUI();

        if (enemigosRestantes <= 0)
        {
            Victoria();
        }
    }

    public void JugadorMuerto()
    {
        Derrota();
    }

    void ActualizarUI()
    {
        if (textoEnemigos != null)
        {
            textoEnemigos.text = "Enemigos: " + enemigosRestantes;
        }
    }

    void Victoria()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Win");
    }

    void Derrota()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Derrota");
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}