using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    public List<SpawnPoint> spawnPoints;
    
    [SerializeField]
    public List<EnemyData> enemies;

    public void SpawnEnemies(List<SpawnPoint> randomizedSpawnPoints, List<EnemyData> randomizedEnemyDatas)
    {
        for (int i = 0; i < 3; i++)
        {
            randomizedSpawnPoints[i].SpawnFriend(randomizedEnemyDatas[i], i);
        }

        for (int i = 3; i < randomizedSpawnPoints.Count; i++)
        {
            randomizedSpawnPoints[i].SpawnEnemy(randomizedEnemyDatas[i]);
        }
    }
    
    public List<SpawnPoint> GetSpawnPoints() => spawnPoints;
    public List<EnemyData> GetEnemies() => enemies;
}