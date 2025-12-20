using UnityEngine;

public class CrowdPerson : MonoBehaviour
{
    [Header("Salto")]
    public float jumpHeight = 0.2f;   // altezza del salto
    public float jumpSpeed = 4f;      // velocità del salto

    [Header("Rotazione verso target")]
    public float rotationSpeed = 5f;  // velocità di rotazione

    private bool attivo = false;
    private float baseY;
    
    // Per la rotazione
    private bool shouldRotate = false;
    private Vector3 rotationTarget;
    
    // Per randomizzare il salto
    private float jumpTimeOffset = 0f;

    private void Start()
    {
        // Salviamo la posizione Y iniziale
        baseY = transform.localPosition.y;
    }

    // Chiamato dal trigger
    public void Attiva()
    {
        attivo = true;
    }

    // Chiamato dal trigger per impostare il target di rotazione
    public void SetRotationTarget(Vector3 target, float speed)
    {
        rotationTarget = target;
        rotationSpeed = speed;
        shouldRotate = true;
    }

    // Chiamato dal trigger per randomizzare l'offset del salto
    public void SetRandomJumpOffset()
    {
        // Offset casuale tra 0 e 2*PI per variare il momento di inizio del salto
        jumpTimeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        if (!attivo) return;

        // --- SALTO SU E GIÙ ---
        // Usa l'offset per variare il momento del salto per ogni sprite
        float newY = baseY + Mathf.Sin(Time.time * jumpSpeed + jumpTimeOffset) * jumpHeight;
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, newY, pos.z);

        // --- ROTAZIONE VERSO TARGET ---
        if (shouldRotate)
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        // Calcola la direzione verso il target (usando la posizione world, non local)
        Vector3 direction = rotationTarget - transform.position;
        
        // Ignora la componente verticale (asse Y in 3D)
        direction.y = 0f;

        // Se la direzione è praticamente zero, non ruotare
        if (direction.sqrMagnitude < 0.001f) 
        {
            shouldRotate = false;
            return;
        }

        // Calcola la rotazione target guardando verso la direzione
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Ruota gradualmente verso il target SOLO sull'asse Y
        Vector3 currentEuler = transform.eulerAngles;
        float targetY = targetRotation.eulerAngles.y;
        
        // Interpola solo l'asse Y
        float newY = Mathf.LerpAngle(currentEuler.y, targetY, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, newY, 0);

        // Ferma la rotazione quando è abbastanza vicino
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currentEuler.y, targetY));
        if (angleDiff < 0.5f)
        {
            transform.eulerAngles = new Vector3(0, targetY, 0);
            shouldRotate = false; // Rotazione completata
        }
    }

    // Visualizza nel Scene view la direzione verso cui guarda
    private void OnDrawGizmosSelected()
    {
        if (shouldRotate && attivo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, rotationTarget);
            
            Gizmos.color = Color.blue;
            Vector3 forward = transform.right; // In 2D, "forward" è spesso "right"
            Gizmos.DrawRay(transform.position, forward * 2f);
        }
    }
}