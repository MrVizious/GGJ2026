using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class MainMenuSpawner : MonoBehaviour
{
    public bool isLeft;
    public MainMenuEnemy mainMenuEnemy;
    public List<AnimatorOverrideController> animations = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Loop().Forget();
    }

    private void Spawn()
    {
        
        MainMenuEnemy newEnemy  = Instantiate(mainMenuEnemy, transform);
        mainMenuEnemy.goesLeft = !isLeft;
        mainMenuEnemy.GetComponent<Animator>().runtimeAnimatorController = animations[Random.Range(0 , animations.Count)];
        mainMenuEnemy.GetComponent<Animator>().SetBool("isWalking", true);
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
