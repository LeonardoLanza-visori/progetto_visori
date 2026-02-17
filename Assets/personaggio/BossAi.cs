using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player; // Trascina qui il tuo XR Origin / Camera
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Il boss segue costantemente la posizione del player
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}