using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int playerIdx;
    public HashSet<int> targetsInRange = new HashSet<int>();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            AddEnemyToEnemiesInRange(other.gameObject.GetComponent<Target>().getIndex());
        }
    }

    public void AddEnemyToEnemiesInRange(int enemyIndex)
    {
        targetsInRange.Add(enemyIndex);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            int friendIndex = other.gameObject.GetComponent<Target>().getIndex();
            targetsInRange.Remove(friendIndex);
        }
    }

}
