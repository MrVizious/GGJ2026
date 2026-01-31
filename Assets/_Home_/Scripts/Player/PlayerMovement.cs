using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null) TryGetComponent<Rigidbody2D>(out _rb);
            return _rb;
        }
    }
    private Vector2 movementVector;

    public void SetMovementVector(InputAction.CallbackContext context)
    {
        Vector2 newMovementVector = context.ReadValue<Vector2>().normalized;
        Debug.Log($"New input for {name} is {newMovementVector}", this);
        movementVector = newMovementVector;
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movementVector.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
