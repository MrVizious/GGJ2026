using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    bool canMove = true;

    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyController friendPrefab;

    public EnemyController SpawnEnemy(Sprite enemySprite)
    {
        EnemyController new_enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        new_enemy.SetEnemySprite(enemySprite);
        return new_enemy;
    }

    public EnemyController SpawnFriend(Sprite enemySprite, int idx)
    {
        EnemyController new_enemy = Instantiate(friendPrefab, transform.position, Quaternion.identity);
        new_enemy.tag = "Friend";
        new_enemy.SetEnemySprite(enemySprite);
        new_enemy.GetComponent<Target>().setIndex(idx);
        return new_enemy;
    }
}