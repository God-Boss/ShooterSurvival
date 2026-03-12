using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float velocidad = 3f;
    public float rangoDeteccion = 10f;
    public float rangoAtaque = 5f;
    public float cadenciaDisparo = 2f;

    private NavMeshAgent agent;
    private Transform jugador;
    private int waypointActual = 0;
    private float tiempoUltimoDisparo;

    private enum Estado { Patrulla, Persecucion, Ataque }
    private Estado estadoActual = Estado.Patrulla;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidad;
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (waypoints.Length > 0)
        {
            IrAlSiguienteWaypoint();
        }
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque)
        {
            estadoActual = Estado.Ataque;
        }
        else if (distancia <= rangoDeteccion && PuedeVerJugador())
        {
            estadoActual = Estado.Persecucion;
        }
        else
        {
            estadoActual = Estado.Patrulla;
        }

        switch (estadoActual)
        {
            case Estado.Patrulla:
                Patrullar();
                break;
            case Estado.Persecucion:
                Perseguir();
                break;
            case Estado.Ataque:
                Atacar();
                break;
        }
    }

    void Patrullar()
    {
        if (waypoints.Length == 0) return;

        if (agent.remainingDistance < 0.5f)
        {
            IrAlSiguienteWaypoint();
        }
    }

    void Perseguir()
    {
        agent.isStopped = false;
        agent.SetDestination(jugador.position);
    }

    void Atacar()
    {
        agent.isStopped = true;

        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0;
        transform.rotation = Quaternion.LookRotation(direccion);

        if (Time.time > tiempoUltimoDisparo + cadenciaDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = transform.position + transform.forward + Vector3.up;
        bullet.transform.rotation = transform.rotation;
    }

    bool PuedeVerJugador()
    {
        Vector3 direccion = jugador.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, direccion, out hit, rangoDeteccion))
        {
            if (hit.transform == jugador)
            {
                return true;
            }
        }

        return false;
    }

    void IrAlSiguienteWaypoint()
    {
        agent.isStopped = false;
        agent.SetDestination(waypoints[waypointActual].position);
        waypointActual = (waypointActual + 1) % waypoints.Length;
    }
}