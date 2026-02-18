using UnityEngine;

public class PlayerVRWalkAnimator : MonoBehaviour
{
    [Header("Riferimenti")]
    public Animator animator;
    public Transform xrCamera; // Main Camera VR

    [Header("Impostazioni")]
    public float sogliaCamminata = 0.3f;

    private Vector3 posizionePrec;

    void Start()
    {
        if (xrCamera == null)
            xrCamera = Camera.main.transform;

        if (animator == null)
            animator = GetComponent<Animator>();

        posizionePrec = xrCamera.position;
    }

    void Update()
    {
        if (xrCamera == null || animator == null) return;

        // Calcola se il player si sta muovendo
        Vector3 movimento = xrCamera.position - posizionePrec;
        movimento.y = 0; // Ignora movimento verticale

        float velocita = movimento.magnitude / Time.deltaTime;
        posizionePrec = xrCamera.position;

        // Attiva camminata se velocità > soglia
        bool staCamminando = velocita > sogliaCamminata;
        animator.SetBool("IsWalking", staCamminando);
    }
}