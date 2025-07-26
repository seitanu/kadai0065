using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target;
    public float speed = 30f;
    public float turnSpeed = 20f; // degrees per second
    public float lifetime = 5f;
    public float hitThreshold = 1.0f;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // ベクトル補正で方向を少しずつ変える（最大旋回角制限）
        Vector3 currentDir = transform.forward;
        float maxRadians = turnSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(currentDir, direction, maxRadians, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // 前進
        transform.position += transform.forward * speed * Time.deltaTime;

        // 命中判定
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < hitThreshold)
        {
            target.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

        // 寿命
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}