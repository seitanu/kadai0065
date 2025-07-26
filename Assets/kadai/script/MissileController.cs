using UnityEngine;
using System.Collections.Generic;

public class MissileController : MonoBehaviour
{
    public float speed = 20f;
    public float maxTurnAngle = 20f;
    public float lifetime = 5f;

    public Transform target;
    private Vector3 currentDirection;
    private float timer;

    public void Initialize(Transform target, Vector3 spreadDirection)
    {
        this.target = target;
        currentDirection = spreadDirection.normalized;
        transform.forward = currentDirection;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 toTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(currentDirection, toTarget);
            if (angle > maxTurnAngle)
            {
                Vector3 axis = Vector3.Cross(currentDirection, toTarget);
                currentDirection = Quaternion.AngleAxis(maxTurnAngle, axis) * currentDirection;
            }
            else
            {
                currentDirection = toTarget;
            }
        }

        transform.position += currentDirection * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyReaction reaction = other.GetComponent<EnemyReaction>();
            if (reaction != null)
            {
                reaction.OnHit();
            }

            Destroy(gameObject);
        }
    }
}
