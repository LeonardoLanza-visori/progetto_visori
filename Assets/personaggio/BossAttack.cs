using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float dannoBoss = 10f;
    private float timerAttacco;
    public float velocitaAttacco = 1.5f; // Un colpo ogni secondo e mezzo

    private void OnCollisionStay(Collision collision)
    {
        // Se il boss tocca il Player
        if (collision.gameObject.CompareTag("Player"))
        {
            timerAttacco += Time.deltaTime;
            if (timerAttacco >= velocitaAttacco)
            {
                Debug.Log("Il Player ha subito danno!");
                // Qui chiameremo la funzione PrendiDanno del Player (che faremo dopo)
                timerAttacco = 0;
            }
        }
    }
}