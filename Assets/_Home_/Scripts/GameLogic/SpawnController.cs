using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    private List<SpawnPoint> _spawnPoints;
    private List<SpawnPoint> spawnPoints
    {
        get
        {
            if (_spawnPoints == null) _spawnPoints = new List<SpawnPoint>(FindObjectsByType(typeof(SpawnPoint), FindObjectsInactive.Include, FindObjectsSortMode.None) as SpawnPoint[]);
            return _spawnPoints;
        }
    }
    private List<EnemyController> spawnedFriends = new List<EnemyController>();

    [SerializeField]
    public List<Sprite> enemySprites;

    public void DespawnFriend(int friendIndex)
    {
        spawnedFriends[friendIndex].gameObject.SetActive(false);
    }
    public void SpawnEnemies(List<SpawnPoint> randomizedSpawnPoints, List<Sprite> randomizedEnemySprites)
    {
        Debug.Log($"Randomized spawn points size: {randomizedSpawnPoints.Count}, randomized snemy sprites size: {randomizedEnemySprites.Count}", this);
        for (int i = 0; i < 3; i++)
        {
            spawnedFriends.Add(randomizedSpawnPoints[i].SpawnFriend(randomizedEnemySprites[i], i));
        }

        int j = 3;
        for (int i = 3; i < randomizedSpawnPoints.Count; i++)
        {
            randomizedSpawnPoints[i].SpawnEnemy(randomizedEnemySprites[j]);
            j++;
            if (j >= randomizedEnemySprites.Count)
            {
                j = 3;
            }
        }
    }

    public List<SpawnPoint> GetSpawnPoints() => spawnPoints;
    public List<Sprite> GetEnemySprites() => enemySprites;
}