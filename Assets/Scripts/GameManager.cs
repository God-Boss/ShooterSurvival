using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textoEnemigos;
    public GameObject panelVictoria;

    private int enemigosRestantes;

    void Awake()
    {
        Instance = this;
        enemigosRestantes = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).Length;
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
        Time.timeScale = 0f;
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
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