using UnityEngine;

public class TriggerArena : MonoBehaviour
{
    [Header("Trascina gli oggetti qui sotto")]
    public AudioSource musica;
    public GameObject interfacciaBoss;
    public GameObject interfacciaPlayer;
    public MonoBehaviour scriptMovimentoBoss; // Trascina qui lo script del boss (es. BossAI)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (musica != null) musica.Play();
            if (interfacciaBoss != null) interfacciaBoss.SetActive(true);
            if (interfacciaPlayer != null) interfacciaPlayer.SetActive(true);
            if (scriptMovimentoBoss != null) scriptMovimentoBoss.enabled = true;

            // Opzionale: disattiva il trigger dopo l'uso per non far ripartire la musica
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}