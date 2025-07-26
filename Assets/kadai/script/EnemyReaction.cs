using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private bool isLockedOn = false;
    private float hitFlashDuration = 0.3f;
    private float hitTimer = 0f;
    private Vector3 originalPos;
    private float shakeDuration = 0.3f;
    private float shakeTimer = 0f;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }

        originalPos = transform.position;
    }

    public void OnLockOn(bool state)
    {
        isLockedOn = state;
        if (rend != null)
        {
            rend.material.color = isLockedOn ? Color.red : originalColor;
        }
    }

    public void OnHit()
    {
        hitTimer = hitFlashDuration;
        shakeTimer = shakeDuration;
        if (rend != null)
        {
            rend.material.color = Color.yellow;
        }
    }

    void Update()
    {
        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f && !isLockedOn)
            {
                rend.material.color = originalColor;
            }
        }

        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            Vector3 shakeOffset = Random.insideUnitSphere * 0.2f;
            transform.position = originalPos + shakeOffset;
            if (shakeTimer <= 0f)
            {
                transform.position = originalPos;
            }
        }
    }
}
