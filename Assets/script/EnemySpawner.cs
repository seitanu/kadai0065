using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int enemyCount = 24;
    public float spawnRadius = 50f;
    public float spawnDepth = 100f; // �v���C���[�O�������̉��s��

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(10f, spawnDepth) // �O���̂�
            );

            Vector3 spawnPosition = player.position + player.forward * randomOffset.z + player.right * randomOffset.x + player.up * randomOffset.y;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.AddComponent<EnemyFloat>();
        }
    }
}