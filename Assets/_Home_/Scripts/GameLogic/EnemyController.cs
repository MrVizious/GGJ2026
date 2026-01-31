using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    private EnemyData enemyData;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null) TryGetComponent<SpriteRenderer>(out _spriteRenderer);
            return _spriteRenderer;
        }
    }


    public void SetEnemyData(EnemyData newEnemyData)
    {
        enemyData = newEnemyData;
    }

    public void UpdateGraphics()
    {
        spriteRenderer.sprite = enemyData.sprite;
    }
}
