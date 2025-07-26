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

        // �x�N�g���␳�ŕ������������ς���i�ő����p�����j
        Vector3 currentDir = transform.forward;
        float maxRadians = turnSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(currentDir, direction, maxRadians, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // �O�i
        transform.position += transform.forward * speed * Time.deltaTime;

        // ��������
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < hitThreshold)
        {
            target.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

        // ����
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}