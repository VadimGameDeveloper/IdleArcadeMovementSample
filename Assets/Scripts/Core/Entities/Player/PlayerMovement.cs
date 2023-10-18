using UnityEngine;
using Zenject;

public class PlayerMovement : Movement
{
    private Camera _mainCamera;
    private Joystick _joystick;


    [Inject]
    public void Constructor(Camera mainCamera, Joystick joystick)
    {
        _mainCamera = mainCamera;
        _joystick = joystick;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _joystick.OnPointerHoldEvent += OnPointerHold;
        _joystick.OnPointerUpEvent += OnPointerUp;
    }
    private void OnDestroy()
    {
        _joystick.OnPointerHoldEvent -= OnPointerHold;
        _joystick.OnPointerUpEvent -= OnPointerUp;
    }

    private void OnPointerHold(Vector2 input)
    {
        _input = input;
    }

    private void OnPointerUp(Vector2 input)
    {
        _input = Vector2.zero;
    }

    protected override Vector3 CalculateMovementDirection(Vector2 input)
    {
        float horizontalInput = input.x;
        float verticalInput = input.y;

        Vector3 forwardVector = new Vector3(_mainCamera.transform.forward.x, 0f, _mainCamera.transform.forward.z).normalized;
        Vector3 rightVector = new Vector3(_mainCamera.transform.right.x, 0f, _mainCamera.transform.right.z).normalized;

        return rightVector * horizontalInput + forwardVector * verticalInput;
    }
}
