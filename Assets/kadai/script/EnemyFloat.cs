using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float floatAmplitude = 0.5f;

    private Vector3 startPos;
    private float floatOffset;

    void Start()
    {
        startPos = transform.position;
        floatOffset = Random.Range(0f, 2f * Mathf.PI); // ”g‚ÌƒYƒŒ‚ð‚Â‚¯‚é
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed + floatOffset) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, newY, 0f);
    }
}