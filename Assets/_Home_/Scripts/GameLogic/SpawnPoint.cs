using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    bool canMove = true;

    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyController friendPrefab;

    public EnemyController SpawnEnemy(EnemyData enemyData)
    {
        EnemyController new_enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        new_enemy.SetEnemyData(enemyData);
        return new_enemy;
    }
    
    public EnemyController SpawnFriend(EnemyData enemyData, int idx)
    {
        EnemyController new_enemy = Instantiate(friendPrefab, transform.position, Quaternion.identity);
        new_enemy.SetEnemyData(enemyData);
        new_enemy.GetComponent<Target>().setIndex(idx);
        return new_enemy;
    }
}