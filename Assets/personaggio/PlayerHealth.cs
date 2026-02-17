using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float saluteMax = 100f;
    public float saluteAttuale;
    public Slider barraPlayer;

    void Start()
    {
        saluteAttuale = saluteMax;
        barraPlayer.maxValue = saluteMax;
        barraPlayer.value = saluteMax;
    }

    public void PrendiDanno(float danno)
    {
        saluteAttuale -= danno;
        barraPlayer.value = saluteAttuale;
        if (saluteAttuale <= 0) Debug.Log("Sei morto!");
    }
}