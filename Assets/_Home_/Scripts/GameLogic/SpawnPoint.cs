using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    bool canMove = true;

    public EnemyData SpawnEnemy(EnemyData enemyPrefab)
    {
        return Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}