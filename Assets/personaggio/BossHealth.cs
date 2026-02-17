using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float vitaMassima = 100f;
    public float vitaAttuale;
    public Slider barraVitaBoss; // Questo è il campo dove trascinerai lo Slider

    void Start()
    {
        vitaAttuale = vitaMassima;
        if (barraVitaBoss != null)
        {
            barraVitaBoss.maxValue = vitaMassima;
            barraVitaBoss.value = vitaMassima;
        }
    }

    public void PrendiDanno(float danno)
    {
        vitaAttuale -= danno;
        if (barraVitaBoss != null)
        {
            barraVitaBoss.value = vitaAttuale;
        }

        if (vitaAttuale <= 0)
        {
            Debug.Log("Boss Morto!");
            gameObject.SetActive(false); // Il boss scompare
        }
    }
}