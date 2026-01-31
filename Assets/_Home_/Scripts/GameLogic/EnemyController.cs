using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    public float maxDistanceToRoam = 7f;
    public float maxSpeed = 4f;
    public LayerMask obstacleLayerMask = ~0;
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
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null) TryGetComponent<Rigidbody2D>(out _rb);
            return _rb;
        }
    }

    void Start()
    {
        Loop().AttachExternalCancellation(destroyCancellationToken).Forget();
    }

    private async UniTask Loop()
    {
        while (true)
        {
            // Wait
            await WaitForRandomTime();
            // Move
            Vector2 targetPosition = ChooseNextTargetPosition();
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                UniTask moveTask = MoveToTargetPosition(targetPosition, Random.Range(0.9f, maxSpeed), cts.Token);
                UniTask maxTimeTask = UniTask.WaitForSeconds(4f, cancellationToken: cts.Token);

                await UniTask.WhenAny(moveTask, maxTimeTask);
            }
            catch (System.OperationCanceledException)
            {
                // Expected when we cancel, just continue to next iteration
            }
            finally
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
    }

    private async UniTask WaitForRandomTime()
    {
        name = "Waiting";
        float waitTime = Random.Range(0.3f, 2f);
        await UniTask.WaitForSeconds(waitTime);
    }

    private Vector2 ChooseNextTargetPosition()
    {
        return GetValidPointInDirection(Random.insideUnitCircle, Random.Range(0.5f, maxDistanceToRoam));
    }

    private Vector2 GetValidPointInDirection(Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance, obstacleLayerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.transform.IsChildOf(transform))
            {
                continue;
            }
            distance = Mathf.Min(Mathf.Abs(hit.point.y - transform.position.y), distance);
            break;
        }
        distance = Mathf.Max(0, distance - 0.9f);
        return transform.position + (Vector3)direction * distance;
    }

    private async UniTask MoveToTargetPosition(Vector2 targetPosition, float speed, CancellationToken token)
    {
        name = "Moving";
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            token.ThrowIfCancellationRequested();

            Vector2 stepPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            Debug.DrawLine(transform.position, targetPosition, Color.red);
            rb.MovePosition(stepPosition);
            await UniTask.WaitForFixedUpdate(token);
        }
        rb.MovePosition(targetPosition);
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
