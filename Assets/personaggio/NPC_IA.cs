using UnityEngine;
using UnityEngine.AI;

public class NPC_IA : MonoBehaviour {
    [Header("Parametri Percorso")]
    public float raggioMinimo = 40f;   // Distanza minima (alza per fare più strada)
    public float raggioMassimo = 150f; // Distanza massima (copre l'arena)
    public float attesaMeta = 0.5f;
    
    private NavMeshAgent agent;
    private Animator anim;
    private float timer;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        // Non scriviamo agent.speed qui, così non si resetta!
        IniziaNuovoPercorso();
    }

    void Update() {
        if (anim != null) {
            // Controlla se l'agente si sta muovendo fisicamente
            float movimentoReale = agent.velocity.magnitude > 0.1f ? 1.0f : 0f;
            // Invia il valore al parametro 'Speed' che hai creato nell'Animator
            anim.SetFloat("Speed", movimentoReale);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
            timer += Time.deltaTime;
            if (timer >= attesaMeta) {
                IniziaNuovoPercorso();
                timer = 0;
            }
        }
    }

    public void IniziaNuovoPercorso() {
        bool trovato = false;
        int tentativi = 0;

        while (!trovato && tentativi < 20) {
            Vector3 direzioneCasuale = (Random.insideUnitSphere * raggioMassimo) + transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(direzioneCasuale, out hit, 40f, NavMesh.AllAreas)) {
                // Accetta solo mete lontane per far fare tanta strada
                if (Vector3.Distance(transform.position, hit.position) >= raggioMinimo) {
                    agent.SetDestination(hit.position);
                    trovato = true;
                }
            }
            tentativi++;
        }
    }
}