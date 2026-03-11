using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float velocidad = 3f;

    private NavMeshAgent agent;
    private int waypointActual = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidad;

        if (waypoints.Length > 0)
        {
            IrAlSiguienteWaypoint();
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (agent.remainingDistance < 0.5f)
        {
            IrAlSiguienteWaypoint();
        }
    }

    void IrAlSiguienteWaypoint()
    {
        agent.SetDestination(waypoints[waypointActual].position);
        waypointActual = (waypointActual + 1) % waypoints.Length;
    }
}