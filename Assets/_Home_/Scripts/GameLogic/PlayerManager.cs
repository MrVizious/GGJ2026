using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int playerIdx;
    private List<int> targetsInRange;
    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            targetsInRange.Add(other.gameObject.GetComponent<Target>().getIndex());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {if (other.CompareTag("Friend"))
        {
            targetsInRange.Remove(other.gameObject.GetComponent<Target>().getIndex());
        }
    }

    public void InteractWithTargets()
    {
        foreach (int idx in targetsInRange)
        {
            gameManager.InteractWithTarget(idx, playerIdx);
        }
    }
}
