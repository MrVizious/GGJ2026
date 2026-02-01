using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class MainMenuSpawner : MonoBehaviour
{
    public bool isLeft;
    public List<MainMenuEnemy> mainMenuEnemies = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Loop().Forget();
    }

    private void Spawn()
    {
        MainMenuEnemy newEnemy  = Instantiate(mainMenuEnemies[Random.Range(0 , mainMenuEnemies.Count)], transform);
        newEnemy.goesLeft = !isLeft;
    }

    private async UniTask Loop()
    {
        while (true)
        {
            await UniTask.WaitForSeconds(Random.Range(0.2f , 4f));
            Spawn();
        }
    }
}
