
using UnityEngine;

public class WeaponDamageVR : MonoBehaviour
{
    [Header("Impostazioni Danno")]
    public float dannoLancia = 20f;
    public float velocitaMinima = 1f; // Velocit� minima per fare danno

    private Rigidbody rb;
    private bool puoColpire = true;
    private float cooldown = 0.5f; // Tempo tra un colpo e l'altro

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Se non ha Rigidbody, aggiungilo
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Controlliamo se abbiamo colpito il Boss
        if (collision.gameObject.CompareTag("Boss") && puoColpire)
        {
            // Controlla la velocit� del colpo
            float velocita = rb.linearVelocity.magnitude;

            Debug.Log("Velocit� colpo: " + velocita);

            if (velocita >= velocitaMinima)
            {
                // Cerchiamo lo script BossHealth
                BossHealth salute = collision.gameObject.GetComponent<BossHealth>();
                if (salute != null)
                {
                    salute.PrendiDanno(dannoLancia);
                    Debug.Log("Boss colpito! Danno: " + dannoLancia);

                    // Cooldown per evitare colpi multipli
                    StartCoroutine(CooldownColpo());
                }
            }
            else
            {
                Debug.Log("Colpo troppo debole!");
            }
        }
    }

    private System.Collections.IEnumerator CooldownColpo()
    {
        puoColpire = false;
        yield return new WaitForSeconds(cooldown);
        puoColpire = true;
    }
}