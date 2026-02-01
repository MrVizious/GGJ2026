using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int playerIdx;
    private HashSet<int> targetsInRange = new HashSet<int>();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            AddEnemyToEnemiesInRange(other.gameObject.GetComponent<Target>().getIndex());
        }
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
            targetsInRange.Remove(other.gameObject.GetComponent<Target>().getIndex());
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractWithTargets();
        }
    }
    public void InteractWithTargets()
    {
        if (!gameManager.TryCatch()) return;
        foreach (int idx in targetsInRange)
        {
            gameManager.InteractWithTarget(idx, playerIdx);
        }
    }
}
