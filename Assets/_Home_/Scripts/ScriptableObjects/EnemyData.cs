using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    public Sprite sprite;
}
