using UnityEngine;
using UnityEngine.AI;

public class BossWalkAnimator : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
            agent = GetComponentInChildren<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (agent == null || animator == null) return;

        // Se il boss si muove → camminata, altrimenti idle
        bool staCamminando = agent.velocity.magnitude > 0.1f;
        animator.SetBool("IsWalking", staCamminando);
    }
}