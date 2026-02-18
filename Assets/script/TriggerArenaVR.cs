using UnityEngine;

public class TriggerArenaVR : MonoBehaviour
{
    [Header("Trascina gli oggetti qui sotto")]
    public AudioSource musica;
    public GameObject interfacciaBoss;
    public GameObject interfacciaPlayer;
    public MonoBehaviour scriptMovimentoBoss;
    public GameObject boss;

    [Header("Impostazioni")]
    public bool attivaUnaSolaVolta = true;

    private bool giaAttivato = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Qualcosa è entrato nel trigger: " + other.gameObject.name);

        // Versione semplificata - controlla solo il tag
        if (other.CompareTag("Player") && (!attivaUnaSolaVolta || !giaAttivato))
        {
            giaAttivato = true;
            Debug.Log("PLAYER ENTRATO NELL'ARENA!");

            if (musica != null) musica.Play();
            if (interfacciaBoss != null) interfacciaBoss.SetActive(true);
            if (interfacciaPlayer != null) interfacciaPlayer.SetActive(true);
            if (scriptMovimentoBoss != null) scriptMovimentoBoss.enabled = true;
            if (boss != null && !boss.activeSelf) boss.SetActive(true);

            if (attivaUnaSolaVolta)
            {
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}