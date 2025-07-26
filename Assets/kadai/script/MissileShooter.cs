using UnityEngine;
using System.Collections.Generic;

public class MissileShooter : MonoBehaviour
{
    public GameObject missilePrefab;
    public int missileCount = 8;
    public float spreadAngle = 30f;
    public Transform missileSpawnPoint;

    private List<Transform> lockOnTargets = new List<Transform>();
    private bool isLocking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isLocking = true;
            lockOnTargets.Clear();
        }

        if (Input.GetKey(KeyCode.Z))
        {
            UpdateLockOnTargets();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            FireMissiles();
            isLocking = false;
        }
    }

    void UpdateLockOnTargets()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 100f);
        lockOnTargets.Clear();
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy") && lockOnTargets.Count < missileCount)
            {
                lockOnTargets.Add(enemy.transform);
                enemy.GetComponent<EnemyReaction>()?.OnLockOn(true);
            }
        }
    }

    void FireMissiles()
    {
        for (int i = 0; i < lockOnTargets.Count; i++)
        {
            Vector3 spreadDir = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0) * transform.forward;

            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
            missile.GetComponent<MissileController>().Initialize(lockOnTargets[i], spreadDir);
        }

        foreach (var enemy in lockOnTargets)
        {
            enemy.GetComponent<EnemyReaction>()?.OnLockOn(false);
        }

        lockOnTargets.Clear();
    }
}

