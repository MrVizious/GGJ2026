using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    bool canMove = true;

    void Spawn(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
