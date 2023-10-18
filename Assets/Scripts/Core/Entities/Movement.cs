using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Movement : MonoBehaviour
{
    protected const float INPUT_ADMISSION = 0.25f;
    protected const float SPEED_MULTIPLY_KOEF = 10f;

    public event Action<float> OnSpeedChanged;

    [SerializeField] protected float _movementSpeed = 10f;
    [SerializeField] protected float _rotationSpeed = 60f;

    protected Rigidbody _rigidbody;
    protected Vector2 _input;
    protected bool _isCanMove = true;

    public bool IsCanMove { set { _isCanMove = value; } }


    protected virtual void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        UpdateMovement(_input);
    }

    protected virtual void UpdateMovement(Vector2 input)
    {
        if (!_isCanMove)
        {
            return;
        }
        ChangeMovementInput();
        Vector3 movementDirection = CalculateMovementDirection(input);
        OnSpeedChanged?.Invoke(input.magnitude);

        if (movementDirection.magnitude < INPUT_ADMISSION)
        {
            return;
        }

        RotatePlayer(movementDirection);
        SetRigidbodyVelocity(movementDirection);
    }
    protected virtual Vector3 CalculateMovementDirection(Vector2 input)
    {
        float horizontalInput = input.x;
        float verticalInput = input.y;

        return Vector3.right * horizontalInput + Vector3.forward * verticalInput;
    }

    protected void RotatePlayer(Vector3 movementDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, SPEED_MULTIPLY_KOEF * _rotationSpeed * Time.fixedDeltaTime));
    }

    protected void SetRigidbodyVelocity(Vector3 movementDirection)
    {
        Vector3 targetVelocity = movementDirection * _movementSpeed * SPEED_MULTIPLY_KOEF;
        _rigidbody.AddForce(targetVelocity, ForceMode.Acceleration);
    }
    protected virtual void ChangeMovementInput()
    {

    }
}
