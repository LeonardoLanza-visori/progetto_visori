using UnityEngine;

public class CrowdPerson : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float speed = 2f;

    private float offset;
    private Vector3 startPos;
    private bool attivo = false;

    void Start()
    {
        startPos = transform.localPosition;
        offset = Random.Range(0f, 10f);
    }

    public void Attiva()
    {
        attivo = true;
    }

    void LateUpdate()
    {
        if (!attivo) return;

        transform.LookAt(Camera.main.transform);

        float y = Mathf.Sin(Time.time * speed + offset) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
