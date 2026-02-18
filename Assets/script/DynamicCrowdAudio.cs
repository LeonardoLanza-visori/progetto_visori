using UnityEngine;

public class DynamicCrowdAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioPubblico;
    [SerializeField] private AudioClip suonoPubblico;

    [Header("Zone Arena")]
    [SerializeField] private Transform centroArena; // Centro dell'arena
    [SerializeField] private float raggioArena = 20f; // Raggio dell'arena

    [Header("Volumi")]
    [SerializeField] private float volumeFuoriArena = 0.2f; // Volume quando sei fuori
    [SerializeField] private float volumeDentroArena = 1f; // Volume quando sei dentro
    [SerializeField] private float velocitaTransizione = 2f; // Quanto veloce cambia il volume

    [Header("Player")]
    [SerializeField] private Transform player; // Main Camera VR

    private float volumeTarget;

    void Start()
    {
        // Setup AudioSource
        if (audioPubblico == null)
        {
            audioPubblico = gameObject.AddComponent<AudioSource>();
        }

        audioPubblico.clip = suonoPubblico;
        audioPubblico.loop = true;
        audioPubblico.playOnAwake = false;
        audioPubblico.spatialBlend = 1f; // 3D audio completo
        audioPubblico.minDistance = 5f;
        audioPubblico.maxDistance = 50f;
        audioPubblico.volume = volumeFuoriArena;

        // Trova player se non assegnato
        if (player == null)
        {
            GameObject mainCam = GameObject.Find("Main Camera");
            if (mainCam != null)
                player = mainCam.transform;
        }

        // Avvia audio
        audioPubblico.Play();

        Debug.Log("Audio pubblico avviato");
    }

    void Update()
    {
        if (player == null || centroArena == null) return;

        // Calcola distanza dal centro arena
        float distanza = Vector3.Distance(player.position, centroArena.position);

        // Determina se sei dentro o fuori l'arena
        if (distanza <= raggioArena)
        {
            // Dentro l'arena
            volumeTarget = volumeDentroArena;
        }
        else
        {
            // Fuori dall'arena
            volumeTarget = volumeFuoriArena;
        }

        // Transizione smooth del volume
        audioPubblico.volume = Mathf.Lerp(audioPubblico.volume, volumeTarget, Time.deltaTime * velocitaTransizione);
    }

    // Visualizza l'area dell'arena nella Scene view
    void OnDrawGizmosSelected()
    {
        if (centroArena == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centroArena.position, raggioArena);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(centroArena.position, 0.5f);
    }
}