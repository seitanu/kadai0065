using System.Collections.Generic;
using UnityEngine;


public class LockOnSystem : MonoBehaviour
{
    public float lockOnRange = 80f;
    public float lockOnAngle = 45f;
    public int maxTargets = 8;
    public Material normalMaterial;
    public Material lockedMaterial;

    public GameObject missilePrefab;
    public Transform missileSpawnPoint; // プレイヤーの中心など

    private List<Transform> lockedTargets = new List<Transform>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            UpdateLockOnTargets();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            FireMissiles(); // 後で実装
            ClearLockOn();
        }
    }

    void UpdateLockOnTargets()
    {
        ClearLockOn(); // 毎フレーム更新

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<(Transform, float)> candidates = new List<(Transform, float)>();

        foreach (GameObject enemy in allEnemies)
        {
            Transform target = enemy.transform;
            Vector3 dirToTarget = target.position - transform.position;
            float angle = Vector3.Angle(transform.forward, dirToTarget);

            if (angle < lockOnAngle && dirToTarget.magnitude < lockOnRange)
            {
                candidates.Add((target, dirToTarget.magnitude));
            }
        }

        candidates.Sort((a, b) => a.Item2.CompareTo(b.Item2)); // 距離昇順

        int count = Mathf.Min(maxTargets, candidates.Count);
        for (int i = 0; i < count; i++)
        {
            Transform target = candidates[i].Item1;
            lockedTargets.Add(target);

            Renderer r = target.GetComponent<Renderer>();
            if (r != null) r.material = lockedMaterial;
        }
    }

    void ClearLockOn()
    {
        foreach (Transform t in lockedTargets)
        {
            if (t != null)
            {
                Renderer r = t.GetComponent<Renderer>();
                if (r != null) r.material = normalMaterial;
            }
        }
        lockedTargets.Clear();
    }

    void FireMissiles()
    {
        if (missilePrefab == null || missileSpawnPoint == null) return;

        int count = lockedTargets.Count;
        if (count == 0) return;

        float angleStep = 360f / count;
        float radius = 2f;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            Vector3 spawnPos = missileSpawnPoint.position + missileSpawnPoint.TransformDirection(offset);

            GameObject missile = Instantiate(missilePrefab, spawnPos, missileSpawnPoint.rotation);
            MissileController mc = missile.GetComponent<MissileController>();
            mc.target = lockedTargets[i];
        }
    }

    public List<Transform> GetLockedTargets()
    {
        return lockedTargets;
    }
}