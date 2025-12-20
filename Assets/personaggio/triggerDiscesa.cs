using UnityEngine;

public class TriggerCancelliDown : MonoBehaviour
{
    [Header("Riferimenti Cancelli")]
    public Transform cancelloRetro;
    public Transform cancelloDavanti;

    [Header("Folla - Ricerca Automatica")]
    [Tooltip("Tag degli sprite della folla (lascia vuoto per cercare tutti i CrowdPerson)")]
    public string crowdTag = "Crowd"; // Opzionale: usa un tag per filtrare
    public bool findAllCrowdPeople = true; // Cerca automaticamente tutti i CrowdPerson
    
    [Header("Attivazione Randomica")]
    [Range(0f, 1f)]
    [Tooltip("Percentuale di sprite da attivare (0.5 = 50%, 1.0 = 100%)")]
    public float activationPercentage = 0.7f; // 70% degli sprite si attivano

    [Header("Velocità movimento")]
    public float retroSpeed = 2f;

    [Header("Posizione target retro")]
    public float retroTargetY = -0.8f;

    [Header("Rotazione folla verso origine")]
    public Vector3 targetPosition = Vector3.zero; // Punto verso cui guardare (0, 0, 0)
    public float rotationSpeed = 5f; // Velocità di rotazione

    [Header("Opzioni Rigidbody")]
    public bool forceMakeKinematic = true;

    private bool attivato = false;
    private bool davantiEliminato = false;

    private Rigidbody retroRb;
    private Vector3 retroTargetPos;
    private CrowdPerson[] allCrowdPeople;

    private void Start()
    {
        if (cancelloRetro != null)
        {
            retroTargetPos = new Vector3(cancelloRetro.position.x, retroTargetY, cancelloRetro.position.z);
            retroRb = cancelloRetro.GetComponent<Rigidbody>();
        }

        // Trova automaticamente tutti i CrowdPerson nella scena
        if (findAllCrowdPeople)
        {
            allCrowdPeople = FindObjectsOfType<CrowdPerson>();
            Debug.Log($"[TriggerCancelliDown] Trovati {allCrowdPeople.Length} CrowdPerson nella scena");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attivato && other.CompareTag("Player"))
        {
            attivato = true;

            // --- ATTIVA LA FOLLA RANDOMICAMENTE ---
            ActivateRandomCrowd();

            // Distruggi cancelloDavanti
            if (!davantiEliminato && cancelloDavanti != null)
            {
                Destroy(cancelloDavanti.gameObject);
                davantiEliminato = true;
            }

            // Preparazione Rigidbody del cancello retro
            if (retroRb != null)
            {
                retroRb.linearVelocity = Vector3.zero;
                retroRb.angularVelocity = Vector3.zero;

                if (forceMakeKinematic)
                    retroRb.isKinematic = true;
            }
        }
    }

    private void ActivateRandomCrowd()
    {
        if (allCrowdPeople == null || allCrowdPeople.Length == 0)
        {
            Debug.LogWarning("[TriggerCancelliDown] Nessun CrowdPerson trovato!");
            return;
        }

        int activatedCount = 0;

        foreach (CrowdPerson person in allCrowdPeople)
        {
            if (person == null) continue;

            // Randomizza se questo sprite si attiva o no
            float randomValue = Random.Range(0f, 1f);
            
            if (randomValue <= activationPercentage)
            {
                // Attiva lo sprite
                person.Attiva();
                
                // Imposta il target di rotazione
                person.SetRotationTarget(targetPosition, rotationSpeed);
                
                // Randomizza l'offset del salto per variare l'animazione
                person.SetRandomJumpOffset();
                
                activatedCount++;
            }
        }

        Debug.Log($"[TriggerCancelliDown] Attivati {activatedCount}/{allCrowdPeople.Length} sprite della folla ({(activationPercentage * 100f):F0}%)");
    }

    private void Update()
    {
        if (!attivato || cancelloRetro == null) return;

        if (retroRb == null)
        {
            Vector3 pos = cancelloRetro.position;
            float newY = Mathf.MoveTowards(pos.y, retroTargetPos.y, retroSpeed * Time.deltaTime);
            cancelloRetro.position = new Vector3(pos.x, newY, pos.z);
        }
    }

    private void FixedUpdate()
    {
        if (!attivato || cancelloRetro == null || retroRb == null) return;

        Vector3 pos = retroRb.position;
        float newY = Mathf.MoveTowards(pos.y, retroTargetPos.y, retroSpeed * Time.fixedDeltaTime);
        Vector3 next = new Vector3(pos.x, newY, pos.z);

        retroRb.MovePosition(next);
    }

    // Metodo per attivare manualmente tutti gli sprite (per test)
    [ContextMenu("Attiva Tutta La Folla")]
    public void ActivateAllCrowd()
    {
        if (allCrowdPeople == null) return;

        foreach (CrowdPerson person in allCrowdPeople)
        {
            if (person != null)
            {
                person.Attiva();
                person.SetRotationTarget(targetPosition, rotationSpeed);
                person.SetRandomJumpOffset();
            }
        }
    }
}