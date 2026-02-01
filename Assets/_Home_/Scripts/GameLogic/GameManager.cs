using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HudController hudController;

    [SerializeField] private Sprite playerLeftCaught;
    [SerializeField] private Sprite playerRightCaught;

    [Range(0.1f, 10.0f)]
    public float secondsOfCatchCooldown = 2.0f;
    private float secondsSinceLastCatch = 0.0f;
    public float catchCooldownPercentage => Mathf.Clamp01(secondsSinceLastCatch / secondsOfCatchCooldown);

    private SpawnController _spawnController;
    private SpawnController spawnController
    {
        get
        {
            if (_spawnController == null) _spawnController = FindAnyObjectByType<SpawnController>();
            return _spawnController;
        }
    }

    List<SpawnPoint> randomizedSpawnPoints;
    List<Sprite> randomizedEnemySprites;

    private List<PlayerManager> _players;
    private List<PlayerManager> players
    {
        get
        {
            if (_players == null) _players = new List<PlayerManager>(FindObjectsByType<PlayerManager>(FindObjectsSortMode.None) as PlayerManager[]);
            return _players;
        }
    }

    private bool[] targetsCaught = new bool[] { false, false, false };

    void Start()
    {
        randomizedSpawnPoints = RandomizeList(spawnController.GetSpawnPoints());
        randomizedEnemySprites = RandomizeList(spawnController.GetEnemySprites());
        spawnController.SpawnEnemies(randomizedSpawnPoints, randomizedEnemySprites);
        hudController.SetTargetSprite(randomizedEnemySprites[0], 0);
        hudController.SetTargetSprite(randomizedEnemySprites[1], 1);
        hudController.SetTargetSprite(randomizedEnemySprites[2], 2);

        // Reset catch cooldown
        secondsSinceLastCatch = secondsOfCatchCooldown;
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

    void Update()
    {
        UpdateCooldownTimer();
    }

    private void UpdateCooldownTimer()
    {
        if (secondsSinceLastCatch < secondsOfCatchCooldown)
        {
            secondsSinceLastCatch += Time.deltaTime;
            secondsSinceLastCatch = Mathf.Min(secondsSinceLastCatch, secondsOfCatchCooldown);
            hudController.UpdateCatchCooldown(catchCooldownPercentage);
        }
    }

    public void CatchPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!TryCatch())
            {
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                foreach (int friendIndexInRange in players[i].targetsInRange)
                {
                    InteractWithTarget(friendIndexInRange, i);
                }
            }
        }
    }

    public void InteractWithTarget(int targetIdx, int playerIdx)
    {

        targetsCaught[targetIdx] = true;
        Sprite caughtSprite = playerIdx == 0 ? playerLeftCaught : playerRightCaught;
        hudController.TargetCaught(caughtSprite, targetIdx);
        spawnController.DespawnFriend(targetIdx);

        foreach (bool caught in targetsCaught)
        {
            if (caught == false)
            {
                return;
            }
        }
        SceneManager.LoadScene("CreditsMenu");
    }
    public bool TryCatch()
    {
        if (secondsSinceLastCatch >= secondsOfCatchCooldown)
        {
            // Reset catch cooldown
            secondsSinceLastCatch = 0.0f;
            hudController.UpdateCatchCooldown(catchCooldownPercentage);
            return true;
        }
        return false;
    }
}