using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    public float reactionDuration = 0.5f;
    public float shakeIntensity = 0.2f;
    public Material normalMaterial;
    public Material hitMaterial;

    private float reactionTimer = 0f;
    private Vector3 originalPosition;
    private Renderer rend;
    private bool isReacting = false;

    void Start()
    {
        originalPosition = transform.position;
        rend = GetComponent<Renderer>();
        if (rend != null) rend.material = normalMaterial;
    }

    void Update()
    {
        if (isReacting)
        {
            reactionTimer -= Time.deltaTime;

            // シェイク処理
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            if (reactionTimer <= 0f)
            {
                // 戻す
                isReacting = false;
                transform.position = originalPosition;
                if (rend != null) rend.material = normalMaterial;
            }
        }
    }

    public void OnHit()
    {
        if (rend != null) rend.material = hitMaterial;
        reactionTimer = reactionDuration;
        originalPosition = transform.position;
        isReacting = true;
    }
}