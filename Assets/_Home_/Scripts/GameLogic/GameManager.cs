using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HudController hudController;

    [SerializeField] private Sprite playerLeftCaught;
    [SerializeField] private Sprite playerRightCaught;

    SpawnController spawnController;
    List<SpawnPoint> randomizedSpawnPoints;
    List<EnemyData> randomizedEnemyDatas;

    private bool[] targetsCaught = new bool[3];

    void Start()
    {
        spawnController = GetComponent<SpawnController>();
        randomizedSpawnPoints = RandomizeList(spawnController.GetSpawnPoints());
        randomizedEnemyDatas = RandomizeList(spawnController.GetEnemies());
        spawnController.SpawnEnemies(randomizedSpawnPoints, randomizedEnemyDatas);
        hudController.SetTargetSprite(randomizedEnemyDatas[0].sprite, 0);
        hudController.SetTargetSprite(randomizedEnemyDatas[1].sprite, 1);
        hudController.SetTargetSprite(randomizedEnemyDatas[2].sprite, 2);
    }

    List<T> RandomizeList<T>(List<T> list)
    {
        List<T> randomized = new List<T>(list);
        int n = randomized.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = randomized[i];
            randomized[i] = randomized[j];
            randomized[j] = temp;
        }
        return randomized;
    }

    public void InteractWithTarget(int targetIdx, int playerIdx)
    {
        targetsCaught[targetIdx] = true;
        Sprite caughtSprite = playerIdx == 0 ? playerLeftCaught : playerRightCaught;
        hudController.TargetCaught(caughtSprite, targetIdx);
        foreach (bool caught in targetsCaught)
        {
            if (caught == false)
            {
                return;
            }
        }
        SceneManager.LoadScene("EndScene");
    }
}