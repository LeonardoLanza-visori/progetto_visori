using UnityEngine;

public class BossAttackVR : MonoBehaviour
{
    [Header("Impostazioni Attacco")]
    public float dannoBoss = 10f;
    public float velocitaAttacco = 1.5f; // Un colpo ogni secondo e mezzo

    private float timerAttacco;
    private Transform playerTransform;

    private void OnCollisionStay(Collision collision)
    {
        // Cerca il player in vari modi (per VR)
        PlayerHealth playerHealth = null;

        // Controlla se ha colpito direttamente il player
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        }

        // Se non trovato, cerca nei parent (per XR Origin)
        if (playerHealth == null)
        {
            playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
        }

        // Se non trovato, cerca per nome
        if (playerHealth == null)
        {
            GameObject xrOrigin = GameObject.Find("XR Origin");
            if (xrOrigin != null)
            {
                playerHealth = xrOrigin.GetComponent<PlayerHealth>();
            }
        }

        // Se abbiamo trovato il player, attacca
        if (playerHealth != null)
        {
            timerAttacco += Time.deltaTime;
            if (timerAttacco >= velocitaAttacco)
            {
                playerHealth.PrendiDanno(dannoBoss);
                Debug.Log("Il Boss ha colpito il Player! Danno: " + dannoBoss);
                timerAttacco = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset timer quando il player esce
        timerAttacco = 0;
    }
}