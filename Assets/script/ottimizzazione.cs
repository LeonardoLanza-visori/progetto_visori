using UnityEngine;

public class ottimizzazione : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnBecameInvisible()
    {
        if (rend != null)
            rend.enabled = false;
    }

    void OnBecameVisible()
    {
        if (rend != null)
            rend.enabled = true;
    }
}
