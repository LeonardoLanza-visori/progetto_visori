using UnityEngine;

public class TriggerCancelli : MonoBehaviour
{
    [Header("Riferimenti Cancelli")]
    public Transform cancelloRetro;
    public Transform cancelloDavanti;

    [Header("Velocità movimento")]
    public float speedRetro = 2f;
    public float speedDavanti = 2f;

    private bool attivato = false;

    private float retroTargetY;
    private float davantiTargetY = -0.85f;

    private Renderer retroRenderer;
    private bool retroScomparso = false;

    private void Start()
    {
        // Calcolo la posizione finale del cancello retro = posizione attuale + 20
        if (cancelloRetro != null)
        {
            retroTargetY = cancelloRetro.position.y + 20f;
            retroRenderer = cancelloRetro.GetComponent<Renderer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attivato && other.CompareTag("Player"))
        {
            attivato = true;
        }
    }

    private void Update()
    {
        if (!attivato)
            return;

        // --- C A N C E L L O   R E T R O ---
        if (cancelloRetro != null && !retroScomparso)
        {
            Vector3 pos = cancelloRetro.position;
            float newY = Mathf.MoveTowards(pos.y, retroTargetY, speedRetro * Time.deltaTime);
            cancelloRetro.position = new Vector3(pos.x, newY, pos.z);

            // Quando raggiunge la destinazione → invisibile
            if (Mathf.Abs(newY - retroTargetY) < 0.01f)
            {
                if (retroRenderer != null)
                    retroRenderer.enabled = false;

                retroScomparso = true;
            }
        }

        // --- C A N C E L L O   D A V A N T I ---
        if (cancelloDavanti != null)
        {
            Vector3 pos = cancelloDavanti.position;
            float newY = Mathf.MoveTowards(pos.y, davantiTargetY, speedDavanti * Time.deltaTime);
            cancelloDavanti.position = new Vector3(pos.x, newY, pos.z);
        }
    }
}
