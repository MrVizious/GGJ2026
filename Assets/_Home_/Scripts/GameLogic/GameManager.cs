using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HudController hudController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private Sprite playerLeftCaught;
    [SerializeField] private Sprite playerRightCaught;

    [SerializeField] private int friendsLeftCaught;
    [SerializeField] private int friendsRightCaught;

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
            _players.Sort(compareByObjectName);
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

        soundController.reproduceSfxClip(14);
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
                soundController.reproduceSfxClip(6);
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                foreach (int friendIndexInRange in players[i].targetsInRange)
                {
                    Debug.Log("Targets in range: "+ i);
                    InteractWithTarget(friendIndexInRange, players[i].getPlayerIndex());
                }
            }
        }
        
    }

    public void InteractWithTarget(int targetIdx, int playerIdx)
    {
        soundController.reproduceSfxClip(2);
        targetsCaught[targetIdx] = true;
        Debug.Log("Player index:" + playerIdx);
        Sprite caughtSprite = playerIdx == 1 ? playerLeftCaught : playerRightCaught;
        hudController.TargetCaught(caughtSprite, targetIdx);
        spawnController.DespawnFriend(targetIdx);

        switch (playerIdx)
        {
            case 1:
            friendsLeftCaught++;
            break;
            case 2:
            friendsRightCaught++;
            break;
            default:
            friendsLeftCaught++;
            break;
        }
        foreach (bool caught in targetsCaught)
        {
            if (caught == false)
            {
                return;
            }
        }
        Debug.Log($"Game is finished", this);
         if(friendsLeftCaught > friendsRightCaught)
        {
            SceneManager.LoadScene("WinLeftScene");
        }
        else
        {
            SceneManager.LoadScene("WinRightScene");
        }
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

    private static int compareByObjectName(PlayerManager playerA, PlayerManager playerB)
    {
        if(playerA.getPlayerIndex() > playerB.getPlayerIndex())
        {
            return 1;
        }
        else
        {
            return -1;
        }
            
    }
}