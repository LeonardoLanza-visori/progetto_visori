using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float dannoLancia = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        // Controlliamo se abbiamo colpito il Boss
        if (collision.gameObject.CompareTag("Boss"))
        {
            // Cerchiamo lo script BossHealth che abbiamo fatto prima
            BossHealth salute = collision.gameObject.GetComponent<BossHealth>();
            if (salute != null)
            {
                salute.PrendiDanno(dannoLancia);
            }
        }
    }
}